using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Entities;
using Sbran.Domain.Enums;
using Sbran.Domain.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Repositories
{
	/// <summary>
	/// Репозиторий документов
	/// </summary>
	public sealed class DocumentRepository : IDocumentRepository
    {
        private readonly DomainContext _context;

        public DocumentRepository(DomainContext databaseContext)
        {
            _context = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        /// <summary>
        /// Получить все документы
        /// </summary>
        /// <returns>Документы</returns>
        public Task<List<Document>> GetAllAsync()
        {
            return _context.Set<Document>().ToListAsync();
        }

        /// <summary>
        /// Получить документ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор документа</param>
        /// <returns>Документ</returns>
        public async Task<Document> GetAsync(Guid id)
        {
            var document = await _context.Set<Document>().FindAsync(id);

            if (document == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return document;
        }

        /// <summary>
        /// Создать документ
        /// </summary>
        /// <param name="invitation">TODO</param>
        /// <param name="name">Название</param>
        /// <param name="content">Содержимое</param>
        /// <param name="createdDate">Дата создания</param>
        /// <param name="updateDate">Дата изменения</param>
        /// <param name="documentType">Тип документа</param>
        /// <returns>Идентификатор документа</returns>
        public Guid Create(
            Invitation invitation,
            string name,
            byte[] content,
            DocumentType documentType)
        {
            var createdDocument = new Document(
                invitation: invitation,
                name: name,
                content: content,
                documentType: documentType);

            _context.Set<Document>().Add(createdDocument);

            return createdDocument.Id;
        }

        /// <summary>
        /// Удалить документ
        /// </summary>
        /// <param name="id">Идентификатор документа</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }

            var deletedDocument = await _context.Set<Document>()
                .FirstOrDefaultAsync(documentItem => documentItem.Id == id);

            if (deletedDocument == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            _context.Set<Document>().Remove(deletedDocument);
        }
    }
}