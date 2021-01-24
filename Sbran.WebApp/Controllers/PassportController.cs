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
    /// Контроллер информации по сотруднику
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class PassportController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeWriteCommand _employeeWriteCommand;

        public PassportController(
            IEmployeeRepository employeeRepository,
            EmployeeWriteCommand employeeWriteCommand)
        {
            _employeeRepository = employeeRepository;
            _employeeWriteCommand = employeeWriteCommand;
        }

        [HttpPost]
        [Route("{passportId:guid?}")]
        public async Task<Guid> AddOrUpdateAsync(Guid? passportId, PassportDto createdPassportData)
        {
            Contract.Argument.IsNotNull(createdPassportData, nameof(createdPassportData));

            var employeeId = await GetEmployeeIdAsync();
            return await _employeeWriteCommand.AddOrUpdatePassportAsync(employeeId, createdPassportData);
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