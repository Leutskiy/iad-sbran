using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Data.Repositories
{
    public interface IVisitDetailRepository
    {
        Task<Guid> UpdateAsync(Guid visitDetailId, VisitDetailDto visitDetailDto);

        VisitDetail Create();

        VisitDetail Add(VisitDetailDto addedVisitDetail);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<VisitDetail>> GetAllAsync();

        Task<VisitDetail> GetAsync(Guid id);
    }
}