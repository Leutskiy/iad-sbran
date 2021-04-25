using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IContactRepository
    {
        Contact Create();

        Task DeleteAsync(Guid id);

        Task<List<Contact>> GetAllAsync();

        Task<Contact> GetAsync(Guid id);

        Contact Add(ContactDto addedContact);

        Task UpdateAsync(Guid currentContcatId, ContactDto newContact);
    }
}