using AuthorizationServer.Application.DTOs;
using AuthorizationServer.Application.Interfaces;
using AuthorizationServer.Domain.Models;
using AuthorizationServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationServer.Application.Services
{
    public class AuthService : IAuthService
    {
        const int AccessTokenExpirationHours = 1;
        const int RefreshTokenExpirationHours = 48;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly RefreshTokensDbContext _refreshTokenDbContext;

        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> options, RefreshTokensDbContext refreshTokensDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = options.Value;
            _refreshTokenDbContext = refreshTokensDbContext;
        }

        public async Task<GeneralAuthResponse> LogInAsync(LoginDTO userCredentials)
        {
            ArgumentNullException.ThrowIfNull(nameof(userCredentials));
            var user = await _userManager.FindByNameAsync(userCredentials.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, userCredentials.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var authClaims = GenerateClaims(roles);
                var token = GenerateToken(authClaims);
                var refreshToken = GenerateRefreshToken();
                SaveRefreshToken(user.Id, refreshToken);
                return new GeneralAuthResponse(true, "User authorized successfully!", token, refreshToken);
            }
            return new GeneralAuthResponse("Invalid login or password.");
        }

        public async Task<GeneralAuthResponse> RegisterAsync(RegisterDTO user)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
            {
                return new GeneralAuthResponse("There is already a user with the same login.");
            }
            user.Roles.ConvertAll(p => p.ToLower());
            var refreshTokenString = GenerateRefreshToken();

            var refreshToken = new RefreshToken() { CreatedDate = DateTime.Now, ExpirationDate = DateTime.Now.AddHours(RefreshTokenExpirationHours), TokenString = refreshTokenString };
            AppUser applicationUser = new AppUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RefreshTokens = new List<RefreshToken>() { refreshToken }
            };

            if (!await ValidateRoles(user.Roles))
            {
                return new GeneralAuthResponse("Invalid role.");
            }

            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded)
            {
                return new GeneralAuthResponse("Cannot register user.");
            }

            await _userManager.AddToRolesAsync(applicationUser, user.Roles);

            var authClaims = GenerateClaims(user.Roles);
            var token = GenerateToken(authClaims);

            await _refreshTokenDbContext.RefreshToken.AddAsync(refreshToken);
            await _refreshTokenDbContext.SaveChangesAsync();

            return new GeneralAuthResponse(true, "User registered successfully!", token, refreshTokenString);
        }

        private async Task<bool> ValidateRoles(IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return false;
                }
            }
            return true;
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            byte[] securityKey = Encoding.UTF8.GetBytes(_appSettings.EncryptionKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(securityKey);

            var securityToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(AccessTokenExpirationHours),
                claims: claims,
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
                issuer: _appSettings.ValidIssuer,
                audience: _appSettings.ValidAudience
            );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        
        private void SaveRefreshToken(string userId, string refreshToken)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user.RefreshTokens == null)
            {
                user.RefreshTokens = new List<RefreshToken>();
            }

            user.RefreshTokens.Add(new RefreshToken
            {
                TokenString = refreshToken,
                ExpirationDate = DateTime.Now.AddHours(RefreshTokenExpirationHours),
                CreatedDate = DateTime.Now
            });

            _userManager.UpdateAsync(user).GetAwaiter().GetResult();
        }

        public async Task<GeneralAuthResponse> RefreshTokenAsync(string refreshToken)
        {
            var token = await _refreshTokenDbContext.RefreshToken.Where(r => r.TokenString == refreshToken).FirstOrDefaultAsync();
            
            if (token == null)
                return new GeneralAuthResponse(false, "Refresh token was not found.", null, null);

            var user = await _userManager.Users.Where(u => u.Id == token.AppUserId).FirstOrDefaultAsync();

            if (user == null)
                return new GeneralAuthResponse(false, "Invalid refresh token.", null, null);

            if (token.ExpirationDate < DateTime.UtcNow)
                return new GeneralAuthResponse(false, "Refresh token has expired.", null, null);

            var roles = await _userManager.GetRolesAsync(user);
            var authClaims = GenerateClaims(roles);
            var accessToken = GenerateToken(authClaims);

            return new GeneralAuthResponse(true, string.Empty, accessToken, refreshToken);
        }

        private List<Claim> GenerateClaims(IEnumerable<string> roles)
        {
            var authClaims = new List<Claim>();
            foreach (var userRole in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }
    }
}
