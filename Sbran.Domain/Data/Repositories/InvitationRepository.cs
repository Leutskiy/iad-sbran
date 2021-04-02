using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using Sbran.Domain.Services.Contracts;
using Sbran.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
    /// <summary>
    /// Репозиторий приглашений
    /// </summary>
    public sealed class InvitationRepository : IInvitationRepository
    {
        private readonly IAlienRepository _alienRepository;
        private readonly IVisitDetailRepository _visitDetailRepository;
        private readonly IForeignParticipantRepository _foreignParticipantRepository;
        private readonly DomainContext _domainContext;

        public InvitationRepository(
            IAlienRepository alienRepository,
            IVisitDetailRepository visitDetailRepository,
            IForeignParticipantRepository foreignParticipantRepository,
            DomainContext domainContext)
        {
            _alienRepository = alienRepository;
            _visitDetailRepository = visitDetailRepository;
            _foreignParticipantRepository = foreignParticipantRepository;
            _domainContext = domainContext;
        }

        /// <summary>
        /// Получить все приглашения
        /// </summary>
        /// <returns>Приглашения</returns>
        public Task<List<Invitation>> GetAllAsync(Guid employeeId)
        {
            return _domainContext.Invitations.ToListAsync();
        }

        // TODO: добавить метод, который будет получать пути для вытаскивания через include
        /// <summary>
        /// Получить приглашение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор приглашения</param>
        /// <returns>Приглашение</returns>
        public async Task<Invitation> GetAsync(Guid id)
        {
            var invitation = await _domainContext.Invitations.Include(inv => inv.Report).FirstOrDefaultAsync(inv => inv.Id == id);
            if (invitation == null)
            {
                throw new Exception($"Сущность не найдена для id: {id}");
            }

            return invitation;
        }

        /// <summary>
        ///  Создать приглашение
        /// </summary>
        /// <param name="alienId">Иностранец</param>
        /// <param name="employeeId">Сотрудник</param>
        /// <param name="visitDetailId">Детали визита</param>
        /// <param name="foreignParticipants">Сопровождение</param>
        /// <param name="createdDate">Дата создания</param>
        /// <param name="updateDate">Дата изменения</param>
        /// <param name="invitationStatus">Статус приглашения</param>
        /// <returns>Идентификатор приглашения</returns>
        public Invitation Create(Alien alien, Employee employee)
        {
            var createdInvitation = new Invitation(alien: alien, employee: employee);

            _domainContext.Invitations.Add(createdInvitation);

            return createdInvitation;
        }

        /// <summary>
        /// Удалить приглашение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор приглашения</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var deletedInvitation = await GetAsync(id);

            _domainContext.Invitations.Remove(deletedInvitation);
        }

        /// <summary>
        /// Добавить приглашение
        /// </summary>
        /// <param name="employee">Сотрудник, который организует приглашение</param>
        /// <param name="addedInvitation">Новое приглашение</param>
        /// <returns></returns>
        public Invitation Add(Employee employee, InvitationDto addedInvitation)
        {
            // TODO: сейчас приглашаемый иностранец всегда должен быть задан при создании приглашения
            var alien = _alienRepository.Add(addedInvitation.Alien);
            var invitation = Create(alien: alien, employee: employee);

            // TODO: сейчас сопровождающие инстранца могут быть не заданы при создании приглашения
            if (addedInvitation.ForeignParticipants != null)
            {
                var foreignParticipants = new List<ForeignParticipant>();
                foreach (var foreignParticipantDto in addedInvitation.ForeignParticipants)
                {
                    var foreignParticipant = _foreignParticipantRepository.Add(foreignParticipantDto);
                    foreignParticipants.Add(foreignParticipant);
                }

                invitation.SetForeignParticipants(foreignParticipants);
            }

            // TODO: сейчас детали визита могут быть не заданы при создании приглашения
            if (addedInvitation.VisitDetail != null)
            {
                var visitDetail = _visitDetailRepository.Add(addedInvitation.VisitDetail);
                invitation.SetVisitDetail(visitDetail);
            }

            return invitation;
        }

        public Task<List<Invitation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SetReport(Guid id, Guid parentId)
        {
            var invitation = await _domainContext.Invitations.FirstOrDefaultAsync(e => e.Id == parentId);
            if (invitation != null)
            {
                invitation.ReportId = id;
                _domainContext.Update(invitation);
                await _domainContext.SaveChangesAsync();
            }
        }

        public async Task Agree(Guid id)
        {
            var invitation = await _domainContext.Invitations.FirstOrDefaultAsync(e => e.Id == id);
            if (invitation != null)
            {
                invitation.SetInvitationStatus(Enums.InvitationStatus.Agreement);
                _domainContext.Update(invitation);
                await _domainContext.SaveChangesAsync();
            }
        }
    }
}