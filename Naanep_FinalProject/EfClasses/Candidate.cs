using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace DataLayer.EfClasses
{
    public class Candidate
    {
        private int _age;
        private string _candidateFullname;

        public string CandidateNumber { get; set; }

        public string CandidateFirstName { get; set; }
        public string? CandidateMiddleName { get; set; }
        public string CandidateLastName { get; set; }

        public string CandidateFullName
        {
            get
            {
                _candidateFullname = CandidateMiddleName == null ? 
                    $"{CandidateFirstName} {CandidateLastName}" : 
                    $"{CandidateFirstName} {CandidateMiddleName[0]}. {CandidateLastName}";

                return _candidateFullname;
            }
            set => _candidateFullname = value;
        }

        public string CandidateGender { get; set; }
        public string CandidateAddress { get; set; }    
        public DateTime CandidateBirthDate { get; set; }
        public string CandidateContactNumber { get; set; }
        public string CandidateEmail { get; set; }  
                          
        public int CandidateAge
        {
            get
            {
                _age = DateTime.Today.Year - CandidateBirthDate.Year;
                if (CandidateBirthDate.DayOfYear > DateTime.Today.DayOfYear) _age--;
                return _age;
            }
            internal set => _age = value;
        }

        public ICollection<Certification> Certifications { get; set; }
        public ICollection<RegisteredCandidate> RegisteredCandidates { get; set; }
        public ICollection<Placement> Placements { get; set; }
        public ICollection<JobHistory> JobHistories { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}