using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ServiceLayer;
using TEC_Interface.ViewModel.Candidate;

namespace TEC_Interface.ViewModel.Qualification
{
    public class QualificationListViewModel
    {
        private QualificationService _qualificationService;
        private QualificationViewModel _selectedQualification;
        private string _searchText;

        public QualificationListViewModel(QualificationService qualificationService)
        {
            _qualificationService = qualificationService;

            QualificationList = new ObservableCollection<QualificationViewModel>(
                _qualificationService.GetQualifications().Select( c=>
                    new QualificationViewModel(c)));

            if (QualificationList.Count > 0) SelectedQualification = QualificationList.First();
            
        }



        #region STORAGE
        public ObservableCollection<QualificationViewModel> QualificationList { get; set; }

        public ObservableCollection<QualificationOpeningViewModel> QualificationOpeningList { get; set; } = 
            new ObservableCollection<QualificationOpeningViewModel>();

        public ObservableCollection<QualificationPrerequisitesViewModel> QualificationPrerequisitesList { get; set; } = 
            new ObservableCollection<QualificationPrerequisitesViewModel>();

        public ObservableCollection<QualificationCertificateViewModel> QualificationCertificateList { get; set; } =
            new ObservableCollection<QualificationCertificateViewModel>();

        #endregion

        #region DataGrid

        public void DisplayQualificationDataGrid(QualificationViewModel selectedQualification)
        {
            DisplayOpenings(selectedQualification.QualificationId);
            DisplayCertification(selectedQualification.QualificationId);
            DisplayPrerequisites(selectedQualification.QualificationId);
            //DisplayUnqualifiedCandidates(selectedQualification.QualificationId);
            //DisplayUnrelatedCourse(selectedQualification.QualificationId);

        }

        

        public void ClearDisplayData()
        {
            QualificationList.Clear();
            QualificationOpeningList.Clear();
            QualificationCertificateList.Clear();
            QualificationPrerequisitesList.Clear();
        }

        private void DisplayCertification(string qualificationId)
        {
            QualificationCertificateList.Clear();

            //var certificates = new CertificationService(MainWindow.MainContext).GetQualificationCertifications(qualificationId)
            //    .Select(c => new QualificationCertificateViewModel(c));

            var certificates = CertificationService.GetQualificationCertifications(qualificationId)
                .Select(c => new QualificationCertificateViewModel(c));

            foreach (var certificate in certificates) 
                QualificationCertificateList.Add(certificate);
        }

        private void DisplayPrerequisites(string qualificationId)
        {
            QualificationPrerequisitesList.Clear();

            var courses = PrerequisiteService.GetQualificationPrerequisites(qualificationId)
                .Select(c => new QualificationPrerequisitesViewModel(c));

            foreach (var course in courses)
                QualificationPrerequisitesList.Add(course);
        }

        private void DisplayOpenings(string qualificationId)
        {
            QualificationOpeningList.Clear();

            var openings = new OpeningService(MainWindow.MainContext).GetQualificationOpenings(qualificationId)
                .Select(c => new QualificationOpeningViewModel(c));

            foreach (var opening in openings) 
                QualificationOpeningList.Add(opening);
        }

        #endregion

        public void SearchQualification(string searchString)
        {
            ClearDisplayData();

            var qualifications = _qualificationService.SearchQualification(searchString);

            foreach (var qualification in qualifications)
            {
                var qualificationViewModel = new QualificationViewModel(qualification);

                QualificationList.Add(qualificationViewModel);
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchQualification(_searchText);
            }
        }

        public QualificationViewModel SelectedQualification
        {
            get => _selectedQualification;
            set
            {
                _selectedQualification = value;
                if (_selectedQualification != null)
                    DisplayQualificationDataGrid(_selectedQualification);
            }
        }

        public void DeleteQualification(QualificationViewModel qualification)
        {
            _qualificationService.DeleteQualification(qualification.QualificationCode);
            QualificationList.Remove(qualification);
        }

        #region QualifyCandidate

        public void DisplayUnqualifiedCandidates()
        {
            CandidateList.Clear();

            var unqualifiedCandidates = new CandidateService(MainWindow.MainContext).GetUnqualifiedCandidate(SelectedQualification.QualificationId);

            foreach (var candidate in unqualifiedCandidates) 
                CandidateList.Add(candidate);
        }

        public ObservableCollection<DataLayer.EfClasses.Candidate> CandidateList { get; set; }
         = new ObservableCollection<DataLayer.EfClasses.Candidate>();


        public DataLayer.EfClasses.Candidate SelectedCandidate { get; set; }

        private string _searchCandidateText;
        public string SearchCandidateString
        {   
            get => _searchCandidateText;
            set
            {
                _searchCandidateText = value;
                SearchCandidate(_searchCandidateText);
            }
        }

        private void SearchCandidate(string searchText)
        {
            CandidateList.Clear();

            var candidates = new CandidateService(MainWindow.MainContext)
                .SearchUnqualifiedCandidate(SelectedQualification.QualificationId,searchText);
            
            foreach (var candidate in candidates)
                CandidateList.Add(candidate);
        }

        public void AddQualifiedCandidate()
        {
            var certification = new Certification
            {
                CandidateNumber = SelectedCandidate.CandidateNumber,
                CertificationDate = DateTime.Now,
                QualificationId = SelectedQualification.QualificationId
            };

            new CertificationService(MainWindow.MainContext).AddCertification(certification);

            QualificationCertificateList.Add(new QualificationCertificateViewModel(certification));

            //DisplayCertification(SelectedQualification.QualificationId);
        }

        #endregion

        #region QualificationPrerequisite
        public ObservableCollection<DataLayer.EfClasses.Course> CourseList { get; set; } 
            = new ObservableCollection<DataLayer.EfClasses.Course>();

        public DataLayer.EfClasses.Course SelectedCourse{ get; set; }

        public void DisplayUnrelatedCourse()
        {
            CourseList.Clear();

            var unrelatedCourses = new CourseService(MainWindow.MainContext).GetUnrelatedCourses(SelectedQualification.QualificationId);

            foreach (var course in unrelatedCourses) 
                CourseList.Add(course);
        }

        private string _searchCourseText;
        public string SearchCourseString
        {
            get => _searchCourseText;
            set
            {
                _searchCourseText = value;
                SearchCourse(_searchCourseText);
            }
        }

        private void SearchCourse(string searchText)
        {
            CourseList.Clear();

            var courses = new CourseService(MainWindow.MainContext).SearchUnrelatedCourse(SelectedQualification.QualificationId,searchText);

            foreach (var course in courses)
                CourseList.Add(course);
        }
        public void AddQualificationPrerequisite()
        {
            var prerequisite = new Prerequisite
            {
                CourseId = SelectedCourse.CourseId,
                RequirementFor = "qualification",
                QualificationId = SelectedQualification.QualificationId
            };

            new PrerequisiteService(MainWindow.MainContext).AddPrerequisite(prerequisite);

            QualificationPrerequisitesList.Add(new QualificationPrerequisitesViewModel(prerequisite));
            CourseList.Remove(SelectedCourse);
            //DisplayPrerequisites(SelectedQualification.QualificationId);
        }


        public void RemoveQualificationPrerequisite()
        {
            new PrerequisiteService(MainWindow.MainContext).DeletePrerequisite(SelectedPrerequisite.PrerequisiteId);
            QualificationPrerequisitesList.Remove(SelectedPrerequisite);

            //DisplayUnrelatedCourse(SelectedQualification.QualificationId);
        }

        public QualificationPrerequisitesViewModel SelectedPrerequisite { get; set; }

        #endregion
    }
}