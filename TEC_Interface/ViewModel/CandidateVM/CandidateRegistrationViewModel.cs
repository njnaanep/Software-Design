using DataLayer.EfClasses;

namespace TEC_Interface.ViewModel.Candidate
{
    public class CandidateRegistrationViewModel
    {
        public string RegistrationId { get; set; }
        public string Course { get; set; }
        public string SessionId{ get; set; }
        public string RegistrationDate{ get; set; }

        public CandidateRegistrationViewModel(string course, string sessionId, string registrationDate, string registrationId)
        {
            Course = course;
            SessionId = sessionId;
            RegistrationDate = registrationDate;
            RegistrationId = registrationId;
        }

        public CandidateRegistrationViewModel(RegisteredCandidate registration)
        {
            Course = registration.TrainingSessionLink.CourseLink.CourseName;
            SessionId = registration.SessionId;
            RegistrationDate = registration.RegisteredDate.ToShortDateString();
            RegistrationId = registration.RegistrationId;
        }

    }
}