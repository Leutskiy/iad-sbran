using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Enums;

namespace Sbran.Domain.Repositories.Contracts
{
    public interface IDocumentRepository
    {
        Guid Create(
            Invitation invitation,
            string name,
            byte[] content,
            DocumentType documentType);

        Task DeleteAsync(Guid id);

        Task<List<Document>> GetAllAsync();

        Task<Document> GetAsync(Guid id);
    }
}