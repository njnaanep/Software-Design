using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ServiceLayer
{
    public class CandidateService
    {
        private TecContext _context;

        public CandidateService(TecContext context) => _context = context;

        public IQueryable<Candidate> GetCandidate() 
            => _context.Candidates.Where(c => !c.IsDeleted); 
        
        public IQueryable<Candidate> GetQualifiedCandidate(string qualificationId)
            => _context.Candidates.Where(candidate => _context.Certifications
                .Where(certification => certification.QualificationId == qualificationId)
                .Select(certification => certification.CandidateNumber)
                .Contains(candidate.CandidateNumber));

        public IQueryable<Candidate> GetUnqualifiedCandidate(string qualificationId)
        {
            var qualifiedCandidates = _context.Certifications
                .Where(c => !c.IsDeleted && c.QualificationId == qualificationId)
                .Select(c => c.CandidateNumber).ToList();

            List<Candidate> unqualifiedCandidates = new List<Candidate>();

            foreach (var candidate in GetCandidate())
            {
                if (!qualifiedCandidates.Any(c => c.Equals(candidate.CandidateNumber)))
                    unqualifiedCandidates.Add(candidate);
            }

            return unqualifiedCandidates.AsQueryable();
        }

        public IQueryable<Candidate> GetUnenrolledCandidates(string sessionId)
        {
            var enrolledCandidate = _context.RegisteredCandidates.Where(c => !c.IsDeleted &&
                    c.SessionId.Equals(sessionId)).Select(c => c.CandidateNumber).ToList();

            var unenrolledCandidates = new List<Candidate>();

            foreach (var candidate in GetCandidate())
                if (!enrolledCandidate.Any(c => c.Equals(candidate.CandidateNumber)))
                    unenrolledCandidates.Add(candidate);

            return unenrolledCandidates.AsQueryable();
        }

        public IQueryable<Candidate> SearchCandidateQuery(IQueryable<Candidate> query, string searchText) 
            => query.Where(c => c.CandidateFullName.Contains(searchText) || c.CandidateAddress.Contains(searchText));

        public IQueryable<Candidate> SearchCandidate(string search)
            => SearchCandidateQuery(GetCandidate(), search);

        public IQueryable<Candidate> SearchQualifiedCandidate(string qualificationId, string search)
            => SearchCandidateQuery(GetQualifiedCandidate(qualificationId), search);

        public IQueryable<Candidate> SearchUnenrolledCandidates(string sessionId, string search)
            => SearchCandidateQuery(GetUnenrolledCandidates(sessionId), search);

        public IQueryable<Candidate> SearchUnqualifiedCandidate(string qualificationId, string searchString) 
            => SearchCandidateQuery(GetUnqualifiedCandidate(qualificationId), searchString);

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

        public void UpdateCandidate(Candidate candidate)
        {
            var editCandidate = _context.Candidates.Find(candidate.CandidateNumber);
            editCandidate.CandidateFirstName = candidate.CandidateFirstName;
            editCandidate.CandidateMiddleName = candidate.CandidateMiddleName;
            editCandidate.CandidateLastName = candidate.CandidateLastName;
            editCandidate.CandidateGender = candidate.CandidateGender;
            editCandidate.CandidateAddress = candidate.CandidateAddress;
            editCandidate.CandidateBirthDate = candidate.CandidateBirthDate;
            editCandidate.CandidateContactNumber = candidate.CandidateContactNumber;
            editCandidate.CandidateEmail = candidate.CandidateEmail;


            _context.SaveChanges();
        }
    }
}
