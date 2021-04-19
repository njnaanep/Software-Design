using System;

namespace DataLayer.EfClasses
{
    public class JobHistory
    {
        public string HistoryId { get; set; }
        public double? WorkedHours { get; set; }

        public DateTime WorkedFrom { get; set; }
        public DateTime? WorkedTo { get; set; }

        public string CandidateNumber { get; set; }
        public Candidate CandidateLink { get; set; }

        public string CompanyId { get; set; }
        public Company CompanyLink { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}