using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DataLayer.EfClasses
{
    public class Course
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public double CourseHours { get; set; }

        public ICollection<TrainingSession> TrainingSessions { get; set; }
        public ICollection<Prerequisite> Prerequisites { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}