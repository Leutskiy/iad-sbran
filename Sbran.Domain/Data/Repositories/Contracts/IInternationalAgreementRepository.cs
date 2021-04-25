using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IInternationalAgreementRepository
    {
        Task DeleteAsync(Guid id);

        Task<List<InternationalAgreement>> GetAllAsync();

        Task<InternationalAgreement> GetAsync(Guid id);

        InternationalAgreement Add(InternationalAgreementDto addedInternationalAgreement);

        Task UpdateAsync(Guid currentInternationalAgreementId, InternationalAgreementDto newInternationalAgreement);

        Task<InternationalAgreement> GetAgreementWithSecondName(string Name);
    }
}