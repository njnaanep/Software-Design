using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DataLayer.EfClasses
{
    public class Opening
    {
        public string OpeningNumber { get; set; }

        public DateTime StartingDate { get; set; }
        public DateTime AnticipatedEndDate { get; set; }
        public double HourlyPay{ get; set; }

        public string QualificationId { get; set; }
        public Qualification QualificationLink { get; set; }

        public string CompanyId { get; set; }
        public Company CompanyLink{ get; set; }

        public ICollection<Placement> Placements { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}