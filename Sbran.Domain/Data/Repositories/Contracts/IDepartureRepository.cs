using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IDepartureRepository
    {
        Task<int> Total();

        Task DeleteAsync(Guid id);

        Task<List<Departure>> GetByEmplIdAsync(Guid employeeId);

        Task<List<Departure>> GetAllAsync();

        Task<Departure> GetAsync(Guid id);

        Departure Add(DepartureDto addedDeparture);

        Task UpdateAsync(Guid currentDepartureId, DepartureDto newDeparture);
        Task SetReport(Guid id, Guid parentId);
        Task Agree(Guid id);
    }
}