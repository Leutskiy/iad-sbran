using ICS.Application.Commands.Read;
using System;
using System.Threading.Tasks;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;

namespace Sbran.CQS.Read
{
	// TODO: Надо подумать: чтобы уменьшить число запросов в базу, делать запрос на приглашение и обновлять нужное поле или сущность
	/// <summary>
	/// Команда персистенса информации по сотруднику
	/// </summary>
	public sealed class InvitationWriteCommand
    {
        private readonly IAlienRepository _alienRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IVisitDetailRepository _visitDetailRepository;
        private readonly AlienWriteCommand _alienWriteCommand;
        private readonly DomainContext _domainContext;

		public InvitationWriteCommand(
			IAlienRepository alienRepository,
			IEmployeeRepository employeeRepository,
			IInvitationRepository invitationRepository,
			IVisitDetailRepository visitDetailRepository,
            AlienWriteCommand alienWriteCommand,
            DomainContext domainContext)
		{
			_alienRepository = alienRepository;
			_employeeRepository = employeeRepository;
			_invitationRepository = invitationRepository;
			_visitDetailRepository = visitDetailRepository;
			_alienWriteCommand = alienWriteCommand;
			_domainContext = domainContext;
		}

        /// <summary>
        /// Добавить приглашение для сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="invitationDto">DTO приглашения</param>
        /// <returns>Идентификатор нового приглашения</returns>
		public async Task<Guid> AddForEmployeeAsync(Guid employeeId, InvitationDto invitationDto)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);
            var invitation = _invitationRepository.Add(employee: employee, addedInvitation: invitationDto);

            await _domainContext.SaveChangesAsync();

            return invitation.Id;
        }

        // TODO: не используется
        public async Task<Guid> AddAlienAsync(InviteeDto alien)
        {
            var newAlien = _alienRepository.Add(alien);

            await _domainContext.SaveChangesAsync();

            return newAlien.Id;
        }

        /// <summary>
        /// Обновить детали визита
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="visitDetailDto">Информация по деталям визита</param>
        /// <returns></returns>
        public async Task<Guid> AddOrUpdateVisitDetailAsync(Guid invitationId, VisitDetailDto visitDetailDto)
        {
            var invitation = await _invitationRepository.GetAsync(invitationId);

            if (invitation.VisitDetailId.HasValue)
            {
                await _visitDetailRepository.UpdateAsync(invitation.VisitDetailId.Value, visitDetailDto);
            }
            else
            {
                var visitDetail = _visitDetailRepository.Add(visitDetailDto);
                invitation.SetVisitDetail(visitDetail);
            }

            await _domainContext.SaveChangesAsync();

            return invitation.VisitDetail!.Id;
        }

        /// <summary>
        /// Выполнить команду Добавить или Обновить паспортные данные приглашенного иностранца
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="passportDto">Паспортные данные для создания паспорта</param>
        /// <returns>Идентификатор паспорта</returns>
        public async Task<Guid> AddOrUpdatePassportAsync(Guid invitationId, PassportDto passportDto)
        {
            var invitation = await _invitationRepository.GetAsync(invitationId);
            var invitedAlienId = invitation.AlienId;

            var alienPassportId = await _alienWriteCommand.AddOrUpdatePassportAsync(invitedAlienId, passportDto);

            await _domainContext.SaveChangesAsync();

            return alienPassportId;
        }

        /// <summary>
        /// Выполнить команду Добавить или Обновить контактные данные иностранца
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="contactDto">Контактные данные</param>
        /// <returns>Идентификатор контакта</returns>
        public async Task<Guid> AddOrUpdateContactAsync(Guid invitationId, ContactDto contactDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            var invitation = await _invitationRepository.GetAsync(invitationId);
            var invitedAlienId = invitation.AlienId;

            var alienContactId = await _alienWriteCommand.AddOrUpdateContactAsync(invitedAlienId, contactDto);

            await _domainContext.SaveChangesAsync();

            return alienContactId;
        }

        /// <summary>
        /// Выполнить команду Добавить или Обновить данные по организации иностранца
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="organizationDto">Данные по организации</param>
        /// <returns>Идентификатор организации</returns>
        public async Task<Guid> AddOrUpdateOrganizationAsync(Guid invitationId, OrganizationDto organizationDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            var invitation = await _invitationRepository.GetAsync(invitationId);
            var invitedAlienId = invitation.AlienId;

            var alienOrganizationId = await _alienWriteCommand.AddOrUpdateOrganizationAsync(invitedAlienId, organizationDto);

            await _domainContext.SaveChangesAsync();

            return alienOrganizationId;
        }

        /// <summary>
        /// Выполнить команду Добавить или Обновить государственные регистрацонные данные иностранца
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="stateRegistrationDto">Государственные регистрацонные данные</param>
        /// <returns>Идентификатор государственных регистрационных данных</returns>
        public async Task<Guid> AddOrUpdateStateRegistrationAsync(Guid invitationId, StateRegistrationDto stateRegistrationDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            var invitation = await _invitationRepository.GetAsync(invitationId);
            var invitedAlienId = invitation.AlienId;

            var alienStateRegistrationId = await _alienWriteCommand.AddOrUpdateStateRegistrationAsync(invitedAlienId, stateRegistrationDto);

            await _domainContext.SaveChangesAsync();

            return alienStateRegistrationId;
        }

        /// <summary>
        /// Выполнить команду Обновить адрес пребывания иностранца
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="stayAddress">Адрес пребывания иностранца</param>
        public async Task<Guid> UpdateAlienStayAddressAsync(Guid invitationId, string stayAddress)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            var invitation = await _invitationRepository.GetAsync(invitationId);
            var invitedAlienId = invitation.AlienId;

            var alienId = await _alienWriteCommand.UpdateAlienStayAddressAsync(invitedAlienId, stayAddress);

            return alienId;
        }

        /// <summary>
        /// Выполнить команду Обновить информацию по работе иностранца
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <param name="jobDto">Данные по работе иностранца</param>
        public async Task<Guid> UpdateAlienJobAsync(Guid invitationId, AlienJobDto jobDto)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            var invitation = await _invitationRepository.GetAsync(invitationId);
            var alienId = await _alienWriteCommand.UpdateAlienJobAsync(invitation.AlienId, jobDto);
            await _domainContext.SaveChangesAsync();

            return alienId;
        }
    }
}