using AuthorizationServer.Application.DTOs;

namespace AuthorizationServer.Application.Interfaces
{
    public interface IAuthService
    {
        Task<GeneralAuthResponse> LogInAsync(LoginDTO userCredentials);
        Task<GeneralAuthResponse> RegisterAsync(RegisterDTO user);
        Task<GeneralAuthResponse> RefreshTokenAsync(string refreshToken);
    }
}
