using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class RegistrationService
    {
        private TecContext _context;

        public RegistrationService(TecContext context) => _context = context;

        public IQueryable<RegisteredCandidate> GetRegisteredCandidates =>
        _context.RegisteredCandidates.Where(c => c.IsDeleted == false)
            .Include(c => c.TrainingSessionLink)
            .ThenInclude(c => c.CourseLink)
            .Include(c => c.CandidateLink);


        public IQueryable<RegisteredCandidate> GetCandidateRegistration(string candidateNum) =>
            _context.RegisteredCandidates.Where(c => c.IsDeleted == false
                                                     && c.CandidateNumber == candidateNum)
                .Include(c => c.TrainingSessionLink)
                .ThenInclude(c => c.CourseLink);


        public IQueryable<RegisteredCandidate> GetTrainingRegistration(string sessionId) =>
            _context.RegisteredCandidates.Where(c => c.IsDeleted == false
                                                     && c.SessionId == sessionId)
                .Include(c => c.CandidateLink);

        public void AddRegistration(RegisteredCandidate registration)
        {
            _context.RegisteredCandidates.Add(registration);
            _context.SaveChanges();
        }

        public void UpdateRegistration(string regId, string candidateId, string sessionId)
        {
            var registration= _context.RegisteredCandidates.Find(regId);
            registration.CandidateNumber = candidateId;
            registration.SessionId = sessionId;

            _context.SaveChanges();
        }

        public void DeleteRegistration(string regId)
        {
            _context.RegisteredCandidates.Find(regId).IsDeleted = true;
            _context.SaveChanges();
        }


    }
}