using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sbran.WebApp
{
    [ApiController]
    [Route("")]
    public sealed class SimpleAuthorizationController : ControllerBase
    {
        private readonly ILogger<SimpleAuthorizationController> _logger;
        private readonly IUserService _userService;
        private readonly IJwtAuthService _jwtAuthManager;

		public SimpleAuthorizationController(
			ILogger<SimpleAuthorizationController> logger,
			IUserService userService,
			IJwtAuthService jwtAuthManager)
        {
            _logger = logger;
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        /// <summary>
        /// Получить докен доступа
        /// </summary>
        /// <param name="loginModel">Модель данных входа пользователя в систему</param>
        /// <returns>Токен доступа</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<IActionResult> GetTokenAsync(LoginRequest loginModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"The login model is not valid for [{loginModel.UserName}] user");
                return BadRequest(ModelState);
            }

            var userCredentialsValidity = await _userService.IsValidUserCredentialsAsync(loginModel.UserName, loginModel.Password);
            if (userCredentialsValidity is false)
            {
                _logger.LogInformation($"Incorrectly passed user [{loginModel.UserName}] login and [{loginModel.Password}] password");
                return Unauthorized();
            }

            var user = await _userService.GetUserAsync(login: loginModel.UserName, password: loginModel.Password);
            var role = _userService.DetectUserRole(login: loginModel.UserName);

            // TODO: доработать набор утверждений о пользователе
            // оставить только имя/роль/идентификатор
            var claims = new[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, loginModel.UserName),
                new Claim(ClaimTypes.NameIdentifier, loginModel.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                new Claim("UserId", $"{user.Id}")
            };

            _logger.LogInformation($"Start generating of the access token for [{loginModel.UserName}] user");

            var jwtAuthResult = _jwtAuthManager.GenerateTokens(loginModel.UserName, claims, DateTime.Now);

            _logger.LogInformation($"The access token is created for [{loginModel.UserName}] user");
            _logger.LogDebug($"The access token: [{jwtAuthResult.AccessToken}]");

            _logger.LogInformation($"The [{loginModel.UserName}] user is logged in the system");

            return Ok(new LoginResult
            {
                Role = role,
                UserName = loginModel.UserName,
                ExpiresAt = jwtAuthResult.ExpiresAt,
                AccessToken = jwtAuthResult.AccessToken,
                RefreshToken = jwtAuthResult.RefreshToken.TokenString
            });
        }
    }
}
