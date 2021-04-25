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
    public sealed class DepartureRepository : IDepartureRepository
    {
        private readonly DomainContext _domainContext;

        public DepartureRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public Departure Add(DepartureDto addedDeparture)
        {
            var departure = new Departure()
            {
                BasicOfDeparture = addedDeparture.BasicOfDeparture,
                CityOfBusiness = addedDeparture.CityOfBusiness,
                DateEnd = addedDeparture.DateEnd,
                DateStart = addedDeparture.DateStart,
                EmployeeId = addedDeparture.EmployeeId,
                HostOrganization = addedDeparture.HostOrganization,
                JustificationOfTheBusiness = addedDeparture.JustificationOfTheBusiness,
                PlaceOfResidence = addedDeparture.PlaceOfResidence,
                PurposeOfTheTrip = addedDeparture.PurposeOfTheTrip,
                SendingCountry = addedDeparture.SendingCountry,
                SourceOfFinancing = addedDeparture.SourceOfFinancing,
                DepartureStatus = Enums.DepartureStatus.NonAgreement,
            };
            _domainContext.Departures.Add(departure);
            return departure;
        }

        public async Task Agree(Guid id)
        {
            var departure = await _domainContext.Departures.FirstOrDefaultAsync(e => e.Id == id);
            if (departure != null)
            {
                departure.DepartureStatus = Enums.DepartureStatus.Agreement;
                _domainContext.Update(departure);
                await _domainContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var departure = await _domainContext.Departures.FirstOrDefaultAsync(e => e.Id == id);
            if (departure != null)
            {
                _domainContext.Remove(departure);
            }
        }

        public async Task<List<Departure>> GetAllAsync() => await _domainContext.Departures.ToListAsync();

        public async Task<Departure> GetAsync(Guid id) => await _domainContext.Departures.FirstOrDefaultAsync(e => e.Id == id);

        public async Task SetReport(Guid id, Guid parentId)
        {
            var departure = await _domainContext.Departures.FirstOrDefaultAsync(e => e.Id == parentId);
            if (departure != null)
            {
                departure.ReportId = id;
                _domainContext.Update(departure);
                await _domainContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Guid currentDepartureId, DepartureDto newDeparture)
        {
            var departure = await _domainContext.Departures.FirstOrDefaultAsync(e => e.Id == currentDepartureId);
            if (departure != null)
            {
                departure.HostOrganization = newDeparture.HostOrganization;
                departure.JustificationOfTheBusiness = newDeparture.JustificationOfTheBusiness;
                departure.BasicOfDeparture = newDeparture.BasicOfDeparture;
                departure.CityOfBusiness = newDeparture.CityOfBusiness;
                departure.DateEnd = newDeparture.DateEnd;
                departure.DateStart = newDeparture.DateStart;
                departure.EmployeeId = newDeparture.EmployeeId;
                departure.PlaceOfResidence = newDeparture.PlaceOfResidence;
                departure.PurposeOfTheTrip = newDeparture.PurposeOfTheTrip;
                departure.SendingCountry = newDeparture.SendingCountry;
                departure.SourceOfFinancing = newDeparture.SourceOfFinancing;
                _domainContext.Departures.Update(departure);
            }
        }
    }
}
