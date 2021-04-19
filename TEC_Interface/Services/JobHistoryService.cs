using System;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace TEC_Interface.Services
{
    public class JobHistoryService
    {
        private TecContext _context;

        public JobHistoryService(TecContext context) => _context = context;

        public IQueryable<JobHistory> GetJobHistories() =>
            _context.JobHistories
                .Include(c => c.CandidateLink)
                .Include(c => c.CompanyLink);

        public IQueryable<JobHistory> GetCandidateJobHistory(string candidateNum) =>
            _context.JobHistories.Where(c => c.CandidateNumber == candidateNum)
                .Include(c => c.CompanyLink);

        public IQueryable<JobHistory> GetCompanyJobHistory(string companyId) =>
            _context.JobHistories.Where(c => c.CompanyId == companyId)
                .Include(c => c.CandidateLink);



        public void AddJobHistory(JobHistory jobHistory)
        {
            _context.JobHistories.Add(jobHistory);
            _context.SaveChanges();
        }
        public void UpdateJobHistory(string historyId, string candidateNum, string companyId, double workedHours, DateTime workedFrom,DateTime workedTo)
        {
            var jobHistory= _context.JobHistories.Find(historyId);
            jobHistory.CandidateNumber = candidateNum;
            jobHistory.CompanyId= companyId;
            jobHistory.WorkedHours= workedHours;
            jobHistory.WorkedFrom = workedFrom;
            jobHistory.WorkedTo = workedTo;
            
            _context.SaveChanges();
        }
        public void DeleteJobHistory(string historyId)
        {
            _context.JobHistories.Find(historyId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}