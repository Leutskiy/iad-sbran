using Sbran.CQS.Converters;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Shared.Contracts;

namespace Sbran.CQS.Read
{
	/// <summary>
	/// Команда чтения приглашений
	/// </summary>
	public sealed class InvitationReadCommand : IReadCommand<InvitationResult>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IReadCommand<AlienResult> _alienReadCommand;
        private readonly EmployeeReadCommand _employeeReadCommand;
        private readonly IReadCommand<VisitDetailResult> _visitDetailReadCommand;
        private readonly ForeignParticipantReadCommand _foreignParticipantReadCommand;

        public InvitationReadCommand(
            IInvitationRepository invitationRepository,
            IReadCommand<AlienResult> alienReadCommand,
            EmployeeReadCommand employeeReadCommand,
            IReadCommand<VisitDetailResult> visitDetailReadCommand,
            ForeignParticipantReadCommand foreignParticipantReadCommand)
        {
            Contract.Argument.IsNotNull(invitationRepository, nameof(invitationRepository));
            Contract.Argument.IsNotNull(alienReadCommand, nameof(alienReadCommand));
            Contract.Argument.IsNotNull(employeeReadCommand, nameof(employeeReadCommand));
            Contract.Argument.IsNotNull(visitDetailReadCommand, nameof(visitDetailReadCommand));
            Contract.Argument.IsNotNull(foreignParticipantReadCommand, nameof(foreignParticipantReadCommand));

            _invitationRepository = invitationRepository;
            _alienReadCommand = alienReadCommand;
            _employeeReadCommand = employeeReadCommand;
            _visitDetailReadCommand = visitDetailReadCommand;
            _foreignParticipantReadCommand = foreignParticipantReadCommand;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="invitationId">Идентификатор приглашения</param>
        /// <returns>Информация о приглашении</returns>
        public async Task<InvitationResult> ExecuteAsync(Guid invitationId)
        {
            Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

            var invitation = await _invitationRepository.GetAsync(invitationId);

            var alienResult = await _alienReadCommand.ExecuteAsync(invitation.AlienId);
            var employeeResult = await _employeeReadCommand.ExecuteAsync(invitation.EmployeeId);

            var visitDetailResult = invitation.VisitDetailId.HasValue
                ? await _visitDetailReadCommand.ExecuteAsync(invitation.VisitDetailId.Value)
                : default;

            var foreignParticipantCollectionResult = invitation.ForeignParticipants != null
                ? await _foreignParticipantReadCommand.ExecuteAsync(invitation.ForeignParticipants.Select(fp => fp.Id))
                : default;

            return DomainEntityConverter.ConvertToResult(
                invitation: invitation,
                alienResult: alienResult,
                employeeResult: employeeResult,
                visitDetailResult: visitDetailResult,
                foreignParticipantResultCollection: foreignParticipantCollectionResult);
        }

        public async Task<IEnumerable<InvitationResult>> ExecuteAsync()
        {
            //TODO: переделать
            var invitations = await _invitationRepository.GetAllAsync();

            var invitationResults = new List<InvitationResult>();

            foreach (var invitation in invitations)
            {
                var invitationResult = await ExecuteAsync(invitation.Id);
                invitationResults.Add(invitationResult);
            }

            return invitationResults;
        }
    }
}