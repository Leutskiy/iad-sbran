using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface INewsRepository
    {
        Task DeleteAsync(Guid id);

        Task<List<News>> GetAllAsync();

        Task<News> GetAsync(Guid id);

        News Add(NewsDto addedNews);

        Task UpdateAsync(Guid currentNewsId, NewsDto newNews);
    }
}