using System;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ServiceLayer
{
    public class ApplicationService
    {
        private TecContext _context;

        public ApplicationService(TecContext context) => _context = context;

        public IQueryable<Application> GetApplications() =>
            _context.Applications.Where(c => c.IsDeleted == false)
                .Include(c => c.CandidateLink)
                .Include(c => c.OpeningLink);

        public IQueryable<Application> GetCandidateApplication(string candidateNum) =>
        _context.Applications.Where(c => c.IsDeleted == false 
                                         && c.CandidateNumber == candidateNum)
            .Include(c => c.OpeningLink)
                .ThenInclude(c => c.CompanyLink);

        public IQueryable<Application> GetPendingApplication() =>
        _context.Applications.Where(c => c.IsDeleted == false && c.ApplicationStatus == "Pending")
            .Include(c => c.CandidateLink)
            .Include(c => c.OpeningLink);


        public IQueryable<Application> GetOpeningApplication(string openingNum) =>
            _context.Applications.Where(c => c.IsDeleted == false
                                             && c.OpeningNumber == openingNum);

        public void AddApplication(Application application)
        {
            _context.Applications.Add(application);
            _context.SaveChanges();
        }

        public void UpdateApplication(string applicationId, string candidateNumber, string openingNumber, DateTime applicationDate)
        {
            var application = _context.Applications.Find(applicationId);
            application.CandidateNumber = candidateNumber;
            application.OpeningNumber = openingNumber;
            application.ApplicationDate= applicationDate;

            _context.SaveChanges();
        }

        public void DeleteApplication(string applicationId)
        {
            var placements = _context.Placements.Where(c => c.ApplicationId == applicationId);

            foreach (var placement in placements) 
                placement.IsDeleted = true;

            _context.Applications.Find(applicationId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}