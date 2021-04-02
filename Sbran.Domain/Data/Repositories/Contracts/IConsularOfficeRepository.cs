using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IConsularOfficeRepository
    {
        Task DeleteAsync(Guid id);

        Task<List<ConsularOffice>> GetAllAsync();

        Task<ConsularOffice> GetAsync(Guid id);

        ConsularOffice Add(ConsularOfficeDto addedConsularOffice);

        Task UpdateAsync(Guid currentConsularOfficeId, ConsularOfficeDto newConsularOffice);
        Task<ConsularOffice> GetAgreementWithSecondName(string Name);
    }
}