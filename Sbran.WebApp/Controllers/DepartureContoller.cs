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
    public class DepartureController : ControllerBase
    {
        private readonly IDepartureRepository _departureRepository;
        private readonly DomainContext _domainContext;

        public DepartureController(
            IDepartureRepository departureRepository,
            DomainContext domainContext)
        {
            _departureRepository = departureRepository;
            _domainContext = domainContext;
        }

        [HttpPost]
        [Route("{departureId:guid?}")]
        public async Task<Departure> CreateOrUpdate(Guid? departureId, DepartureDto createdDepartureData)
        {
			Departure departure;
			if (departureId.HasValue)
            {
                await _departureRepository.UpdateAsync(departureId.Value, createdDepartureData);
                departure = await _departureRepository.GetAsync(departureId.Value);
            }
			else
			{
                departure = _departureRepository.Add(createdDepartureData);
            }

            await _domainContext.SaveChangesAsync();

            return departure;
        }

        //TODO: Почему это находится в этом контроллере?
        [HttpGet]
        [Route("all/{employeeId:guid}")]
        public Task<List<Departure>> GetAllDepartures(Guid employeeId)
        {
            var isAdmin = User.IsInRole(UserRoles.Admin);
            if (isAdmin)
			{
                return _departureRepository.GetAllAsync();
			}
            else
			{
                return _departureRepository.GetByEmplIdAsync(employeeId);
            } 
        }

        //TODO: Почему это находится в этом контроллере?
        [HttpGet]
        [Route("{departureId:guid}")]
        public Task<Departure> GetDepartureById(Guid departureId)
        {
            return _departureRepository.GetAsync(departureId);
        }

        [HttpGet]
        [Route("{id:guid}/agree")]
        public async Task<IActionResult> Agree(Guid id)
        {
            await _departureRepository.Agree(id);
            return Ok();
        }
    }
}
