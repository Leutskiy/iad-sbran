using Sbran.CQS.Converters;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Repositories.Contracts;

namespace Sbran.CQS.Read
{
	/// <summary>
	/// Команда чтения документов
	/// </summary>
	public sealed class DocumentReadCommand : IReadCommand<DocumentResult>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentReadCommand(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="documentId">Идентификатор документа</param>
        /// <returns>Информация о документах</returns>
        public async Task<DocumentResult> ExecuteAsync(Guid documentId)
        {
            var document = await _documentRepository.GetAsync(documentId);

            return DomainEntityConverter.ConvertToResult(document: document);
        }

        public Task<IEnumerable<DocumentResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}