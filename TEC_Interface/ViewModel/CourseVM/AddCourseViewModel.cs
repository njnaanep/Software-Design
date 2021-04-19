using System;
using System.CodeDom;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Course
{
    public class AddCourseViewModel
    {
        private CourseService _courseService;

        public AddCourseViewModel(CourseService courseService)
        {
            _courseService = courseService;
        }

        public string Name { get; set; } 
        public string Description { get; set; } 
        public double Hours { get; set; } 

        public CourseViewModel AssociatedCourse { get; set; }

        public void Add()
        {
            var course = new DataLayer.EfClasses.Course
            {
                CourseName = Name,
                CourseDescription = Description,
                CourseHours = Hours
            };

            _courseService.AddCourse(course);

            AssociatedCourse = new CourseViewModel(course);
        }
    }
}