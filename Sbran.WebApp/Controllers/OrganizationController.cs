using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Sbran.CQS.Read;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;

namespace Sbran.WebApp.Controllers
{
	/// <summary>
	/// Контроллер организации сотрудника
	/// </summary>
	[ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly InvitationWriteCommand _employeeWriteCommand;
        private readonly EmployeeReadCommand _employeeReadCommand;

        public OrganizationController(
            IEmployeeRepository employeeRepository,
            InvitationWriteCommand employeeWriteCommand,
            EmployeeReadCommand employeeReadCommand)
        {
            _employeeRepository = employeeRepository;
            _employeeWriteCommand = employeeWriteCommand;
            _employeeReadCommand = employeeReadCommand;
        }

        [HttpPost]
        [Route("{organizationId:guid?}")]
        public async Task<Guid> AddOrUpdateAsync(Guid? organizationId, OrganizationDto createdOrganizationData)
        {
            Contract.Argument.IsNotNull(createdOrganizationData, nameof(createdOrganizationData));

            var employeeId = await GetEmployeeIdAsync();
            return await _employeeWriteCommand.AddOrUpdateOrganizationAsync(employeeId, createdOrganizationData);
        }

        private async Task<Guid> GetEmployeeIdAsync()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            var userId = Guid.Parse(identityClaims.FindFirst("UserId").Value);
            var employee = await _employeeRepository.GetByUserIdAsync(userId);

            return employee.Id;
        }
    }
}