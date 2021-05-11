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
	/// Команда чтения сотрудников
	/// </summary>
	public sealed class EmployeeReadCommand /*: IReadCommand<EmployeeResult>*/
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IReadCommand<ContactResult> _contactReadCommand;
        private readonly IReadCommand<PassportResult> _passportReadCommand;
        private readonly IReadCommand<OrganizationResult> _organizationReadCommand;
        private readonly IReadCommand<StateRegistrationResult> _stateRegistrationReadCommand;

		public EmployeeReadCommand(
			IEmployeeRepository employeeRepository,
			IReadCommand<ContactResult> contactReadCommand,
			IReadCommand<PassportResult> passportReadCommand,
			IReadCommand<OrganizationResult> organizationReadCommand,
			IReadCommand<StateRegistrationResult> stateRegistrationReadCommand)
		{
			_employeeRepository = employeeRepository;
			_contactReadCommand = contactReadCommand;
			_passportReadCommand = passportReadCommand;
			_organizationReadCommand = organizationReadCommand;
			_stateRegistrationReadCommand = stateRegistrationReadCommand;
		}

		/// <summary>
		/// Выполнить команду на чтение информации по сотруднику
		/// </summary>
		/// <param name="employeeId">Идентификатор иностранца</param>
		/// <returns>Информация по сотруднику</returns>
		public async Task<EmployeeResult> ExecuteAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);

            var contactResult = employee.ContactId.HasValue ? await _contactReadCommand.ExecuteAsync(employee.ContactId.Value) : default;
            var passportResult = employee.PassportId.HasValue ? await _passportReadCommand.ExecuteAsync(employee.PassportId.Value) : default;
            var organizationResult = employee.OrganizationId.HasValue ? await _organizationReadCommand.ExecuteAsync(employee.OrganizationId.Value) : default;
            var stateRegistrationResult = employee.StateRegistrationId.HasValue ? await _stateRegistrationReadCommand.ExecuteAsync(employee.StateRegistrationId.Value) : default;

            return DomainEntityConverter.ConvertToResultForEditEmployeeData(
                employee: employee,
                contactResult: contactResult,
                passportResult: passportResult,
                organizationResult: organizationResult,
                stateRegistrationResult: stateRegistrationResult);
        }

        /// <summary>
        /// Выполнить команду на чтение информации по сотруднику
        /// </summary>
        /// <param name="employeeId">Идентификатор иностранца</param>
        /// <returns>Информация по сотруднику</returns>
        public async Task<EmployeeResult> ExecuteForProfileAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);

            var contactResult = employee.ContactId.HasValue ? await _contactReadCommand.ExecuteAsync(employee.ContactId.Value) : default;
            var passportResult = employee.PassportId.HasValue ? await _passportReadCommand.ExecuteAsync(employee.PassportId.Value) : default;
            var organizationResult = employee.OrganizationId.HasValue ? await _organizationReadCommand.ExecuteAsync(employee.OrganizationId.Value) : default;
            var stateRegistrationResult = employee.StateRegistrationId.HasValue ? await _stateRegistrationReadCommand.ExecuteAsync(employee.StateRegistrationId.Value) : default;

            return DomainEntityConverter.ConvertToResult(
                employee: employee,
                contactResult: contactResult,
                passportResult: passportResult,
                organizationResult: organizationResult,
                stateRegistrationResult: stateRegistrationResult);
        }

        /// <summary>
        /// Выполнить команду на чтение информации по сотруднику
        /// </summary>
        /// <param name="employeeId">Идентификатор иностранца</param>
        /// <returns>Информация по сотруднику</returns>
        /*public async Task<IEnumerable<InvitationResult>> ExecuteAllAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);

            var invitationResults = new List<InvitationResult>();
            var invitations = employee.Invitations;

            foreach (var invitation in invitations)
            {
                var invitationResult = await _invitationReadCommand.ExecuteAsync(invitation.Id);
                invitationResults.Add(invitationResult);
            }

            return invitationResults;
        }*/

        public Task<IEnumerable<EmployeeResult>> ExecuteAsync()
		{
			throw new NotImplementedException();
		}
	}
}