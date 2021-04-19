using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using ServiceLayer;
using TEC_Interface.ViewModel.Course;
using TEC_Interface.ViewModel.TrainingSession;

namespace TEC_Interface.ViewModel.TrainingSessionVM
{
    public class EditSessionViewModel
    {
        public TrainingSessionViewModel SessionToEdit { get; set; }

        private TrainingSessionService _trainingSessionService;
        private CourseService _courseService;

        public EditSessionViewModel(TrainingSessionService trainingSessionService, CourseService courseService, TrainingSessionViewModel sessionToEdit)
        {
            _trainingSessionService = trainingSessionService;
            _courseService = courseService;
            SessionToEdit = sessionToEdit;

            CourseList = new ObservableCollection<DataLayer.EfClasses.Course>(courseService.GetCourses());
            SelectedCourse = CourseList.First(c => c.CourseName == sessionToEdit.CourseName);

            CopyEditableFields(sessionToEdit);
        }

        private void CopyEditableFields(TrainingSessionViewModel sessionToEdit)
        {
            Fee = sessionToEdit.Fee.ToString();
            Capacity = sessionToEdit.Capacity.ToString();
        }

        public DataLayer.EfClasses.Course SelectedCourse { get; set; }

        public ObservableCollection<DataLayer.EfClasses.Course> CourseList { get; set; }

        public string Fee { get; set; }
        public string Capacity { get; set; }

        public void Edit()
        {
            var updatedSession = new DataLayer.EfClasses.TrainingSession
            {
                SessionId = SessionToEdit.SessionId,
                CourseId = SelectedCourse.CourseId,
                SessionFee = double.Parse(Fee),
                SessionCapacity = uint.Parse(Capacity)
            };

            SessionToEdit.Fee = double.Parse(Fee);
            SessionToEdit.Capacity = uint.Parse(Capacity);
            SessionToEdit.CourseName = SelectedCourse.CourseName;

            _trainingSessionService.UpdateTrainingSession(updatedSession);
        }
    }
}