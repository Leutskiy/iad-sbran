using Sbran.Domain.Entities;
using Sbran.Domain.Models;

namespace Sbran.Domain.Services.Contracts
{
    public interface IContactService
    {
        Contact Add(ContactDto addedContact);
    }
}