using System;
using System.Collections.Generic;
using System.IO;
using DataLayer.EfClasses;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace TEC_TestCase
{
    public class ApplicationTest
    {
        private IList<Application> GetApplications(string location)
        {
            var sr = new StreamReader(location);

            var applicationsList = new List<Application>();
            var foreignKey = new ForeignKeys();
            string s = sr.ReadLine();

            while (s != null)
            {
                var application = new Application
                {
                    ApplicationDate = Convert.ToDateTime(s),
                    CandidateNumber =  foreignKey.GetRandomCandidate(),
                    OpeningNumber = foreignKey.GetRandomOpening(),
                };
                application.ApplicationStatus = 
                    applicationsList.Count % 3 == 0 ? "Pending" : "Qualified";

                applicationsList.Add(application);

                s = sr.ReadLine();
            }

            return applicationsList;
        }

        [Test]
        public void GenerateApplications()
        {
            var applications =
                GetApplications(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Application.CSV");

            using var context = new TecContext();
            foreach (var application in applications)
            {
                context.Add(application);
                context.SaveChanges();
            }
        }

        [Test]
        public void DeleteApplication()
        {
            using var context = new TecContext();

            context.Remove(context.Applications.Find("APL-000002"));
            context.SaveChanges();
        }
    }
}