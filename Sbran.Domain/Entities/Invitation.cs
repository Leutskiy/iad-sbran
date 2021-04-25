using Sbran.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Приглашение
    /// </summary>
    public sealed class Invitation
    {
        private Invitation()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор иностранца
        /// </summary>
        public Guid AlienId { get; private set; }

        /// <summary>
        /// Идентификатор отчета
        /// </summary>
        public Guid? ReportId { get; set; }

        /// <summary>
        /// Идентификатор иностранца
        /// </summary>
        public Guid EmployeeId { get; private set; }

        /// <summary>
        /// Идентификатор деталей поездки по приглашению
        /// </summary>
        public Guid? VisitDetailId { get; private set; }

        /// <summary>
        /// Иностранец
        /// </summary>
		public Alien Alien { get; private set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
		public Employee Employee { get; private set; }

        /// <summary>
        /// Детали поездки
        /// </summary>
        public VisitDetail? VisitDetail { get; private set; }

        /// <summary>
        /// Отчет
        /// </summary>
        public Report? Report { get; private set; }

        /// <summary>
        /// Сопровождение
        /// </summary>
        public List<ForeignParticipant> ForeignParticipants { get; private set; }

        /// <summary>
        /// Статус
        /// </summary>
        public InvitationStatus Status { get; private set; }

        #region TODO: убрать в shadow properties

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreatedDate { get; private set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTimeOffset UpdateDate { get; private set; }

        #endregion

        /// <summary>
        /// Создать приглашение
        /// </summary>
        /// <param name="alien">Иностранец, которого приглашают</param>
        /// <param name="employee">Сотрудник, который приглашает</param>
        public Invitation(Alien alien, Employee employee) : this()
        {
            Alien = alien;
            Employee = employee;
            AlienId = alien.Id;
            EmployeeId = employee.Id;
            Status = InvitationStatus.Creating;
            ForeignParticipants = new List<ForeignParticipant>();

            UpdateDate = DateTimeOffset.Now;
            CreatedDate = DateTimeOffset.Now;
        }

        /// <summary>
        /// Задать детали визита
        /// </summary>
        /// <param name="visitDetailId">Детали визита</param>
        public void SetVisitDetail(VisitDetail visitDetail)
        {
            if (VisitDetailId == visitDetail.Id)
            {
                return;
            }

            VisitDetailId = visitDetail.Id;
            VisitDetail = visitDetail;
        }

        /// <summary>
        /// Задать статус приглашения
        /// </summary>
        /// <param name="invitationStatus">Статус приглашения</param>
        public void SetInvitationStatus(InvitationStatus invitationStatus)
        {
            if (Status == invitationStatus)
            {
                return;
            }

            Status = invitationStatus;
        }

        /// <summary>
        /// Добавить сопровождающего иностранца-участника
        /// </summary>
        /// <param name="foreignParticipant">Иностранный участник</param>
        public void AddForeignParticipant(ForeignParticipant foreignParticipant)
        {
            if (ForeignParticipants.Any(fp => fp.Id == foreignParticipant.Id))
            {
                return;
            }

            ForeignParticipants.Add(foreignParticipant);
        }

        /// <summary>
        /// Добавить сопровождающих иностранцев-участников
        /// </summary>
        /// <param name="foreignParticipant">Инострацы участники</param>
        public void AddForeignParticipants(params ForeignParticipant[] foreignParticipants)
        {
            foreach (var foreignParticipant in foreignParticipants)
            {
                if (!ForeignParticipants.Any(fp => fp.Id == foreignParticipant.Id))
                {
                    ForeignParticipants.Add(foreignParticipant);
                }
            }
        }

        /// <summary>
        /// Задать иностранное сопровождения
        /// </summary>
        /// <param name="foreignParticipants">Иностранное сопровождение</param>
        public void SetForeignParticipants(IEnumerable<ForeignParticipant> foreignParticipants)
        {
            AddForeignParticipants(foreignParticipants.ToArray());
        }
    }
}