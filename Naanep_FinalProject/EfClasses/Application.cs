using System;
using System.Collections.Generic;

namespace DataLayer.EfClasses
{
    public class Application
    {
        public string PlacementId { get; set; }
        public string PlacementStatus { get; set; }
        public double TotalWorkedHours { get; set; }

        public string CandidateNumber { get; set; }
        public Candidate CandidateLink { get; set; }

        public string OpeningNumber { get; set; }
        public Opening OpeningLink { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}