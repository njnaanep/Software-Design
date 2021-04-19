using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DataLayer.EfClasses;

namespace TEC_TestCase
{
    public class ForeignKeys
    {
        public string GetRandomCandidate()
        {
            using var context = new TecContext();

            var count = context.Candidates.Count();

            var r = new Random();
            var selected = r.Next(minValue: 0, maxValue: count);

            var candidate = context.Candidates.Skip(selected).Take(1).First();

            return candidate.CandidateNumber;
        }

        public string GetRandomCompany()
        {
            using var context = new TecContext();

            var count = context.Companies.Count();

            var r = new Random();
            var selected = r.Next(minValue: 0, maxValue: count);

            var company = context.Companies.Skip(selected).Take(1).First();

            return company.CompanyId;
        }


        public string GetRandomPlacement()
        {
            using var context = new TecContext();

            var count = context.Placements.Count();

            var r = new Random();
            var selected = r.Next(minValue: 0, maxValue: count);

            var placement = context.Placements.Skip(selected).Take(1).First();

            return placement.PlacementId;
        }

        public string GetRandomOpening()
        {
            using var context = new TecContext();

            var count = context.Openings.Count();

            var r = new Random();
            var selected = r.Next(minValue: 0, maxValue: count);

            var opening = context.Openings.Skip(selected).Take(1).First();

            return opening.OpeningNumber;
        }

        public string GetRandomQualification()
        {
            using var context = new TecContext();

            var count = context.Qualifications.Count();

            var r = new Random();
            var selected = r.Next(minValue: 0, maxValue: count);

            var qualification = context.Qualifications.Skip(selected).Take(1).First();

            return qualification.QualificationId;
        }

        public string GetRandomCourse()
        {
            using var context = new TecContext();

            var count = context.Courses.Count();

            var r = new Random();
            var selected = r.Next(minValue: 0, maxValue: count);

            var course = context.Courses.Skip(selected).Take(1).First();

            return course.CourseId;
        }

        public string GetRandomSessions()
        {
            using var context = new TecContext();

            var count = context.TrainingSessions.Count();

            var r = new Random();
            var selected = r.Next(minValue: 0, maxValue: count);

            var trainingSession = context.TrainingSessions.Skip(selected).Take(1).First();

            return trainingSession.SessionId;
        }


        public string GetRandomDay()
        {
            using var context = new TecContext();

            var count = context.Days.Count();

            var r = new Random();
            var selected = r.Next(minValue: 0, maxValue: count);

            var trainingSession = context.Days.Skip(selected).Take(1).First();

            return trainingSession.DayId;
        }

    }
}