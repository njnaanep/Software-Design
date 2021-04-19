using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    public class QualificationTest 
    {
        [Test]
        public static void GenerateQualifications()
        {
            var qualifications =
                Qualification_ReadFromCSV(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Qualification.CSV");

            using var context = new TecContext();
            foreach (var qualification in qualifications)
            {
                context.Add(qualification);
                context.SaveChanges();
            }
        }

        [Test]
        public void DisplayReadFile()
        {
            var qualifications =
                Qualification_ReadFromCSV(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Qualification.CSV");

            
            foreach (var qualification in qualifications)
            {
                Console.WriteLine($"Code: {qualification.QualificationCode}\tDescription: {qualification.Description}");
            }
        }

        private static IList<Qualification> Qualification_ReadFromCSV(string location)
        {
            var sr = new StreamReader(location);

            var qualificationList = new List<Qualification>();

            string s = sr.ReadLine();

            while (s != null)
            {
                var split = s.Split(';');

                var qualification = new Qualification
                {
                    QualificationCode = split[0],
                    Description = split[1]
                };

                qualificationList.Add(qualification);

                s = sr.ReadLine();
            }

            return qualificationList;
        }

        [Test]
        public void DisplayUnrelatedQualification()
        {
            using var db = new TecContext();

            var courseId = "COR-000008";

            var requiredQualifications =
                db.Prerequisites.Where(prerequisite => prerequisite.RequirementFor.ToLower() == "course" 
                                                       && prerequisite.CourseId == courseId)
                    .Select(prerequisite => prerequisite.QualificationId).ToList();

            var qualifications = db.Qualifications.Where(c => !c.IsDeleted).ToList();

            var qualificationList = qualifications.Where(q
                => !requiredQualifications.Any(c =>
                    c.Equals(q.QualificationId))).AsQueryable();

            //foreach (var qualification in qualifications)
            //{
            //    if (!requiredQualifications.Any(c => c.Equals(qualification.QualificationId)))
            //    {
            //        qualificationList.Add(qualification);
            //    }
            //}

            foreach (var course in qualificationList)
                Console.WriteLine($"{course.QualificationId}\t{course.QualificationCode}");


            Console.WriteLine("\nRequired Courses");
            foreach (var course in requiredQualifications)
            {
                Console.WriteLine(course);
            }
        }
    }
}