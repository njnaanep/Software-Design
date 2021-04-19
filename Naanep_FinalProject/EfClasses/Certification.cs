using System;
using System.Security.Authentication.ExtendedProtection;

namespace DataLayer.EfClasses
{
    public class Certification
    {
        public string CertificationId { get; set; }
        public DateTime CertificationDate { get; set; }

        public string CandidateNumber { get; set; }
        public Candidate CandidateLink { get; set; }

        public string QualificationId { get; set; }
        public Qualification QualificationLink { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}