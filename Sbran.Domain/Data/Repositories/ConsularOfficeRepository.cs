using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
    public sealed class ConsularOfficeRepository : IConsularOfficeRepository
    {
        private readonly DomainContext _domainContext;

        public ConsularOfficeRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public ConsularOffice Add(ConsularOfficeDto addedConsularOffice)
        {
            var consularOffice = new ConsularOffice()
            {
                NameOfTheConsularPost = addedConsularOffice.NameOfTheConsularPost,
                CountryOfLocation = addedConsularOffice.CountryOfLocation,
                CityOfLocation = addedConsularOffice.CityOfLocation,
                EmployeeId = addedConsularOffice.EmployeeId,
                TextOfAgreement = addedConsularOffice.TextOfAgreement,
            };
            _domainContext.ConsularOffices.Add(consularOffice);
            return consularOffice;
        }

        public async Task DeleteAsync(Guid id)
        {
            var consularOffice = await _domainContext.ConsularOffices.FirstOrDefaultAsync(e => e.Id == id);
            if (consularOffice != null)
            {
                _domainContext.Remove(consularOffice);
            }
        }

        public async Task<ConsularOffice> GetAgreementWithSecondName(string Name)
          => await _domainContext.ConsularOffices.FirstOrDefaultAsync(e => e.CountryOfLocation.ToUpper().Contains(Name.ToUpper()));

        public async Task<List<ConsularOffice>> GetAllAsync() => await _domainContext.ConsularOffices.ToListAsync();

        public async Task<ConsularOffice> GetAsync(Guid id) => await _domainContext.ConsularOffices.FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentConsularOfficeId, ConsularOfficeDto newConsularOffice)
        {
            var consularOffice = await _domainContext.ConsularOffices.FirstOrDefaultAsync(e => e.Id == currentConsularOfficeId);
            if (consularOffice != null)
            {

                consularOffice.NameOfTheConsularPost = newConsularOffice.NameOfTheConsularPost;
                consularOffice.CountryOfLocation = newConsularOffice.CountryOfLocation;
                consularOffice.CityOfLocation = newConsularOffice.CityOfLocation;
                consularOffice.EmployeeId = newConsularOffice.EmployeeId;
                consularOffice.TextOfAgreement = newConsularOffice.TextOfAgreement;
            }
        }
    }
}
