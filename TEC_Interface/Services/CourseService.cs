using System.Linq;
using DataLayer.EfClasses;

namespace TEC_Interface.Services
{
    public class CourseService
    {
        private TecContext _context;

        public CourseService(TecContext context) => _context = context;

        public IQueryable<Course> GetCourses() => _context.Courses.Where(c => c.IsDeleted == false);

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(string courseId, string name, string description, double hours)
        {
            var course = _context.Courses.Find(courseId);
            course.CourseName = name;
            course.CourseDescription = description;
            course.CourseHours = hours;

            _context.SaveChanges();
        }

        public void DeleteCourse(string courseId)
        {
            var prerequisites = _context.Prerequisites.Where(c => c.CourseId == courseId);
            foreach (var prerequisite in prerequisites)
                prerequisite.IsDeleted = true;

            var trainingSessions = _context.TrainingSessions.Where(c => c.CourseId == courseId);
            foreach (var trainingSession in trainingSessions)
            {
                var registrations = _context.RegisteredCandidates.Where(c => c.SessionId == trainingSession.SessionId);
                foreach (var registeredCandidate in registrations)
                {
                    registeredCandidate.IsDeleted = true;
                }

                trainingSession.IsDeleted = true;
            }

            _context.Courses.Find(courseId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}