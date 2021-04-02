using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IListOfScientistRepository
    {
        Task DeleteAsync(Guid id);

        Task<List<ListOfScientist>> GetAllAsync();

        Task<ListOfScientist> GetAsync(Guid id);

        ListOfScientist Add(ListOfScientistDto addedListOfScientist);
        Task DeleteAllAsync(Guid id);

        Task UpdateAsync(Guid currentListOfScientistId, ListOfScientistDto newListOfScientist);
    }
}