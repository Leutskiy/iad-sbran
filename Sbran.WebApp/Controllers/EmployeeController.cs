using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Sbran.CQS.Read;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;

namespace Sbran.WebApp.Controllers
{
	/// <summary>
	/// Контроллер информации по сотруднику
	/// </summary>
	[ApiController]
    [Authorize]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeWriteCommand _employeeWriteCommand;
        private readonly EmployeeReadCommand _employeeReadCommand;

        public EmployeeController(
            EmployeeWriteCommand employeeWriteCommand,
            EmployeeReadCommand employeeReadCommand)
        {
            Contract.Argument.IsNotNull(employeeWriteCommand, nameof(employeeWriteCommand));
            Contract.Argument.IsNotNull(employeeReadCommand, nameof(employeeReadCommand));

            _employeeWriteCommand = employeeWriteCommand;
            _employeeReadCommand = employeeReadCommand;
        }

        [HttpGet]
        [Route("{employeeId:guid}")]
        public Task<EmployeeResult> GetAsync(Guid employeeId)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            return _employeeReadCommand.ExecuteAsync(employeeId);
        }

        [HttpPut]
        [Route("{employeeId:guid}/job")]
        public async Task UpdateJobAsync(Guid employeeId, JobDto jobData)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            await _employeeWriteCommand.UpdateEmployeeJobAsync(employeeId, jobData);
        }

        [HttpPut]
        [Route("{employeeId:guid}/contact")]
        public Task<Guid> UpdateContactAsync(Guid employeeId, ContactDto contactDto)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            return _employeeWriteCommand.AddOrUpdateContactAsync(employeeId, contactDto);
        }

        [HttpPut]
        [Route("{employeeId:guid}/passport")]
        public Task<Guid> UpdatePassportAsync(Guid employeeId, PassportDto passportDto)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            return _employeeWriteCommand.AddOrUpdatePassportAsync(employeeId, passportDto);
        }

        [HttpPut]
        [Route("{employeeId:guid}/organization")]
        public Task<Guid> UpdateOrganizationAsync(Guid employeeId, OrganizationDto organizationDto)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            return _employeeWriteCommand.AddOrUpdateOrganizationAsync(employeeId, organizationDto);
        }

        [HttpPut]
        [Route("{employeeId:guid}/scientificinfo")]
        public Task UpdateScientificInfoAsync(Guid employeeId, ScientificInfoDto scientificInfo)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            return _employeeWriteCommand.UpdateEmployeeScientificInfoAsync(employeeId, scientificInfo);
        }

        [HttpPut]
        [Route("{employeeId:guid}/stateregistration")]
        public Task<Guid> UpdateStateRegistrationAsync(Guid employeeId, StateRegistrationDto stateRegistrationDto)
        {
            Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

            return _employeeWriteCommand.AddOrUpdateStateRegistrationAsync(employeeId, stateRegistrationDto);
        }
    }
}