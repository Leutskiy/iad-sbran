using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Sbran.CQS.Read;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;

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

        private readonly ProfileWriteCommand _profileWriteCommand;

        public ProfileController(
            IReadCommand<ProfileResult> profileReadCommand,
            EmployeeReadCommand employeeReadCommand,
            ProfileWriteCommand profileWriteCommand)
        {
            _profileReadCommand = profileReadCommand;
            _employeeReadCommand = employeeReadCommand;
            _profileWriteCommand = profileWriteCommand;
        }

        [HttpGet]
        [Route("{profileId:guid}/employee/{employeeId:guid}")]
        public async Task<IActionResult> GetById(Guid profileId, Guid employeeId)
        {
            var profileResult = await _profileReadCommand.ExecuteAsync(profileId);
            var employeeResult = await _employeeReadCommand.ExecuteAsync(employeeId);

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
                Position = employeeResult.Position
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
    }
}
