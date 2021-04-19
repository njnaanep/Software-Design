using System;
using System.Collections.Generic;using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NUnit.Framework;
using DataLayer.EfClasses;
using DataLayer.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json.Serialization;
using NUnit.Framework.Internal;

namespace TEC_TestCase
{
    [TestFixture()]
    public class CandidateTest
    {

        [Test]
        public void Candidate_ViewCandidate()
        {
            using var context = new TecContext();

            var candidates = context.Candidates;

            foreach (var candidate in candidates)
            {
                Console.WriteLine($"{candidate.CandidateNumber}");
            }
        }

        [Test]
        public void Candidate_ManualAdd()
        {
            using var context = new TecContext();
            var newCandidate = new Candidate
            {
                CandidateFirstName = "Nathan Jesther",
                CandidateLastName = "Naanep",
                CandidateMiddleName = "Gagula",
                //CandidateName = "Nata de Coco",
                CandidateGender = "Male",
                CandidateAddress = "DVO",
                CandidateBirthDate = new DateTime(2000, 3, 25),
                CandidateContactNumber = "09999999999",
                CandidateEmail = "mail@mail.com"
            };

            context.Add(newCandidate);
            context.SaveChanges();

        }

        public static void GenerateCandidates()
        {
            var candidates =
                Candidate_ReadFromCSV(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Candidate.CSV");

            using var context = new TecContext();
            foreach (var candidate in candidates)
            {
                context.Add(candidate);
                context.SaveChanges();
            }
        }

        [Test]
        public void DeleteCandidate()
        {
            using var context = new TecContext();

            context.Remove(context.Candidates.Find("CDT-000001"));
            context.Remove(context.Candidates.Find("CDT-000202"));
            //context.Remove(context.Candidates.Find("CDT-000203"));
            context.SaveChanges();
        }

        private static IList<Candidate> Candidate_ReadFromCSV(string location)
        {
            var sr = new StreamReader(location);

            var listCandidate = new List<Candidate>();

            string s = sr.ReadLine();

            while (s != null)
            {
                var split = s.Split(',');


                var newCandidate = new Candidate
                {
                    CandidateFirstName = split[0],
                    CandidateMiddleName = split[1] == "" ? null : split[1],
                    CandidateLastName = split[2],
                    CandidateGender = split[3],
                    CandidateAddress = split[4],
                    CandidateBirthDate = Convert.ToDateTime(split[5]),
                    CandidateContactNumber = split[6],
                    CandidateEmail = split[7],
                };
                //newCandidate.CandidateMiddleName = split[1] == "" ? null : split[1];


                listCandidate.Add(newCandidate);

                s = sr.ReadLine();
            }

            return listCandidate;
        }

        [Test]
        public void Candidate_ViewCandidateFromDataBase()
        {
            using var context = new TecContext();

            var candidates = context.Candidates.Where(c => c.IsDeleted == false);

            foreach (var candidate in candidates)
            {
                //Console.WriteLine($"{candidate.CandidateNumber}\t{candidate.CandidateName}\t{candidate.CandidateAge}");
            }
        }

        [Test]
        public void Candidate_ViewApplications()
        {
            using var context = new TecContext();

            //var candidates = context.Candidates.Where(c => c.IsDeleted == false);

            //foreach (var candidate in candidates)
            //{
            //    Console.WriteLine($"Candidate: {candidate.CandidateName}");

            //    var candidatesApplication = context.Applications
            //        .Where(c => c.CandidateNumber == candidate.CandidateNumber
            //                    && c.IsDeleted == false);

            //    foreach (var application in candidatesApplication) 
            //        Console.WriteLine($"{application.ApplicationId}");
            //}

            var candidate = context.Candidates.Find(new ForeignKeys().GetRandomCandidate());

            var candidateApplication =
                context.Placements.Where(c => c.CandidateNumber == candidate.CandidateNumber).ToList();

            //Console.WriteLine($"Candidate: {candidate.CandidateName}");
            foreach (var application in candidateApplication)
            {
                Console.WriteLine($"{application.PlacementId}");
            }

        }

        [Test]
        public void Candidate_UpdateCandidate()
        {
            using var context = new TecContext();
            //var editCandidate = context.Candidates.Find("CDT-000202");

            var updateCandidate = new Candidate
            {
                CandidateNumber = "CDT-000202",
                //CandidateName = "Nathan Jesther G. Naanep",
                CandidateGender = "Male",
                CandidateAddress = "Koronadal City",
                CandidateEmail = "mail.edu.ph"
            };

            context.Candidates.Update(updateCandidate);
            context.SaveChanges();


        }

        [Test]
        public void SearchCandidate()
        {
            using var context = new TecContext();
            var search = "ap";

            var candidates = context.Candidates.Where(c => c.CandidateFullName.Contains(search));
            var searchCandidate = candidates.Where(c => !c.IsDeleted);

            if (!searchCandidate.Any())
                Console.WriteLine($"No candidate contains {search}");
            else
                foreach (var candidate in searchCandidate)
                    Console.WriteLine($"{candidate.CandidateFullName}\t{candidate.IsDeleted}");
        }

        [Test]
        public void ConvertEnumsToList()
        {
            var genders = Enum.GetNames(typeof(Gender));
            foreach (var gender in genders)
            {
                Console.WriteLine(gender);
            }
        }

        public enum Gender
        {
            Male,
            Female,
            Gay,
            Lesbian,
            Bisexual
        }

        [Test]
        public void GetCandidateWithOutAssociatedEntity_ByLinq()
        {
            using var _context = new TecContext();

            string qualificationCode = "QLN-000001";

            var candidates = _context.Candidates.Where(candidate =>
                _context.Certifications
                    .Where(certification => certification.QualificationId == qualificationCode)
                    .Select(certification => certification.CandidateNumber)
                    .Contains(candidate.CandidateNumber));

            foreach (var candidate in candidates)
                Console.WriteLine($"{candidate.CandidateNumber}\t{candidate.CandidateFullName}");

        }

        [Test]
        public void GetCandidateWithOutAssociatedEntity_ExplicitLoading()
        {
            using var _context = new TecContext();


            string qualificationCode = "QLN-000001";


            var candidates = _context.Candidates.Where(c => !c.IsDeleted).ToList();

            var certifications = _context.Certifications.Where(c => c.QualificationId == qualificationCode)
                .Select(c => c.CandidateNumber).ToList();


            var candidateList = new List<Candidate>();

            foreach (var candidate in candidates)
                if (certifications.Any(c => c == candidate.CandidateNumber))
                    if (!candidateList.Contains(candidate))
                        candidateList.Add(candidate);

            foreach (var candidate in candidateList)
                Console.WriteLine($"{candidate.CandidateNumber}\t{candidate.CandidateFullName}");
        }

        [Test]
        public void GetCandidateWithOutTheGivenQualification()
        {
            using var _context = new TecContext();


            string qualificationCode = "QLN-000001";

            var qualifiedCandidates = _context.Candidates.Where(candidate =>
                _context.Certifications.Where(certification => certification.QualificationId == qualificationCode)
                    .Select(certification => certification.CandidateNumber)
                    .Contains(candidate.CandidateNumber)).Select(c => c.CandidateNumber).ToList();

            var candidateList = _context.Candidates.Where(candidate =>
                !qualifiedCandidates.Any(c => c
                    .Equals(candidate.CandidateNumber)));

            //var candidateList = new List<Candidate>();

            //foreach (var candidate in _context.Candidates)
            //    if (!qualifiedCandidates.Any(c => c.Equals(candidate.CandidateNumber)))
            //        candidateList.Add(candidate);

            foreach (var candidate in candidateList)
                Console.WriteLine($"{candidate.CandidateNumber}\t{candidate.CandidateFullName}");

        }

        [Test]
        public void ViewUnEnrolledCandidate()
        {
            using var context = new TecContext();

            var sessionId = "TRA-000005";

            var enrolledCandidate = context.RegisteredCandidates.Where(c =>
                    c.SessionId.Equals(sessionId)).Select(c => c.CandidateNumber)
                .OrderBy(c => c).ToList();

            var candidates = context.Candidates.Where(c => !c.IsDeleted);

            foreach (var candidate in enrolledCandidate) Console.WriteLine(candidate);

            var candidateList = new List<Candidate>();

            foreach (var candidate in candidates)
                if (!enrolledCandidate.Any(c => c.Equals(candidate.CandidateNumber)))
                    candidateList.Add(candidate);

            Console.WriteLine($"\nNon Enrolled Candidate\n");
            foreach (var candidate in candidateList)
                Console.WriteLine($"{candidate.CandidateNumber}\t{candidate.CandidateFullName}");


            //var candidateList = new List<Candidate>();

            //foreach (var candidate in candidates)
            //{
            //    if (!enrolledCandidate.Any(c => c.Equals(candidate.CandidateNumber)))
            //    {
            //        candidateList.Add(candidate);
            //    }
            //}
        }

    }



    public class Order
    {
        private string _status;
        private bool _isServed;

        public string Status
        {
            get => _status;
            set => _status = value;
        }

        public bool IsServed => Status == "Served";
    }

    [TestFixture()]
    public class OrderTest
    {
        [Test]
        public void CheckBool()
        {
            var order = new Order();

            order.Status = "Served";

            Console.WriteLine(order.IsServed);
        }

        [Test]
        public void CheckTimeIf5MinuteHadPass()
        {
            var start = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));

            if (start.AddMinutes(5) >= DateTime.Now)
                Console.WriteLine("YES");
            else
                Console.WriteLine("NO");
        }
    }



}

