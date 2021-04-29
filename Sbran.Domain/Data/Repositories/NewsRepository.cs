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
	public sealed class NewsRepository : INewsRepository
    {
        private readonly DomainContext _domainContext;

        public NewsRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public News Add(NewsDto addedNews)
        {
            var news = new News()
            {
                DateTime = DateTime.Now,
                Name = addedNews.Name,
            };
            _domainContext.News.Add(news);
            return news;
        }

        public async Task DeleteAsync(Guid id)
        {
            var news = await _domainContext.News.FirstOrDefaultAsync(e => e.Id == id);
            if (news != null)
            {
                _domainContext.Remove(news);
            }
        }

        public async Task<List<News>> GetAllAsync() => await _domainContext
            .News
            .ToListAsync();

        public async Task<News> GetAsync(Guid id) => await _domainContext.News
            .FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentNewsId, NewsDto newNews)
        {
            var news = await _domainContext.News.FirstOrDefaultAsync(e => e.Id == currentNewsId);
            if (news != null)
            {
                news.DateTime = newNews.DateTime;
                news.Name = newNews.Name;
                _domainContext.News.Update(news);
            }
        }
    }
}
