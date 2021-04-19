using System;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TEC_Interface.ViewModel.Candidate;

namespace ServiceLayer
{
    public class CandidateService
    {
        private TecContext _context;

        public CandidateService(TecContext context) => _context = context;

        public IQueryable<Candidate> GetCandidate()
        {
            return _context.Candidates.Where(c => c.IsDeleted == false);
        }

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

        public void UpdateCandidate(string candidateNumber, string name, string gender, string address, DateTime birthDate, string contactNum, string email)
        {
            var editCandidate = _context.Candidates.Find(candidateNumber);
            editCandidate.CandidateName = name;
            editCandidate.CandidateGender= gender;
            editCandidate.CandidateAddress= address;
            editCandidate.CandidateBirthDate= birthDate;
            editCandidate.CandidateContactNumber= contactNum;
            editCandidate.CandidateEmail= email;

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

            //_context.Candidates.Update(candidate);
            _context.SaveChanges();
        }


        public void UpdateCandidate(Candidate candidate)
        {
            var editCandidate = _context.Candidates.Find(candidate.CandidateNumber);
            editCandidate.CandidateName = candidate.CandidateName;
            editCandidate.CandidateGender = candidate.CandidateGender;
            editCandidate.CandidateAddress = candidate.CandidateAddress;
            editCandidate.CandidateBirthDate = candidate.CandidateBirthDate;
            editCandidate.CandidateContactNumber = candidate.CandidateContactNumber;
            editCandidate.CandidateEmail = candidate.CandidateEmail;

            //_context.Candidates.Update(candidate);
            _context.SaveChanges();
        }
    }
}
