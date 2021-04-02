using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories.Contracts
{
    public interface IAppendixRepository
    {
        Task DeleteAsync(Guid id);
        Task DeleteAllAsync(Guid id);

        Task<List<Appendix>> GetAllAsync();

        Task<Appendix> GetAsync(Guid id);
        Task<Appendix> GetByReportIdAsync(Guid id);

        Appendix Add(AppendixDto addedAppendix);

        Task UpdateAsync(Guid currentAppendixId, AppendixDto newAppendix);
    }
}