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
    public sealed class ListOfScientistRepository : IListOfScientistRepository
    {
        private readonly DomainContext _domainContext;

        public ListOfScientistRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public ListOfScientist Add(ListOfScientistDto addedListOfScientist)
        {
            var listOfScientist = new ListOfScientist()
            {
                Email = addedListOfScientist.Email,
                FullName = addedListOfScientist.FullName,
                PhoneNumber = addedListOfScientist.PhoneNumber,
                Position = addedListOfScientist.Position,
                AcademicDegree = addedListOfScientist.AcademicDegree,
                Type = addedListOfScientist.Type,
                ReportId = addedListOfScientist.ReportId,
                WorkPlace = addedListOfScientist.WorkPlace,
            };
            _domainContext.ListOfScientists.Add(listOfScientist);
            return listOfScientist;
        }

        public async Task DeleteAllAsync(Guid id)
        {
            var listOfScientist = _domainContext.ListOfScientists.Where(e => e.ReportId == id);
            if (listOfScientist != null)
            {
                _domainContext.RemoveRange(listOfScientist);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var listOfScientist = await _domainContext.ListOfScientists.FirstOrDefaultAsync(e => e.Id == id);
            if (listOfScientist != null)
            {
                _domainContext.Remove(listOfScientist);
            }
        }

        public async Task<List<ListOfScientist>> GetAllAsync() => await _domainContext.ListOfScientists.ToListAsync();

        public async Task<ListOfScientist> GetAsync(Guid id) => await _domainContext.ListOfScientists.FirstOrDefaultAsync(e => e.Id == id);

        public async Task SetReport(Guid id, Guid parentId)
        {
            var listOfScientist = await _domainContext.ListOfScientists.FirstOrDefaultAsync(e => e.Id == parentId);
            if (listOfScientist != null)
            {
                listOfScientist.ReportId = id;
                _domainContext.Update(listOfScientist);
                await _domainContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Guid currentListOfScientistId, ListOfScientistDto newListOfScientist)
        {
            var listOfScientist = await _domainContext.ListOfScientists.FirstOrDefaultAsync(e => e.Id == currentListOfScientistId);
            if (listOfScientist != null)
            {
                listOfScientist.Email = newListOfScientist.Email;
                listOfScientist.PhoneNumber = newListOfScientist.PhoneNumber;
                listOfScientist.FullName = newListOfScientist.FullName;
                listOfScientist.Position = newListOfScientist.Position;
                listOfScientist.ReportId = newListOfScientist.ReportId;
                listOfScientist.Type = newListOfScientist.Type;
                listOfScientist.AcademicDegree = newListOfScientist.AcademicDegree;
                listOfScientist.WorkPlace = newListOfScientist.WorkPlace;
                _domainContext.ListOfScientists.Update(listOfScientist);
            }
        }
    }
}
