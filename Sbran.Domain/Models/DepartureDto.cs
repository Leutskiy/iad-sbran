using System;

namespace Sbran.Domain.Models
{
	public sealed class DepartureDto
    {
        public string? SendingCountry { get; set; }
        public string? CityOfBusiness { get; set; }
        public string? SourceOfFinancing { get; set; }
        public string? BasicOfDeparture { get; set; }
        public string? HostOrganization { get; set; }
        public string? PlaceOfResidence { get; set; }
        public string? PurposeOfTheTrip { get; set; }
        public string? Status { get; set; }
        public string? JustificationOfTheBusiness { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
