using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
    public sealed class ReportRepository : IReportRepository
    {
        private readonly DomainContext _domainContext;

        public ReportRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public Report Add(ReportDto addedReport)
        {
            var report = new Report()
            {
                MainPart = addedReport.MainPart,
                Findings = addedReport.Findings,
                ForeignInterest = addedReport.ForeignInterest,
                Suggestion = addedReport.Suggestion

            };
            _domainContext.Reports.Add(report);
            return report;
        }

        public async Task DeleteAsync(Guid id)
        {
            var report = await _domainContext.Reports.FirstOrDefaultAsync(e => e.Id == id);
            if (report != null)
            {
                _domainContext.Remove(report);
            }
        }

        public async Task<List<Report>> GetAllAsync() => await _domainContext.Reports.ToListAsync();

        public async Task<Report> GetAsync(Guid id) => await _domainContext.Reports.FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Guid currentReportId, ReportDto newReport)
        {
            var report = await _domainContext.Reports.FirstOrDefaultAsync(e => e.Id == currentReportId);
            if (report != null)
            {
                report.MainPart = newReport.MainPart;
                report.Findings = newReport.Findings;
                report.ForeignInterest = newReport.ForeignInterest;
                report.Suggestion = newReport.Suggestion;
            }
        }
    }
}
