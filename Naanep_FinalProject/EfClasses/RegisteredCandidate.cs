using System;

namespace DataLayer.EfClasses
{
    public class RegisteredCandidate
    {
        public string RegistrationId { get; set; }

        public DateTime RegisteredDate { get; set; }

        public string CandidateNumber { get; set; }
        public Candidate CandidateLink { get; set; }

        public string SessionId { get; set; }
        public TrainingSession TrainingSessionLink { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}