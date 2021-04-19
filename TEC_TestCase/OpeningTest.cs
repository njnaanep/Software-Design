using System;
using System.Collections.Generic;
using System.IO;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    [TestFixture]
    public class OpeningTest
    {
        [Test]
        public static void GenerateOpenings()
        {
            var openings =
                GetOpenings(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Opening.csv");

            using var context = new TecContext();

            foreach (var opening in openings)
            {
                context.Add(opening);
                context.SaveChanges();
            }
        }

        private static IList<Opening> GetOpenings(string location)
        {
            var sr = new StreamReader(location);

            var openings = new List<Opening>();

            var foreignKey = new ForeignKeys();
            string s = sr.ReadLine();

            while (s != null)
            {
                var split = s.Split(',');

                var newOpening = new Opening
                {
                    StartingDate = Convert.ToDateTime(split[0]),
                    AnticipatedEndDate = Convert.ToDateTime(split[1]),
                    HourlyPay = Convert.ToDouble(split[2]),
                    QualificationId = foreignKey.GetRandomQualification(),
                    CompanyId = foreignKey.GetRandomCompany()
                };

                openings.Add(newOpening);

                s = sr.ReadLine();
            }

            return openings;
        }

        [Test]
        public void AddOpening_Manual()
        {
            var openings =
                GetOpenings(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Opening.csv");

            
            using var context = new TecContext();

            context.Add(openings[0]);
            context.SaveChanges();

            Console.WriteLine(openings[0].OpeningNumber);
        }

        [Test]
        public void Opening_ManualDelete()
        {
            using var context = new TecContext();

            context.Remove(context.Openings.Find("OPN-000001"));
            context.SaveChanges();
        }

        [Test]
        public void AddOpening_Loaded()
        {
            var openings =
                GetOpenings(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Opening.csv");

            using var context = new TecContext();

            foreach (var opening in openings)
            {
                context.Add(opening);
                context.SaveChanges();
            }
        }
    }
}