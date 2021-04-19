using DataLayer.EfCode.Configuration;
using ServiceLayer;
using TEC_Interface.ViewModel.Course;

namespace TEC_Interface.View.Course
{
    public class EditCourseViewModel
    {
        public CourseViewModel CourseToEdit { get; set; }
        private CourseService _courseService;

        public EditCourseViewModel(CourseViewModel courseToEdit, CourseService courseService)
        {
            _courseService = courseService;
            CourseToEdit = courseToEdit;

            CourseId = CourseToEdit.CourseId;
            CopyEditableField(CourseToEdit);

        }

        private void CopyEditableField(CourseViewModel courseToEdit)
        {
            Name = courseToEdit.CourseName;
            Description = courseToEdit.Description;
            Hours = courseToEdit.Hours;
        }

        public string CourseId { get; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }

        public void Edit()
        {
            var updatedCourse = new DataLayer.EfClasses.Course
            {
                CourseId = CourseId,
                CourseName = Name,
                CourseDescription = Description,
                CourseHours = Hours
            };

            CourseToEdit.CourseName = Name;
            CourseToEdit.Description= Description;
            CourseToEdit.Hours= Hours;

            _courseService.UpdateCourse(updatedCourse);
        }
    }
}