using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class RegistrationService
    {
        private TecContext _context;

        public RegistrationService(TecContext context) => _context = context;

        public static IQueryable<RegisteredCandidate> GetRegisteredCandidates =>
        new TecContext().RegisteredCandidates.Where(c => !c.IsDeleted)
            .Include(c => c.TrainingSessionLink)
            .ThenInclude(c => c.CourseLink)
            .Include(c => c.CandidateLink);


        public static IQueryable<RegisteredCandidate> GetCandidateRegistration(string candidateNum) =>
            GetRegisteredCandidates.Where(c => c.CandidateNumber == candidateNum);
        
        public static  IQueryable<RegisteredCandidate> GetTrainingRegistration(string sessionId)
            => GetRegisteredCandidates.Where(c => c.SessionId == sessionId);

        public void AddRegistration(RegisteredCandidate registration)
        {
            _context.RegisteredCandidates.Add(registration);
            _context.SaveChanges();
        }

        public void UpdateRegistration(RegisteredCandidate registration)
        {
            var updatedRegistration = _context.RegisteredCandidates.Find(registration.RegistrationId);
            updatedRegistration.CandidateNumber = registration.CandidateNumber;
            updatedRegistration.SessionId = registration.SessionId;
            updatedRegistration.RegisteredDate = registration.RegisteredDate;


            _context.SaveChanges();
        }
        public void DeleteRegistration(string regId)
        {
            _context.RegisteredCandidates.Find(regId).IsDeleted = true;
            _context.SaveChanges();
        }


    }
}