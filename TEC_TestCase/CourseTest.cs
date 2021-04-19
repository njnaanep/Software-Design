using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    public class CourseTest
    {
        private static IList<Course> GetCourse(string location)
        {
            var sr = new StreamReader(location);

            var courses = new List<Course>();
            string s = sr.ReadLine();

            while (s != null)
            {
                var split = s.Split(',');

                var newCourse = new Course
                {
                    CourseName = split[0],
                    CourseDescription = split[2],
                    CourseHours = Convert.ToDouble(split[1]),
                };

                courses.Add(newCourse);

                s = sr.ReadLine();
            }

            return courses;
        }

        [Test]
        public static void GenerateCourses()
        {
            var courses =
                GetCourse(@"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Course.CSV");

            using var context = new TecContext();

            foreach (var course in courses)
            {
                context.Add(course);
                context.SaveChanges();
            }
        }

        [Test]
        public void DeleteCourse()
        {
            using var context = new TecContext();

            context.Remove(context.Courses.Find("COR-000001"));
            //context.SaveChanges();
        }

        [Test]
        public void DisplayUnrelatedCourse()
        {
            using var db = new TecContext();

            string qualification = "QLN-000001";
            var requiredCourses =
                db.Prerequisites.Where(c => c.RequirementFor.ToLower() == "qualification"
                                            && c.QualificationId == qualification)
                    .Select(c => c.CourseId).OrderBy(c => c).ToList();

            var courses = db.Courses.Where(c => !c.IsDeleted).ToList();

            var courseList = courses.Where(course => !requiredCourses.Any(c =>
                c.Equals(course.CourseId))).OrderBy(c => c.CourseId).ToList();

            foreach (var course in courseList)
                Console.WriteLine($"{course.CourseId}\t{course.CourseName}");


            Console.WriteLine("\nRequired Courses");
            foreach (var course in requiredCourses)
            {
                Console.WriteLine(course);
            }
        }

        
    }
}