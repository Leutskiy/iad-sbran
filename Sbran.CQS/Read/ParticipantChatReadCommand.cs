using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Repositories.Contracts;

namespace Sbran.CQS.Read
{
	public sealed class ParticipantChatReadCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IEmployeeRepository _employeeRepository;

		public ParticipantChatReadCommand(IUserRepository userRepository, IProfileRepository profileRepository, IEmployeeRepository employeeRepository)
		{
			_userRepository = userRepository;
			_profileRepository = profileRepository;
			_employeeRepository = employeeRepository;
		}

		/// <summary>
		/// Получить Идентификатор иностранца
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <returns>Идентификатор иностранца</returns>
		public async Task<IEnumerable<ParticipantChatResult>> GetParticipants()
        {
			var employeesWithPassport = await _employeeRepository.GetAllWithPassportAsync();

			var userProfileCollection = await _userRepository.GetUsersFull();

			var participants = employeesWithPassport.Join(
				inner: userProfileCollection,
				outerKeySelector: empl => empl.UserId,
				innerKeySelector: user => user.Id,
				resultSelector: (empl, userprof) => new ParticipantChatResult
				{
					UserId = empl.UserId,
					UserFullName = empl.Passport?.SurnameRus + " " + empl.Passport?.NameRus + " " + empl.Passport?.PatronymicNameRus,
					Avatar = userprof.Profile.Photo,
					LastMessage = "Test",
					LastMesssageDate = DateTimeOffset.Now
				}) ?? new List<ParticipantChatResult>();

			return participants;

		}
    }
}
