using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Sbran.CQS.Read;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Models;
using Sbran.Domain.Entities;
using System.Collections.Generic;
using Sbran.Domain.Data.Repositories.Contracts;

namespace Sbran.WebApp.Controllers
{
	///TODO: лучше назвать как ProfileEmployee
	/// <summary>
	/// Контроллер профиля
	/// </summary>
	[ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IReadCommand<ProfileResult> _profileReadCommand;
        private readonly EmployeeReadCommand _employeeReadCommand;
        private readonly IInvitationRepository _invitationRepositore;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IDepartureRepository _departureRepository;

        private readonly ProfileWriteCommand _profileWriteCommand;

		public ProfileController(
			IReadCommand<ProfileResult> profileReadCommand,
			EmployeeReadCommand employeeReadCommand,
			ProfileWriteCommand profileWriteCommand,
			IInvitationRepository invitationRepositore,
			IMembershipRepository membershipRepository,
			IPublicationRepository publicationRepository,
			IDepartureRepository departureRepository)
		{
			_profileReadCommand = profileReadCommand;
			_employeeReadCommand = employeeReadCommand;
			_profileWriteCommand = profileWriteCommand;
			_invitationRepositore = invitationRepositore;
			_membershipRepository = membershipRepository;
			_publicationRepository = publicationRepository;
			_departureRepository = departureRepository;
		}

		[HttpGet]
        [Route("{profileId:guid}/employee/{employeeId:guid}")]
        public async Task<IActionResult> GetById(Guid profileId, Guid employeeId)
        {
            var profileResult = await _profileReadCommand.ExecuteAsync(profileId);
            var employeeResult = await _employeeReadCommand.ExecuteAsync(employeeId);

            var isAdmin = User.IsInRole(UserRoles.Admin);

            var invitesCount = isAdmin ? await _invitationRepositore.Total() : employeeResult.Invitations.Count;
            var publishsCount = isAdmin ? await _publicationRepository.Total() : employeeResult.Publications.Count;
            var departuresCount = isAdmin ? await _departureRepository.Total() : employeeResult.Departures.Count;
            var membershipsCount = isAdmin ? await _membershipRepository.Total() : employeeResult.Memberships.Count;

            var userInfo = new UserInfoResult
            {
                Profile = profileResult,
                ShortName = employeeResult.Organization?.ShortName,
                AcademicDegree = employeeResult.AcademicDegree,
                AcademicRank = employeeResult.AcademicRank,
                Education = employeeResult.Education,
                Fio = employeeResult.Passport?.ToFio(),
                Email = employeeResult.Contact?.Email,
                /// TODO: реализовать получение и заполнение факсов + база
                Fax = null,
                MobilePhoneNumber = employeeResult.Contact?.MobilePhoneNumber,
                WorkPlace = employeeResult.WorkPlace,
                Position = employeeResult.Position,
                InvitesCount = invitesCount,
                DeparturesCount = departuresCount,
                MembershipsCount = membershipsCount,
                PublishsCount = publishsCount,
                ScientificInterests = ConvertScientificInterestsListForString(employeeResult.ScientificInterests),
                Memberships = ConvertMembershipsListForString(employeeResult.Memberships)
            };

            var objectJson = JsonSerializer.SerializeToUtf8Bytes(userInfo, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var mediaType = new MediaTypeHeaderValue("application/octet-stream");
            var result = new FileContentResult(objectJson, mediaType);

            return result;
        }

        [HttpPost]
        [Route("{profileId:guid}")]
        public Task UpdateAsync(Guid profileId, ProfileDto profileDto)
        {
            return _profileWriteCommand.UpdateAsync(profileId, profileDto);
        }

        private string ConvertScientificInterestsListForString(List<ScientificInterests> list)
        {
            var result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += i != list.Count - 1 ? list[i].NameOfScientificInterests + ", " : list[i].NameOfScientificInterests;
            }
            return result;
        }

        private string ConvertMembershipsListForString(List<Membership> list)
        {
            var result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += i != list.Count - 1 ? list[i].NameOfCompany + ", " : list[i].NameOfCompany;
            }
            return result;
        }
    }
}
