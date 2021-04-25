using Sbran.CQS.Converters;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Data.Repositories.Contracts;

namespace Sbran.CQS.Read
{
	/// <summary>
	/// Команда чтения контакта
	/// </summary>
	public sealed class ContactReadCommand : IReadCommand<ContactResult>
    {
        private readonly IContactRepository _contactRepository;

        public ContactReadCommand(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="contactId">Идентификатор контакта</param>
        /// <returns>Информация о контакте</returns>
        public async Task<ContactResult> ExecuteAsync(Guid contactId)
        {
            var contact = await _contactRepository.GetAsync(contactId);

            return DomainEntityConverter.ConvertToResult(contact: contact);
        }

        public Task<IEnumerable<ContactResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}