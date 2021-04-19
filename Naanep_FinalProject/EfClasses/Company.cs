using System.Collections;
using System.Collections.Generic;

namespace DataLayer.EfClasses
{
    public class Company
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }

        public ICollection<JobHistory> JobHistories { get; set; }
        public ICollection<Opening> Openings { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}