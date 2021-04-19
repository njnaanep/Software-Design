using System;
using System.Linq;
using DataLayer.EfClasses;
using TEC_Interface.ViewModel.Candidate;

namespace TEC_Interface.Services
{
    public class CandidateService
    {
        private TecContext _context;

        public CandidateService(TecContext context) => _context = context;

        public IQueryable<Candidate> GetCandidate() => 
            _context.Candidates.Where(c => c.IsDeleted == false);

        public IQueryable<Candidate> SearchCandidate(string search) =>
            _context.Candidates.Where(c => c.IsDeleted == false 
                                           && c.CandidateName.Contains(search));

        public void DeleteCandidate(string candidateNumber)
        {
            _context.Candidates.Find(candidateNumber).IsDeleted = true;
            _context.SaveChanges();
        }

        public void AddCandidate(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            _context.SaveChanges();
        }

        public void UpdateCandidate(CandidateViewModel candidate)
        {
            var editCandidate = _context.Candidates.Find(candidate.CandidateNumber);
            editCandidate.CandidateName = candidate.Name;
            editCandidate.CandidateGender = candidate.Gender;
            editCandidate.CandidateAddress = candidate.Address;
            editCandidate.CandidateBirthDate = Convert.ToDateTime(candidate.Birthdate);
            editCandidate.CandidateContactNumber = candidate.ContactNumber;
            editCandidate.CandidateEmail = candidate.Email;

            _context.SaveChanges();
        }
    }
}
