using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
	public sealed class InternationalAgreementRepository : IInternationalAgreementRepository
    {
        private readonly DomainContext _domainContext;

        public InternationalAgreementRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public InternationalAgreement Add(InternationalAgreementDto addedInternationalAgreement)
        {
            var internationalAgreement = new InternationalAgreement()
            {
                TheNameOfTheAgreement = addedInternationalAgreement.TheNameOfTheAgreement,
                TheFirstPartyToTheAgreement = addedInternationalAgreement.TheFirstPartyToTheAgreement,
                TheSecondPartyToTheAgreement = addedInternationalAgreement.TheSecondPartyToTheAgreement,
                EmployeeId = addedInternationalAgreement.EmployeeId,
                PlaceOfSigning = addedInternationalAgreement.PlaceOfSigning,
                DateOfEntry = addedInternationalAgreement.DateOfEntry,
                TextOfTheAgreement = addedInternationalAgreement.TextOfTheAgreement
            };
            _domainContext.InternationalAgreements.Add(internationalAgreement);
            return internationalAgreement;
        }

        public async Task DeleteAsync(Guid id)
        {
            var internationalAgreement = await _domainContext.InternationalAgreements.FirstOrDefaultAsync(e => e.Id == id);
            if (internationalAgreement != null)
            {
                _domainContext.Remove(internationalAgreement);
            }
        }

        public async Task<InternationalAgreement> GetAgreementWithSecondName(string Name)
     => await _domainContext.InternationalAgreements.FirstOrDefaultAsync(e => e.TheSecondPartyToTheAgreement.ToUpper().Contains(Name.ToUpper()));

        public async Task<List<InternationalAgreement>> GetAllAsync() => await _domainContext.InternationalAgreements.ToListAsync();

        public async Task<InternationalAgreement> GetAsync(Guid id) => await _domainContext.InternationalAgreements.FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentInternationalAgreementId, InternationalAgreementDto newInternationalAgreement)
        {
            var internationalAgreement = await _domainContext.InternationalAgreements.FirstOrDefaultAsync(e => e.Id == currentInternationalAgreementId);
            if (internationalAgreement != null)
            {

                internationalAgreement.TheNameOfTheAgreement = newInternationalAgreement.TheNameOfTheAgreement;
                internationalAgreement.TheFirstPartyToTheAgreement = newInternationalAgreement.TheFirstPartyToTheAgreement;
                internationalAgreement.TheSecondPartyToTheAgreement = newInternationalAgreement.TheSecondPartyToTheAgreement;
                internationalAgreement.PlaceOfSigning = newInternationalAgreement.PlaceOfSigning;
                internationalAgreement.DateOfEntry = newInternationalAgreement.DateOfEntry;
                internationalAgreement.TextOfTheAgreement = newInternationalAgreement.TextOfTheAgreement;
                internationalAgreement.EmployeeId = newInternationalAgreement.EmployeeId;
            }
        }
    }
}
