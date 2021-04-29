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
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly DomainContext _domainContext;

        public MembershipController(
            IMembershipRepository membershipRepository,
            DomainContext domainContext)
        {
            _membershipRepository = membershipRepository;
            _domainContext = domainContext;
        }

        [HttpPost]
        [Route("{membershipId:guid?}")]
        public async Task<Membership> CreateOrUpdate(Guid? membershipId, MembershipDto createdMembershipData)
        {
            if (membershipId == null)
            {
                var membership = _membershipRepository.Add(createdMembershipData);
                await _domainContext.SaveChangesAsync();
                return await _membershipRepository.GetAsync(membership.Id);
            }

            await _membershipRepository.UpdateAsync(membershipId.Value, createdMembershipData);
            await _domainContext.SaveChangesAsync();

            return await _membershipRepository.GetAsync(membershipId.Value);
        }

        [HttpGet]
        [Route("{employeeId:guid}/{type}")]
        public async Task<List<Membership>> getAllMemberships(Guid employeeId, int type)
        {
            var list = await _membershipRepository.GetAllAsync();
            if (type == 1)
            {
                return list.Where(e => e.EmployeeId == employeeId && e.MembershipType == Domain.Enums.MembershipType.Russian).ToList();
            }
            return list.Where(e => e.EmployeeId == employeeId && e.MembershipType == Domain.Enums.MembershipType.Other).ToList();

        }
    }
}
