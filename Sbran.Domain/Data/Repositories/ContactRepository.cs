using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
	/// <summary>
	/// Репозиторий контактов
	/// </summary>
	public sealed class ContactRepository : IContactRepository
    {
        private readonly DomainContext _domainContext;

        public ContactRepository(DomainContext databaseContext)
        {
            _domainContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        /// <summary>
        /// Получить все контакты
        /// </summary>
        /// <returns>Контакты</returns>
        public Task<List<Contact>> GetAllAsync()
        {
            return _domainContext.Set<Contact>().ToListAsync();
        }

        /// <summary>
        /// Получить контакт
        /// </summary>
        /// <param name="id">Идентификатор контакта</param>
        /// <returns>Контакт</returns>
        public async Task<Contact> GetAsync(Guid id)
        {
            var contact = await _domainContext.Set<Contact>().FindAsync(id);
            if (contact == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return contact;
        }

        /// <summary>
        /// Создать контакт
        /// </summary>
        /// <returns></returns>
        public Contact Create()
        {
            var createdContact = new Contact();

            _domainContext.Set<Contact>().Add(createdContact);

            return createdContact;
        }

        /// <summary>
        /// Добавить контакт
        /// </summary>
        /// <param name="addedContact">Добавляемый контакт</param>
        /// <returns>Контакт</returns>
        public Contact Add(ContactDto addedContact)
        {
            var contact = Create();

            contact.SetEmail(addedContact.Email);
            contact.SetPostcode(addedContact.Postcode);
            contact.SetHomePhoneNumber(addedContact.HomePhoneNumber);
            contact.SetWorkPhoneNumber(addedContact.WorkPhoneNumber);
            contact.SetMobilePhoneNumber(addedContact.MobilePhoneNumber);

            return contact;
        }

        public async Task UpdateAsync(Guid currentContcatId, ContactDto newContact)
        {
            var currentContact = await GetAsync(currentContcatId);
            currentContact.SetEmail(newContact.Email);
            currentContact.SetPostcode(newContact.Postcode);
            currentContact.SetHomePhoneNumber(newContact.HomePhoneNumber);
            currentContact.SetWorkPhoneNumber(newContact.WorkPhoneNumber);
            currentContact.SetMobilePhoneNumber(newContact.MobilePhoneNumber);
        }

        /// <summary>
        /// Удалить контакт
        /// </summary>
        /// <param name="id">Идентификатор контакта</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var deletedContact = await GetAsync(id);

            _domainContext.Set<Contact>().Remove(deletedContact);
        }
    }
}