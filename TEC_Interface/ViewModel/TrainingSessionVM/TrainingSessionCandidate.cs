using System;
using DataLayer.EfClasses;

namespace TEC_Interface.ViewModel.TrainingSession
{
    public class TrainingSessionCandidate
    {
        public string RegistrationId { get; set; }
        public string CandidateName { get; set; }
        public string RegistrationDate { get; set; }

        public TrainingSessionCandidate(string registrationId, string candidateName, string registrationDate)
        {
            RegistrationId = registrationId;
            CandidateName = candidateName;
            RegistrationDate = registrationDate;
        }

        public TrainingSessionCandidate(RegisteredCandidate registration)
        {
            RegistrationId = registration.RegistrationId;
            CandidateName = registration.CandidateLink.CandidateFullName;
            RegistrationDate = registration.RegisteredDate.ToShortDateString();
        }
    }
}