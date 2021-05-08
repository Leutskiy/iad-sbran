using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IPublicationRepository
    {
        Task<int> Total();

        Task<List<Publication>> GetByEmplIdAsync(Guid employeeId);

        Publication Add(PublicationDto addedPublication);

        Task DeleteAsync(Guid id);

        Task<List<Publication>> GetAllAsync();

        Task<Publication> GetAsync(Guid id);
        Task UpdateAsync(Guid id, PublicationDto publicationDto);
    }
}