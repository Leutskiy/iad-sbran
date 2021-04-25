using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
	public interface IPassportRepository
    {
        Passport Create();

        Passport Add(PassportDto addedPassport);

        Task UpdateAsync(Guid currentPassportId, PassportDto newPassport);

        Task DeleteAsync(Guid id);

        Task<List<Passport>> GetAllAsync();

        Task<Passport> GetAsync(Guid id);
    }
}