using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sbran.CQS.Read;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sbran.WebApp.Controllers
{
	/// <summary>
	/// Контроллер государственных регистрационных данных
	/// </summary>
	[ApiController]
    [Authorize]
    [Route("api/v1/stateregistration")]
    public class StateRegistrationController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeWriteCommand _employeeWriteCommand;

        public StateRegistrationController(
            IEmployeeRepository employeeRepository,
            EmployeeWriteCommand employeeWriteCommand)
        {
            _employeeRepository = employeeRepository;
            _employeeWriteCommand = employeeWriteCommand;
        }

        [HttpPost]
        [Route("{stateregistrationId:guid?}")]
        public async Task<Guid> AddOrUpdateAsync(Guid? stateRegistrationId, StateRegistrationDto createdStateRegistrationData)
        {
            Contract.Argument.IsNotNull(createdStateRegistrationData, nameof(createdStateRegistrationData));

            var employee = await GetEmployeeIdAsync();
            return await _employeeWriteCommand.AddOrUpdateStateRegistrationAsync(employee.Id, createdStateRegistrationData);
        }

        private Task<Employee> GetEmployeeIdAsync()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            var userId = Guid.Parse(identityClaims.FindFirst("UserId").Value);
            return _employeeRepository.GetByUserIdAsync(userId);
        }
    }
}