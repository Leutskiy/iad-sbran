using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Services.Contracts
{
    public interface IVisitDetailService
    {
        VisitDetail Add(VisitDetailDto addedVisitDetail);
    }
}