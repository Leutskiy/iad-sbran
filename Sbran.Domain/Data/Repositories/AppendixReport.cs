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
    public sealed class AppendixRepository : IAppendixRepository
    {
        private readonly DomainContext _domainContext;

        public AppendixRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public Appendix Add(AppendixDto addedAppendix)
        {
            var appendix = new Appendix()
            {
                FileBinary = addedAppendix.FileBinary,
                FileName = addedAppendix.FileName,
                ReportId = addedAppendix.ReportId.Value
            };
            _domainContext.Appendixs.Add(appendix);
            return appendix;
        }

        public async Task DeleteAllAsync(Guid id)
        {
            var appendix = _domainContext.Appendixs.Where(e => e.ReportId == id);
            if (appendix != null)
            {
                _domainContext.RemoveRange(appendix);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var appendix = await _domainContext.Appendixs.FirstOrDefaultAsync(e => e.Id == id);
            if (appendix != null)
            {
                _domainContext.Remove(appendix);
            }
        }

        public async Task<List<Appendix>> GetAllAsync() => await _domainContext.Appendixs.ToListAsync();

        public async Task<Appendix> GetAsync(Guid id) => await _domainContext.Appendixs.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Appendix> GetByReportIdAsync(Guid id) => await _domainContext.Appendixs.FirstOrDefaultAsync(e => e.ReportId == id);

        public async Task UpdateAsync(Guid currentAppendixId, AppendixDto newAppendix)
        {
            var appendix = await _domainContext.Appendixs.FirstOrDefaultAsync(e => e.Id == currentAppendixId);
            if (appendix != null)
            {
                appendix.FileBinary = newAppendix.FileBinary;
                appendix.FileName = newAppendix.FileName;
                appendix.ReportId = newAppendix.ReportId.Value;
            }
        }
    }
}
