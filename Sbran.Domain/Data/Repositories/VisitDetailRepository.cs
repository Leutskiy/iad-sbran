using Sbran.Domain.Data.Adapters;
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
	/// Репозиторий деталей поездки
	/// </summary>
	public sealed class VisitDetailRepository : IVisitDetailRepository
    {
		private readonly DomainContext _context;

		public VisitDetailRepository(DomainContext databaseContext)
        {
            _context = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        /// <summary>
        /// Обновить детали визита
        /// </summary>
        /// <param name="visitDetailId">Идентификатор деталей визита</param>
        /// <param name="visitDetailDto">Информация по деталям визита</param>
        /// <returns>Идентификатор деталей визита</returns>
        public async Task<Guid> UpdateAsync(Guid visitDetailId, VisitDetailDto visitDetailDto)
        {
            Contract.Argument.IsNotEmptyGuid(visitDetailId, nameof(visitDetailId));

            var visitDetail = await GetAsync(visitDetailId);

            visitDetail.SetGoal(visitDetailDto.Goal);
            visitDetail.SetCountry(visitDetailDto.Country);
            visitDetail.SetVisitingPoints(visitDetailDto.VisitingPoints);
            visitDetail.SetPeriodDays(visitDetailDto.PeriodInDays);
            visitDetail.SetArrivalDate(visitDetailDto.ArrivalDate);
            visitDetail.SetDepartureDate(visitDetailDto.DepartureDate);
            visitDetail.SetVisaType(visitDetailDto.VisaType);
            visitDetail.SetVisaCity(visitDetailDto.VisaCity);
            visitDetail.SetVisaCountry(visitDetailDto.VisaCountry);
            visitDetail.SetVisaMultiplicity(visitDetailDto.VisaMultiplicity);

            return visitDetail.Id;
        }

        /// <summary>
        /// Получить все детали визита
        /// </summary>
        /// <returns>Детали визита</returns>
        public async Task<IEnumerable<VisitDetail>> GetAllAsync()
        {
            var visitDetails = await _context.Set<VisitDetail>().ToArrayAsync();

            return visitDetails;
        }

        /// <summary>
        /// Получить детали визита по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор деталей визита</param>
        /// <returns>Детали визита</returns>
        public async Task<VisitDetail> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }

            var visitDetail = await _context.Set<VisitDetail>().FindAsync(id);

            if (visitDetail == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return visitDetail;
        }

        /// <summary>
        /// Создать детали визита
        /// </summary>
        /// <param name="goal">Цель визита</param>
        /// <param name="country">Страна визита</param>
        /// <param name="visitingPoints">Пункты посещения</param>
        /// <param name="period">Период пребывания</param>
        /// <param name="arrivalDate">Дата прибытия</param>
        /// <param name="departureDate">Дата отъезда</param>
        /// <param name="visaType">Тип визы</param>
        /// <param name="visaCity">Город получения визы</param>
        /// <param name="visaCountry">Страна получения визы</param>
        /// <param name="visaMultiplicity">Кратность визы</param>
        /// <returns>Идентификатор деталей визита</returns>
        public VisitDetail Create()
        {
            var createdVisitDetail = new VisitDetail();          

            // TODO: унести в репозиторий
            _context.Set<VisitDetail>().Add(createdVisitDetail);

            return createdVisitDetail;
        }

        /// <summary>
        /// Добавить детали визита
        /// </summary>
        /// <param name="addedVisitDetail">DTO деталей визита</param>
        /// <returns>Детали визита</returns>
        public VisitDetail Add(VisitDetailDto addedVisitDetail)
        {
            var createdVisitDetail = Create();

            createdVisitDetail.SetGoal(addedVisitDetail.Goal);
            createdVisitDetail.SetCountry(addedVisitDetail.Country);
            createdVisitDetail.SetVisitingPoints(addedVisitDetail.VisitingPoints);
            createdVisitDetail.SetPeriodDays(addedVisitDetail.PeriodInDays);
            createdVisitDetail.SetArrivalDate(addedVisitDetail.ArrivalDate);
            createdVisitDetail.SetDepartureDate(addedVisitDetail.DepartureDate);
            createdVisitDetail.SetVisaType(addedVisitDetail.VisaType);
            createdVisitDetail.SetVisaCity(addedVisitDetail.VisaCity);
            createdVisitDetail.SetVisaCountry(addedVisitDetail.VisaCountry);
            createdVisitDetail.SetVisaMultiplicity(addedVisitDetail.VisaMultiplicity);

            return createdVisitDetail;
        }

        /// <summary>
        /// Удалить детали визита
        /// </summary>
        /// <param name="id">Идентификатор детелей визита</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var deletedVisitDetail = await GetAsync(id);

            _context.Set<VisitDetail>().Remove(deletedVisitDetail);
        }
    }
}