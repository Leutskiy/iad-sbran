using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
