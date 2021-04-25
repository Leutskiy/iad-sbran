using Sbran.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;

namespace Sbran.WebApp.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountOldController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly DomainContext _domainContext;
        private readonly SystemContext _systemContext;

        public AccountOldController(
            IProfileRepository profileRepository,
            IEmployeeRepository employeeRepository,
            DomainContext domainContext,
            SystemContext systemContext,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _employeeRepository = employeeRepository;
            _domainContext = domainContext;
            _systemContext = systemContext;
        }

        [HttpGet]
        [Authorize]
        [Route("details")]
        public  async Task<AccountDetailsDto> GetDetailsAsync()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;

            var userId = Guid.Parse(identityClaims.FindFirst("UserId").Value);

            var profileId = await _userRepository.GetProfileId(userId);
            var employeeId = await _userRepository.GetEmployeeId(userId);

            var model = new AccountDetailsDto()
            {
                ProfileId = $"{profileId}",
                EmployeeId = $"{employeeId}"
            };

            return model;
        }

        // POST api/account/register
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProfile = _profileRepository.Create();
            var newUser = _userRepository.Create(model.UserName, model.Password, newProfile);
            
            _employeeRepository.Create(newUser.Id);

            await _systemContext.SaveChangesAsync();
            await _domainContext.SaveChangesAsync();

            return Ok();
        }
    }
}
