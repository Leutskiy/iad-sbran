using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
	/// <summary>
	/// Репозиторий паспортных данных
	/// </summary>
	public sealed class PassportRepository : IPassportRepository
    {
        private readonly DomainContext _domainContext;

        public PassportRepository(DomainContext databaseContext)
        {
            _domainContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        /// <summary>
        /// Получить все паспортные данные
        /// </summary>
        /// <returns>Паспортные данные</returns>
        public Task<List<Passport>> GetAllAsync()
        {
            return _domainContext.Set<Passport>().ToListAsync();
        }

        /// <summary>
        /// Получить паспортные данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор паспортных данных</param>
        /// <returns>Паспортные данные</returns>
        public async Task<Passport> GetAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var passport = await _domainContext.Set<Passport>().FindAsync(id);

            if (passport == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return passport;
        }

        /// <summary>
        /// Создать паспортные данные
        /// </summary>
        /// <returns>Идентификатор паспортных данных</returns>
        public Passport Create()
        {
            var createdPassport = new Passport();

            _domainContext.Set<Passport>().Add(createdPassport);

            return createdPassport;
        }

        /// <summary>
        /// Добавить паспортные данные
        /// </summary>
        /// <param name="addedPassport">DTO добавляемого паспорта</param>
        public Passport Add(PassportDto addedPassport)
        {
            var createdPassport = Create();

            createdPassport.SetNameRus(addedPassport.NameRus);
            createdPassport.SetNameEng(addedPassport.NameEng);
            createdPassport.SetSurnameRus(addedPassport.SurnameRus);
            createdPassport.SetSurnameEng(addedPassport.SurnameEng);
            createdPassport.SetPatronymicNameRus(addedPassport.PatronymicNameRus);
            createdPassport.SetPatronymicNameEng(addedPassport.PatronymicNameEng);

            createdPassport.SetBirthPlace(addedPassport.BirthPlace);
            createdPassport.SetBirthCountry(addedPassport.BirthCountry);
            createdPassport.SetDepartmentCode(addedPassport.DepartmentCode);
            createdPassport.SetCitizenship(addedPassport.Citizenship);

            createdPassport.SetIdentityDocument(addedPassport.IdentityDocument);
            createdPassport.SetResidence(addedPassport.Residence);
            createdPassport.SetResidenceCountry(addedPassport.ResidenceCountry);
            createdPassport.SetResidenceRegion(addedPassport.ResidenceRegion);
            createdPassport.SetIssuePlace(addedPassport.IssuePlace);
            createdPassport.SetBirthDate(addedPassport.BirthDate);
            createdPassport.SetIssueDate(addedPassport.IssueDate);
            createdPassport.SetGender(addedPassport.Gender);

            return createdPassport;
        }

        public async Task UpdateAsync(Guid currentPassportId, PassportDto newPassport)
        {
            var currentPassport = await GetAsync(currentPassportId);

            currentPassport.SetNameRus(newPassport.NameRus);
            currentPassport.SetNameEng(newPassport.NameEng);
            currentPassport.SetSurnameRus(newPassport.SurnameRus);
            currentPassport.SetSurnameEng(newPassport.SurnameEng);
            currentPassport.SetPatronymicNameRus(newPassport.PatronymicNameRus);
            currentPassport.SetPatronymicNameEng(newPassport.PatronymicNameEng);

            currentPassport.SetBirthPlace(newPassport.BirthPlace);
            currentPassport.SetBirthCountry(newPassport.BirthCountry);
            currentPassport.SetDepartmentCode(newPassport.DepartmentCode);
            currentPassport.SetCitizenship(newPassport.Citizenship);

            currentPassport.SetIdentityDocument(newPassport.IdentityDocument);
            currentPassport.SetResidence(newPassport.Residence);
            currentPassport.SetResidenceCountry(newPassport.ResidenceCountry);
            currentPassport.SetResidenceRegion(newPassport.ResidenceRegion);
            currentPassport.SetIssuePlace(newPassport.IssuePlace);
            currentPassport.SetBirthDate(newPassport.BirthDate);
            currentPassport.SetIssueDate(newPassport.IssueDate);
            currentPassport.SetGender(newPassport.Gender);
        }

        /// <summary>
        /// Удалить паспортные данные
        /// </summary>
        /// <param name="id">Идентификатор паспортных данных</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var deletedPassport = await GetAsync(id);

            _domainContext.Set<Passport>().Remove(deletedPassport);
        }
    }
}