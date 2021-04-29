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
    public class ScientificInterestsController : ControllerBase
    {
        private readonly IScientificInterestsRepository _scientificInterestsRepository;
        private readonly DomainContext _domainContext;

        public ScientificInterestsController(
            IScientificInterestsRepository scientificInterestsRepository,
            DomainContext domainContext)
        {
            _scientificInterestsRepository = scientificInterestsRepository;
            _domainContext = domainContext;
        }

        [HttpPost]
        [Route("{scientificInterestsId:guid?}")]
        public async Task<ScientificInterests> CreateOrUpdate(Guid? scientificInterestsId, ScientificInterestsDto createdScientificInterestsData)
        {
            if (scientificInterestsId == null)
            {
                var scientificInterests = _scientificInterestsRepository.Add(createdScientificInterestsData);
                await _domainContext.SaveChangesAsync();
                return await _scientificInterestsRepository.GetAsync(scientificInterests.Id);
            }

            await _scientificInterestsRepository.UpdateAsync(scientificInterestsId.Value, createdScientificInterestsData);
            await _domainContext.SaveChangesAsync();

            return await _scientificInterestsRepository.GetAsync(scientificInterestsId.Value);
        }

        [HttpGet]
        [Route("{employeeId:guid}")]
        public async Task<List<ScientificInterests>> getAllScientificInterestss(Guid employeeId)
        {
            var list = await _scientificInterestsRepository.GetAllAsync();
            return list.Where(e => e.EmployeeId == employeeId).ToList();
        }
    }
}
