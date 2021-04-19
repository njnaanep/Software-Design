using System;
using System.Collections.Generic;
using System.IO;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    [TestFixture]
    public class RegistrationTest
    {
        [Test]
        public static void GenerateRegistrations()
        {
            var registeredCandidates =
                GetRegisteredCandidates(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\RegisteredCandidate.CSV");

            using var context = new TecContext();

            foreach (var registeredCandidate in registeredCandidates)
            {
                context.Add(registeredCandidate);
                context.SaveChanges();
            }

            //context.Add(registeredCandidates[0]);
            //context.SaveChanges();
        }

        [Test]
        public void DeleteSession()
        {
            using var context = new TecContext();

            context.Remove(context.RegisteredCandidates.Find("REG-000001"));
            context.SaveChanges();
        }

        private static IList<RegisteredCandidate> GetRegisteredCandidates(string location)
        {
            var sr = new StreamReader(location);

            var registeredCandidates = new List<RegisteredCandidate>();
            string s = sr.ReadLine();

            while (s != null)
            {
                var registeredCandidate = new RegisteredCandidate
                {
                    RegisteredDate = Convert.ToDateTime(s),
                    CandidateNumber = new ForeignKeys().GetRandomCandidate(),
                    SessionId= new ForeignKeys().GetRandomSessions()
                };

                registeredCandidates.Add(registeredCandidate);

                s = sr.ReadLine();
            }

            return registeredCandidates;
        }

    }
}