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
    public sealed class PublicationRepository : IPublicationRepository
    {
        private readonly DomainContext _domainContext;

        public PublicationRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public Publication Add(PublicationDto addedPublication)
        {
            var publication = new Publication()
            {
                ScientificAdvisor = addedPublication.ScientificAdvisor,
                TitleOfTheArticle = addedPublication.TitleOfTheArticle,
                Abstract = addedPublication.Abstract,
                Keywords = addedPublication.Keywords,
                MainTextOfTheArticle = addedPublication.MainTextOfTheArticle,
                Literature = addedPublication.Literature,
                EmployeeId = addedPublication.EmployeeId
            };
            _domainContext.Publications.Add(publication);
            return publication;
        }

        public async Task DeleteAsync(Guid id)
        {
            var publication = await _domainContext.Publications.FirstOrDefaultAsync(e => e.Id == id);
            if (publication != null)
            {
                _domainContext.Remove(publication);
            }
        }

        public async Task<List<Publication>> GetAllAsync() => await _domainContext.Publications.ToListAsync();

        public async Task<Publication> GetAsync(Guid id) => await _domainContext.Publications.FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentPublicationId, PublicationDto newPublication)
        {
            var publication = await _domainContext.Publications.FirstOrDefaultAsync(e => e.Id == currentPublicationId);
            if (publication != null)
            {

                publication.ScientificAdvisor = newPublication.ScientificAdvisor;
                publication.TitleOfTheArticle = newPublication.TitleOfTheArticle;
                publication.Abstract = newPublication.Abstract;
                publication.Keywords = newPublication.Keywords;
                publication.MainTextOfTheArticle = newPublication.MainTextOfTheArticle;
                publication.Literature = newPublication.Literature;
                publication.EmployeeId = newPublication.EmployeeId;
            }
        }
    }
}
