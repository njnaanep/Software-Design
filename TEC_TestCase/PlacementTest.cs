using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TEC_TestCase
{
    [TestFixture]
    public class PlacementTest
    {
        public static void GeneratePlacements_CSV()
        {
            var placements =
                GetPlacements(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Placement.csv");
            using var context = new TecContext();

            foreach (var placement in placements)
            {
                context.Add(placement);
                context.SaveChanges();
            }
        }

        public static void GeneratePlacements()
        {
            using var context = new TecContext();
            var foreignKey = new ForeignKeys();

            for (int i = 0; i < 200; i++)
            {
                var newPlacement = new Placement
                {
                    CandidateNumber = foreignKey.GetRandomCandidate(),
                    OpeningNumber = foreignKey.GetRandomOpening(),
                    PlacementStatus = i % 5 != 0 ? "Qualified" : "Pending"
                };

                context.Add(newPlacement);
                context.SaveChanges();
            } }

        private static IList<Placement> GetPlacements(string location)
        {
            var sr = new StreamReader(location);
            var placements = new List<Placement>();

            var foreignKey = new ForeignKeys();

            string s = sr.ReadLine();

            while (s != null)
            {
                var newPlacement = new Placement
                {
                    OpeningNumber = foreignKey.GetRandomOpening(),
                    CandidateNumber = foreignKey.GetRandomCandidate(),
                    //TotalWorkHours = Convert.ToDouble(s)
                };
                newPlacement.PlacementStatus = 
                    placements.Count % 5 != 0 ? "Qualified" : "Pending";


                placements.Add(newPlacement);

                s = sr.ReadLine();
            }

            return placements;
        }

        [Test]
        public void ViewPlacementsFromCSV()
        {
            var placements =
                GetPlacements(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Placement.csv");
            foreach (var placement in placements)
            {
                Console.WriteLine($"{placement.CandidateNumber} {placement.OpeningNumber}");
            }
        }

        [Test]
        public void ViewPendingPlacement()
        {
            using var context = new TecContext();

            var pendingPlacements = context.Placements.Where(c => c.PlacementStatus.ToLower().Equals("pending"))
                .Include(c => c.CandidateLink)
                .Include(c => c.OpeningLink);

            foreach (var pendingPlacement in pendingPlacements)
            {
                //Console.WriteLine($"{pendingPlacement.OpeningNumber} {pendingPlacement.CandidateLink.CandidateName}");
            }
        }


    }
}