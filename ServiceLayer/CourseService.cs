using System.Linq;
using DataLayer.EfClasses;

namespace ServiceLayer
{
    public class CourseService
    {
        private TecContext _context;

        public CourseService(TecContext context) => _context = context;

        public IQueryable<Course> GetCourses() => _context.Courses.Where(c => !c.IsDeleted);

        public IQueryable<Course> GetUnrelatedCourses(string qualificationId)
        {
            var requiredCourses = _context.Prerequisites
                .Where(c => !c.IsDeleted && c.RequirementFor.ToLower() == "qualification"
                                         && c.QualificationId == qualificationId)
                .Select(c => c.CourseId).ToList();

            return GetCourses().Where(course => 
                    !requiredCourses.Any(c => c.Equals(course.CourseId))).AsQueryable();
        }

        
        public IQueryable<Course> SearchCourse(string search) => SearchCourseQuery(GetCourses(), search);

        public IQueryable<Course> SearchUnrelatedCourse(string qualificationId, string search)
            => SearchCourseQuery(GetUnrelatedCourses(qualificationId), search);

        public IQueryable<Course> SearchCourseQuery(IQueryable<Course> courseQueryable, string search)
            => courseQueryable.Where(c => c.CourseName.Contains(search) || c.CourseDescription.Contains(search));

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            var updatedCourse = _context.Courses.Find(course.CourseId);
            updatedCourse.CourseName = course.CourseName;
            updatedCourse.CourseDescription = course.CourseDescription;
            updatedCourse.CourseHours = course.CourseHours;

            _context.SaveChanges();
        }

        public void DeleteCourse(string courseId)
        {
            _context.Courses.Find(courseId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}