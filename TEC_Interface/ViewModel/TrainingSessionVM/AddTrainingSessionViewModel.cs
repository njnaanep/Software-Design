using System;
using System.Collections.ObjectModel;
using System.Linq;
using ServiceLayer;
using TEC_Interface.ViewModel.Course;
using TEC_Interface.ViewModel.TrainingSession;

namespace TEC_Interface.ViewModel.TrainingSessionVM
{
    public class AddTrainingSessionViewModel
    {
        private TrainingSessionService _trainingSessionService;
        private readonly CourseService _courseService;

        public AddTrainingSessionViewModel(TrainingSessionService trainingSessionService, CourseService courseService)
        {
            _trainingSessionService = trainingSessionService;
            _courseService = courseService;

            CourseList = new ObservableCollection<DataLayer.EfClasses.Course>(_courseService.GetCourses());

            if (CourseList.Count > 0) 
                SelectedCourse = CourseList.First();
        }

        public DataLayer.EfClasses.Course SelectedCourse { get; set; }
        public ObservableCollection<DataLayer.EfClasses.Course> CourseList { get; set; }

        public string Fee { get; set; }
        public string Capacity { get; set; } 


        public TrainingSessionViewModel AssociatedSession { get; set; }

        public void Add()
        {
            var session = new DataLayer.EfClasses.TrainingSession
            {
                SessionFee = double.Parse(Fee),
                SessionCapacity = uint.Parse(Capacity),
                CourseId = SelectedCourse.CourseId
            };

            _trainingSessionService.AddTrainingSession(session);

            AssociatedSession = new TrainingSessionViewModel(session);
        }
    }
}