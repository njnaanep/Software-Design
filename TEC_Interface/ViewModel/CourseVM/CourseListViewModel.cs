
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using DataLayer.EfClasses;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Course
{
    public class CourseListViewModel
    {
        private CourseService _courseService;
        private CourseViewModel _selectedCourse;
        private string _searchText;


        #region STORAGE
        public ObservableCollection<CourseViewModel> CourseList { get; set; }

        public ObservableCollection<CoursePrerequisite> CoursePrerequisites { get; set; }
            = new ObservableCollection<CoursePrerequisite>();

        public ObservableCollection<CourseTrainingSession> CourseTrainingSessions { get; set; }
         = new ObservableCollection<CourseTrainingSession>();
        #endregion

        public CourseViewModel SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                if (_selectedCourse != null) 
                    DisplayCourseDatGrid(_selectedCourse.CourseId);
            }
        }

        #region DATA GRID

        private void DisplayCourseDatGrid(string courseId)
        {
            DisplayPrerequisites(courseId);
            DisplayTrainingSessions(courseId);
            //DisplayUnrelatedQualification(courseId);
        }

        private void DisplayTrainingSessions(string courseId)
        {
            CourseTrainingSessions.Clear();

            var sessions = new TrainingSessionService(MainWindow.MainContext).GetCourseTrainingSessions(courseId)
                .Select(c => new CourseTrainingSession(c));

            foreach (var session in sessions) 
                CourseTrainingSessions.Add(session);
        }

        private void DisplayPrerequisites(string courseId)
        {
            CoursePrerequisites.Clear();

            var qualifications = PrerequisiteService.GetCoursePrerequisites(courseId)
                .Select(c => new CoursePrerequisite(c));

            foreach (var qualification in qualifications) 
                CoursePrerequisites.Add(qualification);

            
        }

        #endregion

        public CourseListViewModel(CourseService courseService)
        {
            _courseService = courseService;

            CourseList = new ObservableCollection<CourseViewModel>(_courseService.GetCourses().Select(c=> 
                new CourseViewModel(c)));

            
            if (CourseList.Count > 0)
                SelectedCourse = CourseList.First();
        }

        public void SearchCourse(string searchString)
        {
            CourseList.Clear();

            var courses = _courseService.SearchCourse(searchString);

            foreach (var course in courses)
            {
                var courseViewModel = new CourseViewModel(course);

                CourseList.Add(courseViewModel);
            }

            if (CourseList.Count > 0) SelectedCourse = CourseList.First();

        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchCourse(_searchText);
            }
        }

        public void DeleteCourse(CourseViewModel course)
        {
            _courseService.DeleteCourse(course.CourseId);
            CourseList.Remove(course);
        }

        #region Prerquisite

        public ObservableCollection<DataLayer.EfClasses.Qualification> QualificationList { get; set; }
         = new ObservableCollection<DataLayer.EfClasses.Qualification>();
        public DataLayer.EfClasses.Qualification SelectedQualification { get; set; }

        public void DisplayUnrelatedQualification()
        {
            QualificationList.Clear();

            var unrelatedQualification =
                new QualificationService(MainWindow.MainContext).GetUnrelatedQualifications(SelectedCourse.CourseId);

            foreach (var qualification in unrelatedQualification) 
                QualificationList.Add(qualification);
        }

        public void AddPrerequisite()
        {
            var prerequisite = new Prerequisite
            {
                CourseId = SelectedCourse.CourseId,
                QualificationId = SelectedQualification.QualificationId,
                RequirementFor = "Course"
            };

            new PrerequisiteService(MainWindow.MainContext).AddPrerequisite(prerequisite);

            CoursePrerequisites.Add(new CoursePrerequisite(prerequisite));

            QualificationList.Remove(SelectedQualification);
            //DisplayPrerequisites(SelectedCourse.CourseId);
        }

        public CoursePrerequisite SelectedPrerequisite { get; set; }
        public void RemovePrerequisite()
        {
            new PrerequisiteService(MainWindow.MainContext).DeletePrerequisite(SelectedPrerequisite.PrerequisiteId);
            CoursePrerequisites.Remove(SelectedPrerequisite);

            //DisplayUnrelatedQualification(SelectedCourse.CourseId);
        }

        #endregion
    }
}