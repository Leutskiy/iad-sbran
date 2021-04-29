using System;

namespace Sbran.Domain.Models
{
	public sealed class ScientificInterestsDto
    {/// <summary>
     /// наименование научных интересов 
     /// </summary>
        public string? NameOfScientificInterests { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
