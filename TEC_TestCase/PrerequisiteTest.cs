using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TEC_TestCase
{
    [TestFixture]
    public class PrerequisiteTest
    {
        public static void GeneratePrerequisites()
        {
            using var context = new TecContext();
            var foreignKey = new ForeignKeys();

            for (int i = 0; i < 200; i++)
            {

                var newCoursePrerequisite = new Prerequisite()
                {
                    CourseId = foreignKey.GetRandomCourse(),
                    QualificationId = foreignKey.GetRandomQualification(),
                    RequirementFor = context.Prerequisites.Count() % 2 == 0 ? 
                        "Course" : "Qualification"
                };


                context.Add(newCoursePrerequisite);
                context.SaveChanges();
            }
        }

        [Test]
        public void DisplayPrerequisiteForQualification()
        {
            using var context = new TecContext();

            var prereq = context.Prerequisites.Where(c => c.RequirementFor.ToLower() == "course")
                .Include(c => c.CourseLink)
                .Include(c => c.QualificationLink);

            foreach (var prerequisite in prereq)
            {
                Console.WriteLine($"{prerequisite.QualificationLink.QualificationCode} Require {prerequisite.CourseLink.CourseName}");
            }
        }
    }
}