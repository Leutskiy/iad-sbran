﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sbran.CQS.Read;
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
            if (departureId == null)
            {
                var departure = _departureRepository.Add(createdDepartureData);
                await _domainContext.SaveChangesAsync();
                return await _departureRepository.GetAsync(departure.Id);
            }

            await _departureRepository.UpdateAsync(departureId.Value, createdDepartureData);
            await _domainContext.SaveChangesAsync();

            return await _departureRepository.GetAsync(departureId.Value);
        }

        [HttpGet]
        [Route("{employeeId:guid}")]
        public async Task<List<Departure>> getAllDepartures(Guid employeeId)
        {
            var list = await _departureRepository.GetAllAsync();
            return list.Where(e => e.EmployeeId == employeeId).ToList();
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