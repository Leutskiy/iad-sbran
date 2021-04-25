using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sbran.Domain.Data.Repositories.Contracts;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sbran.WebApp
{
    [AllowAnonymous]
    [ApiController]
    [Route("")]
    public class SimpleAuthorizationController : ControllerBase
    {
        private readonly ILogger<SimpleAuthorizationController> _logger;
        private readonly IUserService _userService;
        private readonly IJwtAuthService _jwtAuthManager;
        private readonly IUserRepository _userRepository;

        public SimpleAuthorizationController(ILogger<SimpleAuthorizationController> logger, IUserService userService, IJwtAuthService jwtAuthManager, IUserRepository userRepository)
        {
            _logger = logger;
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
            _userRepository = userRepository;
        }

        // POST /token
        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<IActionResult> GetTokenAsync(LoginRequest loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _userService.IsValidUserCredentialsAsync(loginModel.UserName, loginModel.Password).ConfigureAwait(false))
            {
                return Unauthorized();
            }

            var user = await _userRepository.Get(loginModel.UserName, loginModel.Password);

            var role = _userService.GetUserRole(loginModel.UserName);
            var claims = new[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, loginModel.UserName),
                new Claim(ClaimTypes.NameIdentifier, loginModel.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                new Claim("UserId", $"{user.Id}")
                
            };


            var jwtResult = _jwtAuthManager.GenerateTokens(loginModel.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{loginModel.UserName}] logged in the system.");
            return Ok(new LoginResult
            {
                UserName = loginModel.UserName,
                Role = role,
                //ExpiresAt = jwtResult.ExpiresAt.HasValue ? jwtResult.ExpiresAt.Value.Millisecond : 0,
                ExpiresAt = jwtResult.ExpiresAt,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });

        }
    }
}
