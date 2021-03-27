using Microsoft.AspNetCore.Authorization;
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
    public class InternationalAgreementController : ControllerBase
    {
        private readonly IInternationalAgreementRepository _internationalAgreementRepository;
        private readonly DomainContext _domainContext;

        public InternationalAgreementController(
            IInternationalAgreementRepository internationalAgreementRepository,
            DomainContext domainContext)
        {
            _internationalAgreementRepository = internationalAgreementRepository;
            _domainContext = domainContext;
        }

        [HttpPost]
        [Route("{internationalAgreementId:guid?}")]
        public async Task<InternationalAgreement> CreateOrUpdate(Guid? internationalAgreementId, InternationalAgreementDto createdInternationalAgreementData)
        {
            if (internationalAgreementId == null)
            {
                var internationalAgreement = _internationalAgreementRepository.Add(createdInternationalAgreementData);
                await _domainContext.SaveChangesAsync();
                return await _internationalAgreementRepository.GetAsync(internationalAgreement.Id);
            }

            await _internationalAgreementRepository.UpdateAsync(internationalAgreementId.Value, createdInternationalAgreementData);
            await _domainContext.SaveChangesAsync();

            return await _internationalAgreementRepository.GetAsync(internationalAgreementId.Value);
        }

        [HttpGet]
        [Route("{employeeId:guid}")]
        public async Task<List<InternationalAgreement>> getAllInternationalAgreements(Guid employeeId)
        {
            var list = await _internationalAgreementRepository.GetAllAsync();
            return list.Where(e => e.EmployeeId == employeeId).ToList();
        }
    }
}
