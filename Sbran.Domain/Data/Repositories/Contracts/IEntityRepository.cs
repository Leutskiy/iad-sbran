using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories.Contracts
{
	public interface IEntityRepository<T>
    {

        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);

        Task<T> CreateAsync(T model);

        Task DeleteAsync(Guid id);


    }
}
