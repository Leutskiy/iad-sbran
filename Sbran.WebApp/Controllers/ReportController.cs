using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
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
        private readonly IInvitationRepository _invitationRepository;
        private readonly IDepartureRepository _departureRepository;
        private readonly IAppendixRepository _appendixRepository;
        private readonly DomainContext _domainContext;

        public ReportController(
            IReportRepository reportRepository,
            IInvitationRepository invitationRepository,
            IDepartureRepository departureRepository,
            IAppendixRepository appendixRepository,
            DomainContext domainContext)
        {
            _reportRepository = reportRepository;
            _invitationRepository = invitationRepository;
            _departureRepository = departureRepository;
            _appendixRepository = appendixRepository;
            _domainContext = domainContext;
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
                        await _departureRepository.SetReport(report.Id, createdReportData.ParentId.Value);
                        report.ReportType = Domain.Enums.ReportType.Departure;
                        break;

                    case Domain.Enums.ReportType.Invition:
                        await _invitationRepository.SetReport(report.Id, createdReportData.ParentId.Value);
                        report.ReportType = Domain.Enums.ReportType.Invition;
                        break;
                }
                if ((createdReportData.FileName != null & createdReportData.FileBinary != null) || createdReportData.Description != null)
                {
                    var appendixDto = new AppendixDto
                    {
                        Description = createdReportData.Description,
                        FileBinary = createdReportData.FileBinary,
                        FileName = createdReportData.FileName,
                        ReportId = report.Id
                    };
                    _appendixRepository.Add(appendixDto);
                }


                await _domainContext.SaveChangesAsync();

                return await GetById(report.Id);
            }

            await _reportRepository.UpdateAsync(reportId.Value, createdReportData);
            if ((createdReportData.FileName != null & createdReportData.FileBinary != null) || createdReportData.Description != null)
            {
                var appendixDto = new AppendixDto
                {
                    Description = createdReportData.Description,
                    FileBinary = createdReportData.FileBinary,
                    FileName = createdReportData.FileName,
                    ReportId = reportId.Value
                };
                _appendixRepository.UpdateAsync(createdReportData.AppendixId.Value,appendixDto);
            }
            await _domainContext.SaveChangesAsync();
            return await GetById(reportId.Value);
        }


        [HttpGet]
        [Route("{reportId:guid}")]
        public async Task<ReportDto> GetById(Guid reportId)
        {
            var report = await _reportRepository.GetAsync(reportId);
            var appendix = await _appendixRepository.GetByReportIdAsync(reportId);
            var reportDto = new ReportDto
            {
                Findings = report?.Findings ?? "",
                Id = report?.Id,
                ForeignInterest = report?.ForeignInterest ?? "",
                Suggestion = report?.Suggestion ?? "",
                MainPart = report?.MainPart ?? "",
                Description = appendix?.Description ?? "",
                FileBinary = appendix?.FileBinary ?? null,
                FileName = appendix?.FileName ?? "",
                AppendixId = appendix?.Id,
            };
            return reportDto;
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