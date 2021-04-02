using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IReportRepository
    {
        Task DeleteAsync(Guid id);

        Task<List<Report>> GetAllAsync();

        Task<Report> GetAsync(Guid id);

        Report Add(ReportDto addedReport);

        Task UpdateAsync(Guid currentReportId, ReportDto newReport);
        Task SetStatus(Guid Id);
    }
}