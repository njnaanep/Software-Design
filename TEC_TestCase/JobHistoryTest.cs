using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TEC_TestCase
{
    [TestFixture]
    public class JobHistoryTest
    {
        [Test]
        public static void GenerateJobHistory()
        {
            var histories =
                GetJobHistories(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\History.csv");
            
            using var context = new TecContext();

            foreach (var jobHistory in histories)
            {
                context.Add(jobHistory);
                context.SaveChanges();
            }
        }

        [Test]
        public void AddJobHistory()
        {
            using var context = new TecContext();
            var newHistory = new JobHistory
            {
                WorkedFrom = DateTime.Now.AddYears(-1),
                WorkedTo = DateTime.Now.AddYears(1),
                CandidateNumber = "CDT-000001",
                CompanyId = "CMP-000001"
            };

            context.JobHistories.Add(newHistory);
            context.SaveChanges();
        }

        private static IList<JobHistory> GetJobHistories(string location)
        {
            var sr = new StreamReader(location);

            var historyList = new List<JobHistory>();

            var foreignKey = new ForeignKeys();
            string s = sr.ReadLine();

            while (s != null)
            {
                var split = s.Split(',');
                var newHistory = new JobHistory
                {
                    CandidateNumber = foreignKey.GetRandomCandidate(),
                    CompanyId = foreignKey.GetRandomCompany(),
                    WorkedHours = Convert.ToDouble(split[0]),
                    WorkedFrom = Convert.ToDateTime(split[1])
                };
                if (split[2]!="") 
                    newHistory.WorkedTo = Convert.ToDateTime(split[2]);


                historyList.Add(newHistory);

                s = sr.ReadLine();
            }

            return historyList;
        }

        [Test]
        public void ViewJobHistoryFromCSV()
        {
            var histories =
                GetJobHistories(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\History.csv");
            foreach (var jobHistory in histories)
            {
                Console.WriteLine($"{jobHistory.CandidateNumber}\t{jobHistory.CompanyId}\t{jobHistory.WorkedHours}\t{jobHistory.WorkedFrom}\t{jobHistory.WorkedTo}");
            }
        }

        [Test]
        public void JobHistory_ViewJobHistory_DisplayCandidateAndCompany()
        {
            using var context = new TecContext();

            var histories = context.JobHistories.Where(c => c.IsDeleted == false)
                .Include(c => c.CandidateLink)
                .Include(c => c.CompanyLink);

            Console.WriteLine($"{"History ID",-15}{"Candidate",-25}{"Company",-20}");

            foreach (var jobHistory in histories)
            {
                //Console.WriteLine($"{jobHistory.HistoryId,-15}{jobHistory.CandidateLink.CandidateName,-25}{jobHistory.CompanyLink.CompanyName,-20}");
            }
        }
    }
}