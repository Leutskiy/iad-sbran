using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sbran.WebApp.Controllers
{
	/// <summary>
	/// Контроллер контактных данных по сотруднику
	/// </summary>
	[ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ConsularOfficeController : ControllerBase
    {
        private readonly IConsularOfficeRepository _consularOfficeRepository;
        private readonly DomainContext _domainContext;

        public ConsularOfficeController(
            IConsularOfficeRepository consularOfficeRepository,
            DomainContext domainContext)
        {
            _consularOfficeRepository = consularOfficeRepository;
            _domainContext = domainContext;
        }

        [HttpPost]
        [Route("{consularOfficeId:guid?}")]
        public async Task<ConsularOffice> CreateOrUpdate(Guid? consularOfficeId, ConsularOfficeDto createdConsularOfficeData)
        {
            if (consularOfficeId == null)
            {
                var consularOffice = _consularOfficeRepository.Add(createdConsularOfficeData);
                await _domainContext.SaveChangesAsync();
                return await _consularOfficeRepository.GetAsync(consularOffice.Id);
            }

            await _consularOfficeRepository.UpdateAsync(consularOfficeId.Value, createdConsularOfficeData);
            await _domainContext.SaveChangesAsync();

            return await _consularOfficeRepository.GetAsync(consularOfficeId.Value);
        }

        [HttpGet]
        [Route("{employeeId:guid}")]
        public async Task<List<ConsularOffice>> getAllConsularOffices(Guid employeeId)
        {
            var list = await _consularOfficeRepository.GetAllAsync();
            return list.Where(e => e.EmployeeId == employeeId).ToList();
        }
    }
}
