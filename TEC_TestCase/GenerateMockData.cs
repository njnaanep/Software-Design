using System.Runtime.InteropServices.ComTypes;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    public class GenerateMockData
    {
        [Test]
        public static void GenerateAllData()
        {
            CandidateTest.GenerateCandidates();
            CompanyTest.GenerateCompanies();
            JobHistoryTest.GenerateJobHistory();
            QualificationTest.GenerateQualifications();
            CourseTest.GenerateCourses();
            OpeningTest.GenerateOpenings();
            PlacementTest.GeneratePlacements();
            CertificationTest.GenerateCertifications();
            PrerequisiteTest.GeneratePrerequisites();
            SessionTest.GenerateSessions();
            RegistrationTest.GenerateRegistrations();
            DayTest.GenerateDays();
            ScheduleTest.GenerateSchedules();
        }

        [Test]
        public void GenerateRequiredData()
        {
            
        }
    }
}