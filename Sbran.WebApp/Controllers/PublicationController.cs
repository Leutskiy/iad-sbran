using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sbran.WebApp.Controllers
{
	/// <summary>
	/// Контроллер контактных данных по сотруднику
	/// </summary>
	[ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly DomainContext _domainContext;

        public PublicationController(
            IPublicationRepository publicationRepository,
            DomainContext domainContext)
        {
            _publicationRepository = publicationRepository;
            _domainContext = domainContext;
        }

        [HttpPost]
        [Route("{publicationId:guid?}")]
        public async Task<Publication> CreateOrUpdate(Guid? publicationId, PublicationDto createdPublicationData)
        {
            if (publicationId == null)
            {
                var publication = _publicationRepository.Add(createdPublicationData);
                await _domainContext.SaveChangesAsync();
                return await _publicationRepository.GetAsync(publication.Id);
            }

            await _publicationRepository.UpdateAsync(publicationId.Value, createdPublicationData);
            await _domainContext.SaveChangesAsync();

            return await _publicationRepository.GetAsync(publicationId.Value);
        }

        [HttpGet]
        [Route("{employeeId:guid}/all")]
        public Task<List<Publication>> getAllPublications(Guid employeeId)
        {
            var isAdmin = User.IsInRole(UserRoles.Admin);
            if (isAdmin)
			{
                return _publicationRepository.GetAllAsync();
            }
            else
			{
                return _publicationRepository.GetByEmplIdAsync(employeeId);
            }
        }

        //TODO: Почему это находится в этом контроллере?
        [HttpGet]
        [Route("{publicationId:guid}")]
        public Task<Publication> GetPublicationById(Guid publicationId)
        {
            return _publicationRepository.GetAsync(publicationId);
        }
    }
}
