using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.CQS.Read;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;

namespace Sbran.WebApp.Controllers
{
	// TODO: inviter --- invitee
	// TODO: перенести invitationId  в конец пути
	/// <summary>
	/// Контроллер приглашений
	/// </summary>
	[ApiController]
	[Authorize]
	[Route("api/v1")]
	public class InvitationController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly InvitationWriteCommand _invitationWriteCommand;
		private readonly IReadCommand<InvitationResult> _invitationReadCommand;

		public InvitationController(
			IEmployeeRepository employeeRepository,
			InvitationWriteCommand invitationWriteCommand,
			IReadCommand<InvitationResult> invitationReadCommand)
		{
			_employeeRepository = employeeRepository;
			_invitationReadCommand = invitationReadCommand;
			_invitationWriteCommand = invitationWriteCommand;
		}

		#region Invitation

		[HttpGet]
		[Route("invitation/{invitationId:guid}")]
		public async Task<InvitationResult> GetById(Guid invitationId)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return await _invitationReadCommand.ExecuteAsync(invitationId);
		}

		// TODO: унести в котроллер сотруднику
		[HttpGet]
		[Route("employee/{employeeId:guid}/invitations")]
		public async Task<IEnumerable<InvitationResult>> GetAll(Guid employeeId)
		{
			Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

			var employee = await _employeeRepository.GetAsync(employeeId);

			var invitationResults = new List<InvitationResult>();
			var invitations = employee.Invitations;

			foreach (var invitation in invitations)
			{
				var invitationResult = await _invitationReadCommand.ExecuteAsync(invitation.Id);
				invitationResults.Add(invitationResult);
			}

			return invitationResults;
		}

		// TODO: унести в контроллер сотруднику
		// TODO: переделать метод для добавления приглашения для рабочего; скоре всего надо передать сюда полное DTO вместо InviteeDto
		[HttpPost]
		[Route("employee/{employeeId:guid}/invitation")]
		public Task<Guid> AddInvitation(Guid employeeId, InvitationDto invitationDto)
		{
			Contract.Argument.IsNotEmptyGuid(employeeId, nameof(employeeId));

			return _invitationWriteCommand.AddForEmployeeAsync(employeeId, invitationDto);
		}

		#endregion

		#region VisitDetail

		[HttpPut]
		[Route("invitation/{invitationId:guid}/visitdetails")]
		public Task<Guid> UpdateVisitDetailAsync(Guid invitationId, VisitDetailDto visitDetailDto)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.AddOrUpdateVisitDetailAsync(invitationId, visitDetailDto);
		}

		#endregion

		#region Alien

		// TODO: надо понять, что делать с JobDto / AlienJobDto
		[HttpPut]
		[Route("invitation/{invitationId:guid}/alien/job")]
		public Task<Guid> UpdateAlienStateRegistrationAsync(Guid invitationId, AlienJobDto alienJobDto)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.UpdateAlienJobAsync(invitationId, alienJobDto);
		}

		[HttpPut]
		[Route("invitation/{invitationId:guid}/alien/passport")]
		public Task<Guid> UpdateAlienPassportAsync(Guid invitationId, PassportDto passportDto)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.AddOrUpdatePassportAsync(invitationId, passportDto);
		}

		[HttpPut]
		[Route("invitation/{invitationId:guid}/alien/contact")]
		public Task<Guid> UpdateAlienContactAsync(Guid invitationId, ContactDto contactDto)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.AddOrUpdateContactAsync(invitationId, contactDto);
		}

		/* TODO: надо подумать насчет работы для иностранца
		[HttpPut]
		[Route("invitation/{invitationId:guid}/alien/job")]
		public Task<Guid> UpdateAlienJobAsync(Guid invitationId, AlienJobDto jobDto)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.AddOrUpdateJobAsync(invitationId, jobDto);
		}
		*/

		[HttpPut]
		[Route("invitation/{invitationId:guid}/alien/organization")]
		public Task<Guid> UpdateAlienOrganizationAsync(Guid invitationId, OrganizationDto organizationDto)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.AddOrUpdateOrganizationAsync(invitationId, organizationDto);
		}

		[HttpPut]
		[Route("invitation/{invitationId:guid}/alien/stateregistration")]
		public Task<Guid> UpdateAlienStateRegistrationAsync(Guid invitationId, StateRegistrationDto stateRegistrationDto)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.AddOrUpdateStateRegistrationAsync(invitationId, stateRegistrationDto);
		}

		[HttpPut]
		[Route("invitation/{invitationId:guid}/alien/{stayaddress}")]
		public Task<Guid> UpdateAlienStayAddressAsync(Guid invitationId, string stayAddress)
		{
			Contract.Argument.IsNotEmptyGuid(invitationId, nameof(invitationId));

			return _invitationWriteCommand.UpdateAlienStayAddressAsync(invitationId, stayAddress);
		}

		#endregion
	}
}