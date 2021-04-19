using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class JobHistoryService
    {
        private TecContext _context;

        public JobHistoryService(TecContext context) => _context = context;

        public static IQueryable<JobHistory> GetJobHistories() =>
            new TecContext().JobHistories
                .Include(c => c.CandidateLink)
                .Include(c => c.CompanyLink);

        public IQueryable<JobHistory> SearchJobHistories(string searchString)
            => GetJobHistories().Where(c => c.IsDeleted == false &&
                                          (c.CandidateLink.CandidateFullName.Contains(searchString)
                                           || c.CompanyLink.CompanyName.Contains(searchString)));

        public static IQueryable<JobHistory> GetCandidateJobHistory(string candidateNum) 
            => GetJobHistories().Where(c => c.CandidateNumber == candidateNum);

        public static IQueryable<JobHistory> GetCompanyJobHistory(string companyId) 
            => GetJobHistories().Where(c => c.CompanyId == companyId);



        public void AddJobHistory(JobHistory jobHistory)
        {
            _context.JobHistories.Add(jobHistory);
            _context.SaveChanges();
        }

        public void UpdateJobHistory(JobHistory history)
        {
            var jobHistory = _context.JobHistories.Find(history.HistoryId);
            jobHistory.CandidateNumber = history.CandidateNumber;
            jobHistory.CompanyId = history.CompanyId;
            jobHistory.WorkedHours = history.WorkedHours;
            jobHistory.WorkedFrom = history.WorkedFrom;
            jobHistory.WorkedTo = history.WorkedTo;

            _context.SaveChanges();
        }
        public void DeleteJobHistory(string historyId)
        {
            _context.JobHistories.Find(historyId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}