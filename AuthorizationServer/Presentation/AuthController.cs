using AuthorizationServer.Application.DTOs;
using AuthorizationServer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Presentation
{
    [ApiController]
    [Route("API/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        public AuthController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<GeneralAuthResponse>> LogIn([FromBody] LoginDTO loginDTO)
        {
            var response = await _authenticationService.LogInAsync(loginDTO);
            return response.Success ? response : BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralAuthResponse>> Register([FromBody] RegisterDTO registerDTO)
        {
            var response = await _authenticationService.RegisterAsync(registerDTO);
            return response.Success ? response : BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralAuthResponse>> RefreshTokenAsync(string refreshToken)
        {
            var response = await _authenticationService.RefreshTokenAsync(refreshToken);
            return response.Success ? response : BadRequest(response);
        }
    }
}
