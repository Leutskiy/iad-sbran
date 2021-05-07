using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sbran.WebApp.Controllers
{
	/// <summary>
	/// Контроллер контактных данных по сотруднику
	/// </summary>
	[ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IListOfScientistRepository _listOfScientistRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IDepartureRepository _departureRepository;
        private readonly IAppendixRepository _appendixRepository;
        private readonly DomainContext _domainContext;

        public ReportController(
            IReportRepository reportRepository,
            IInvitationRepository invitationRepository,
            IDepartureRepository departureRepository,
            IAppendixRepository appendixRepository,
            IListOfScientistRepository listOfScientistRepository,
            DomainContext domainContext)
        {
            _reportRepository = reportRepository;
            _invitationRepository = invitationRepository;
            _departureRepository = departureRepository;
            _appendixRepository = appendixRepository;
            _domainContext = domainContext;
            _listOfScientistRepository = listOfScientistRepository;
        }

        [HttpPost]
        [Route("{reportId:guid?}")]
        public async Task<ReportDto> CreateOrUpdate(Guid? reportId, ReportDto createdReportData)
        {
            if (reportId == null)
            {
                var report = _reportRepository.Add(createdReportData);

                switch (createdReportData.ReportType)
                {
                    case Domain.Enums.ReportType.Departure:
                        await _departureRepository.SetReport(report.Id, createdReportData.ParentId!.Value);
                        report.ReportType = Domain.Enums.ReportType.Departure;
                        break;

                    case Domain.Enums.ReportType.Invition:
                        await _invitationRepository.SetReport(report.Id, createdReportData.ParentId!.Value);
                        report.ReportType = Domain.Enums.ReportType.Invition;
                        break;
                }
                if (createdReportData.ListOfScientists?.Count > 0)
                {
                    foreach (var temp in createdReportData.ListOfScientists)
                    {
                        if (temp != null)
                        {
                            temp.ReportId = report.Id;
                            _listOfScientistRepository.Add(temp);
                        }
                    }
                }
                if (createdReportData.Appendix?.Count > 0)
                {
                    foreach (var temp in createdReportData.Appendix)
                    {
                        if (temp != null)
                        {
                            temp.ReportId = report.Id;
                            _appendixRepository.Add(temp);
                        }
                    }
                }

                await _domainContext.SaveChangesAsync();

                return await GetById(report.Id);
            }

            await _reportRepository.UpdateAsync(reportId.Value, createdReportData);

            if (createdReportData.ListOfScientists?.Count > 0)
            {
                await _listOfScientistRepository.DeleteAllAsync(reportId.Value);
                foreach (var temp in createdReportData.ListOfScientists)
                {
                    if (temp != null)
                    {
                            temp.ReportId = reportId.Value;
                            _listOfScientistRepository.Add(temp);
                    }
                }
            }

            if (createdReportData.Appendix?.Count > 0)
            {
                await _appendixRepository.DeleteAllAsync(reportId.Value);
                foreach (var temp in createdReportData.Appendix)
                {
                    if (temp != null)
                    {
                            temp.ReportId = reportId.Value;
                            _appendixRepository.Add(temp);
                    }
                }
            }

            await _domainContext.SaveChangesAsync();
            return await GetById(reportId.Value);
        }


        [HttpGet]
        [Route("{reportId:guid}")]
        public async Task<ReportDto> GetById(Guid reportId)
        {            
            var report = await _reportRepository.GetAsync(reportId);
            var reportDto = new ReportDto
            {
                Findings = report?.Findings ?? "",
                Id = report?.Id,
                ForeignInterest = report?.ForeignInterest ?? "",
                Suggestion = report?.Suggestion ?? "",
                MainPart = report?.MainPart ?? "",
                Status = report?.Status,
                Appendix = report?.Appendices
                .Select(e => new AppendixDto()
                {
                    FileBinary = e.FileBinary!,
                    FileName = e.FileName!,
                    Id = e.Id,
                    ReportId = e.ReportId
                }).ToList(),
                ReportType = report!.ReportType,
                ListOfScientists = report?.ListOfScientists
                .Select(e => new ListOfScientistDto()
                {
                    AcademicDegree = e.AcademicDegree,
                    Email = e.Email,
                    FullName = e.FullName,
                    PhoneNumber = e.PhoneNumber,
                    Position = e.Position,
                    ReportId = e.ReportId,
                    Id = e.Id,
                    Type = e.Type,
                    WorkPlace = e.WorkPlace
                }).ToList(),
            };
            return reportDto;
        }

        [HttpGet]
        [Route("{reportId:guid}/agree")]
        public async Task<IActionResult> Agree(Guid reportId)
        {
            await _reportRepository.SetStatus(reportId);
            await _domainContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getFile/{appendixId:guid}")]
        public async Task<IActionResult> GetFile(Guid appendixId)
        {
            var file = await _appendixRepository.GetAsync(appendixId);
            return File(file.FileBinary, "application/octet-stream", file.FileName);
        }

    }
}