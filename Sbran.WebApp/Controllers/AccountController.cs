using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using Sbran.WebApp.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sbran.WebApp.Controllers
{
	[ApiController]
    [Authorize]
    [Route("api/v2/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly DomainContext _context;
        private readonly IUserService _userService;
        private readonly IJwtAuthService _jwtAuthManager;
        private readonly SystemContext _systemContext;
        private readonly INewsRepository _newsRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVoteListRepository _voteListRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IReadCommand<ProfileResult> _profileReadCommand;

        public AccountController(ILogger<AccountController> logger, IUserService userService, IJwtAuthService jwtAuthManager, INewsRepository newsRepository, IEmployeeRepository employeeRepository, IVoteListRepository voteListRepository, DomainContext context, SystemContext systemContext, IReadCommand<ProfileResult> profileReadCommand, IVoteRepository voteRepository)
        {
            _logger = logger;
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
            _employeeRepository = employeeRepository;
            _voteListRepository = voteListRepository;
            _newsRepository = newsRepository;
            _context = context;
            _systemContext = systemContext;
            _voteListRepository = voteListRepository;
            _profileReadCommand = profileReadCommand;
            _voteRepository = voteRepository;
        }

        [Obsolete("Сейчас не используем. Вместо этого работает в режиме Identity Server и выдаем токены через отдельную службу.")]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _userService.IsValidUserCredentialsAsync(request.UserName, request.Password))
            {
                return Unauthorized();
            }

            var role = _userService.DetectUserRole(request.UserName);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{request.UserName}] logged in the system.");

            return Ok(new LoginResult
            {
                UserName = request.UserName,
                Role = role,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        // Вывод информации для главное страницы
        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<MainDto> LoginGet()
        {
            var news = await _newsRepository.GetAllAsync();
            var employees = await _employeeRepository.GetAllAsync();
            var votes = await _voteRepository.GetAllAsync();
            if (votes != null)
            {
                foreach (var temp in votes)
                {
                    double summ = 0;
                    temp.voteLists = _context.VoteLists.OrderBy(e=>e.Id).Where(e => e.VoteId == temp.Id).ToList();
                    foreach (var tempx in temp.voteLists)
                    {
                        summ += tempx.Count;
                        tempx.Vote = null;
                    }
                    foreach (var tempx in temp.voteLists)
                    {
                        if (summ != 0)
                        {
                            tempx.Count = (int)((tempx.Count / summ) * 100);
                        }
                    }
                }
            }
            var users = new List<EmployeeInfo>();
            if (employees != null)
            {
                foreach (var temp in employees)
                {
                    var user = await _systemContext.Users
                        .Include(e => e.Profile)
                        .FirstOrDefaultAsync(e => e.Id == temp.UserId);
                    string image = "assets/images/avatar.jpg";
                    if (user != null)
                    {
                        if (user.Profile.Photo != null)
                        {
                            image = "data:image/jpeg;base64," + Convert.ToBase64String(user?.Profile?.Photo);
                        }
                    }
                    var emp = new EmployeeInfo
                    {
                        Email = temp?.Contact?.Email ?? "",
                        PhoneNumber = temp?.Contact?.MobilePhoneNumber ?? "",
                        FullName = temp?.Passport?.ToFio() ?? "",
                        Avatar = image,
                    };
                    users.Add(emp);
                }
            }
            var main = new MainDto
            {
                News = news,
                Votes = votes,
                Employees = users,
                CountEmployees = employees.Count,
                CountOnlineEmployees = UserHandler.ConnectedIds.Count
            };

            return main;
        }

        [AllowAnonymous]
        [HttpPost("addNews")]
        public async Task<News> AddNews(NewsDto news)
        {
            var newNews = _newsRepository.Add(news);
            await _context.SaveChangesAsync();
            return newNews;
        }

        [AllowAnonymous]
        [HttpPost("sendVoteList/{id:guid}")]
        public async Task<IActionResult> SendVoteList(Guid id)
        {
            await _voteListRepository.AddCount(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("addVote")]
        public async Task<IActionResult> addvote(VoteDto votes)
        {
            var vote = _voteRepository.Add(votes);
            await _context.SaveChangesAsync();
            if (votes.voteLists != null)
            {
                foreach (var temp in votes.voteLists)
                {
                    temp.VoteId = vote.Id;
                    _voteListRepository.Add(temp);
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("user")]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            return Ok(new LoginResult
            {
                UserName = User.Identity.Name,
                Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                OriginalUserName = User.FindFirst("OriginalUserName")?.Value
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            // optionally "revoke" JWT token on the server side --> add the current token to a block-list
            // https://github.com/auth0/node-jsonwebtoken/issues/375

            var userName = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            _logger.LogInformation($"User [{userName}] logged out the system.");
            return Ok();
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity.Name;
                _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                _logger.LogInformation($"User [{userName}] has refreshed JWT token.");
                return Ok(new LoginResult
                {
                    UserName = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }

        [HttpPost("impersonation")]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult Impersonate([FromBody] ImpersonationRequest request)
        {
            var userName = User.Identity.Name;
            _logger.LogInformation($"User [{userName}] is trying to impersonate [{request.UserName}].");

            var impersonatedRole = _userService.DetectUserRole(request.UserName);
            if (string.IsNullOrWhiteSpace(impersonatedRole))
            {
                _logger.LogInformation($"User [{userName}] failed to impersonate [{request.UserName}] due to the target user not found.");
                return BadRequest($"The target user [{request.UserName}] is not found.");
            }
            if (impersonatedRole == UserRoles.Admin)
            {
                _logger.LogInformation($"User [{userName}] is not allowed to impersonate another Admin.");
                return BadRequest("This action is not supported.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName),
                new Claim(ClaimTypes.Role, impersonatedRole),
                new Claim("OriginalUserName", userName)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{request.UserName}] is impersonating [{request.UserName}] in the system.");
            return Ok(new LoginResult
            {
                UserName = request.UserName,
                Role = impersonatedRole,
                OriginalUserName = userName,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        [HttpPost("stop-impersonation")]
        public ActionResult StopImpersonation()
        {
            var userName = User.Identity.Name;
            var originalUserName = User.FindFirst("OriginalUserName")?.Value;
            if (string.IsNullOrWhiteSpace(originalUserName))
            {
                return BadRequest("You are not impersonating anyone.");
            }
            _logger.LogInformation($"User [{originalUserName}] is trying to stop impersonate [{userName}].");

            var role = _userService.DetectUserRole(originalUserName);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,originalUserName),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(originalUserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{originalUserName}] has stopped impersonation.");
            return Ok(new LoginResult
            {
                UserName = originalUserName,
                Role = role,
                OriginalUserName = null,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }
    }
}
