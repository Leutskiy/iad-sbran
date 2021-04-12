using Sbran.Domain.Entities;
using System.Collections.Generic;

namespace Sbran.Domain.Models
{
    public sealed class MainDto
    {
        public List<News> News { get; set; }
        public List<EmployeeInfo> Employees { get; set; }
        public List<Vote> Votes { get; set; }
        public int CountEmployees { get; set; }
        public int CountOnlineEmployees { get; set; }
    }
}
