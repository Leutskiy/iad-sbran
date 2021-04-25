using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Services.Contracts
{
    public interface IPassportService
    {
        Passport Add(PassportDto addedPassport);
    }
}