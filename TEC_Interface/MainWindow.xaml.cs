using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer.EfClasses;
using DataLayer.EfCode.Configuration;
using ServiceLayer;
using TEC_Interface.View;
using TEC_Interface.View.Company;
using TEC_Interface.View.Course;
using TEC_Interface.View.JobHistory;
using TEC_Interface.View.JobHistoryView;
using TEC_Interface.View.OpeningView;
using TEC_Interface.View.Qualifications;
using TEC_Interface.View.TrainingSession;
using TEC_Interface.View.TrainingSessionView;
using TEC_Interface.ViewModel.Candidate;
using TEC_Interface.ViewModel.Company;
using TEC_Interface.ViewModel.Course;
using TEC_Interface.ViewModel.Opening;
using TEC_Interface.ViewModel.Placement;
using TEC_Interface.ViewModel.Qualification;
using TEC_Interface.ViewModel.TrainingSession;
using TEC_TestCase;

namespace TEC_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private CandidateService _candidateService;
        private CandidateListViewModel _candidateListViewModel;

        private CompanyService _companyService;
        private CompanyListViewModel _companyListViewModel;

        private QualificationService _qualificationService;
        private QualificationListViewModel _qualificationListViewModel;

        private JobHistoryService _jobHistoryService;
        private JobHistoryListViewModel _jobHistoryListViewModel;

        private OpeningService _openingService;
        private OpeningListViewModel _openingListViewModel;

        private PlacementService _placementService;
        private PlacementListViewModel _placementListViewModel;

        private CourseService _courseService;
        private CourseListViewModel _courseListViewModel;

        private TrainingSessionService _trainingSessionService;
        private TrainingSessionListViewModel _trainingSessionListViewModel;
        private ScheduleService _scheduleService;

        #endregion

        public static TecContext MainContext;

        public MainWindow()
        {
            InitializeComponent();

            // GenerateMockData.GenerateAllData();

            MainContext = new TecContext();

            _candidateService = new CandidateService(MainContext);
            _candidateListViewModel = new CandidateListViewModel(_candidateService);

            _companyService = new CompanyService(MainContext);
            _companyListViewModel = new CompanyListViewModel(_companyService);

            _qualificationService = new QualificationService(MainContext);
            _qualificationListViewModel = new QualificationListViewModel( _qualificationService);

            _jobHistoryService = new JobHistoryService(MainContext);
            _jobHistoryListViewModel = new JobHistoryListViewModel(_jobHistoryService);
            
            _placementService = new PlacementService(MainContext);
            _placementListViewModel = new PlacementListViewModel(_placementService);            

            _openingService = new OpeningService(MainContext);
            _openingListViewModel = new OpeningListViewModel(_openingService);

            _courseService = new CourseService(MainContext);
            _courseListViewModel = new CourseListViewModel(_courseService);

            _trainingSessionService = new TrainingSessionService(MainContext);
            _scheduleService = new ScheduleService(MainContext);
            _trainingSessionListViewModel = new TrainingSessionListViewModel(_trainingSessionService);


            CandidateTab.DataContext = _candidateListViewModel;
            CompanyTab.DataContext = _companyListViewModel;
            QualificationTab.DataContext = _qualificationListViewModel;
            HistoryTab.DataContext = _jobHistoryListViewModel;
            OpeningTab.DataContext = _openingListViewModel;
            PlacementTab.DataContext = _placementListViewModel;
            CourseTab.DataContext = _courseListViewModel;
            SessionTab.DataContext = _trainingSessionListViewModel;
        }

        #region Navigation  
        private void NavigationSelection(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                NavigationTab.SelectedIndex = NavigationList.SelectedIndex;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"NAVIGATION ERROR\n{exception}");
            }

        }

        private void ExpandNavigation(object sender, RoutedEventArgs e) 
            => NavigationGrid.Width = (bool)!NavigationToggle.IsChecked ? 45 : 200;
        #endregion


        #region Candidate
        private void BtnAddCandidate(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            var addCandidateWindow = new AddCandidatesView(_candidateListViewModel, _candidateService);
            addCandidateWindow.Show();
        }
        private void BtnEditCandidate(object sender, RoutedEventArgs e)
        {
            if (_candidateListViewModel.SelectedCandidate != null)
            {
                var editCandidate = new EditCandidateView(_candidateListViewModel.SelectedCandidate, _candidateService);
                editCandidate.Show();
            }
            
        }
        private void BtnDeleteCandidate(object sender, RoutedEventArgs e)
        {
            if (_candidateListViewModel.SelectedCandidate != null) 
            {
                var res = MessageBox.Show($"Delete {_candidateListViewModel.SelectedCandidate.FullName}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _candidateListViewModel.DeleteCandidate(_candidateListViewModel.SelectedCandidate);
                
            }
        }

        #endregion

        #region Company
        private void BtnAddCompany(Object sender, RoutedEventArgs e)
        {
            var addCompanyWindow = new AddCompanyView(_companyListViewModel, _companyService);
            addCompanyWindow.Show();
        }
        private void BtnEditCompany(object sender, RoutedEventArgs e)
        {
            if (_companyListViewModel.SelectedCompany != null) 
            {
                var editCompany = new EditCompanyView(_companyListViewModel.SelectedCompany, _companyService);
                editCompany.Show();
            }
        }
        private void BtnDeleteCompany(object sender, RoutedEventArgs e)
        {
            if (_companyListViewModel.SelectedCompany != null) 
            {
                var res = MessageBox.Show($"Delete {_companyListViewModel.SelectedCompany.CompanyName}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _companyListViewModel.DeleteCompany(_companyListViewModel.SelectedCompany);

            }
        }
        #endregion

        #region Qualification

        private void BtnAddQualification(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            var addQualificationView = new AddQualificationView(_qualificationService, _qualificationListViewModel);
            addQualificationView.Show();
        }

        private void BtnEditQualification(object sender, RoutedEventArgs e)
        {
            if (_qualificationListViewModel.SelectedQualification != null)
            {
                var editQualification = new EditQualificationView(_qualificationListViewModel.SelectedQualification, _qualificationService);
                editQualification.Show();
            }
        }

        private void BtnDeleteQualification(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_qualificationListViewModel.SelectedQualification != null)
            {
                var res = MessageBox.Show($"Delete {_qualificationListViewModel.SelectedQualification.QualificationCode}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _qualificationListViewModel.DeleteQualification(_qualificationListViewModel.SelectedQualification);

            }
        }

        private void DisplayCandidateForQualification(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_qualificationListViewModel.SelectedQualification != null)
            {
                _qualificationListViewModel.DisplayUnqualifiedCandidates();
                QualificationCandidatePopup.IsOpen = true;
            }
        }

        private void CancelNewRegistration(object sender, RoutedEventArgs e)
            => QualificationCandidatePopup.IsOpen = false;

        private void AddRegistration(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_qualificationListViewModel.SelectedCandidate != null)
                {
                    QualificationCandidatePopup.IsOpen = false;
                    _qualificationListViewModel.AddQualifiedCandidate();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Candidate:\n{exception}");
            }
        }

        private void CloseCourseLisPopup(object sender, RoutedEventArgs e) =>
            QualificationPrerequisitePopup.IsOpen = false;

        private void AddQualificationPrerequisitePopUp(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_qualificationListViewModel.SelectedCourse != null)
                {
                    QualificationPrerequisitePopup.IsOpen = false;
                    MessageBox.Show($"Prerequisite Successfully Added");
                    _qualificationListViewModel.AddQualificationPrerequisite();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Prerequisite:\n{exception}");
            }
        }
        private void AddQualificationPrerequisitePopup(object sender, RoutedEventArgs e)
        {
            if (_qualificationListViewModel.SelectedQualification != null)
            {
                _qualificationListViewModel.DisplayUnrelatedCourse();
                QualificationPrerequisitePopup.IsOpen = true;
            }
        }

        private void DeleteQualificationPrerequisite(object sender, RoutedEventArgs e)
        {
            if (_qualificationListViewModel.SelectedPrerequisite != null)
            {
                var res = MessageBox.Show($"Delete {_qualificationListViewModel.SelectedPrerequisite.Course} from Prerequisites?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _qualificationListViewModel.RemoveQualificationPrerequisite();
            }
        }

        #endregion

        #region Course
        private void BtnAddCourse(object sender, RoutedEventArgs e)
        {
            var addCourseView = new AddCourseView(_courseService, _courseListViewModel);
            addCourseView.Show();
        }

        private void BtnEditCourse(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_courseListViewModel.SelectedCourse != null)
            {
                var editCourse = new EditCourseView(_courseListViewModel.SelectedCourse, _courseService);
                editCourse.Show();
            }
        }

        private void BtnRemoveCourse(object sender, RoutedEventArgs e)
        {
            if (_courseListViewModel.SelectedCourse != null)
            {
                var res = MessageBox.Show($"Delete {_courseListViewModel.SelectedCourse.CourseName}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _courseListViewModel.DeleteCourse(_courseListViewModel.SelectedCourse);

            }
        }

        private void RemoveCoursePrerequisite(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_courseListViewModel.SelectedPrerequisite != null)
            {
                var res = MessageBox.Show($"Delete {_courseListViewModel.SelectedPrerequisite.Qualification} from Prerequisites??",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _courseListViewModel.RemovePrerequisite();
            }
        }
        private void DisplayQualificationPopUp(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_courseListViewModel.SelectedCourse != null)
            {
                _courseListViewModel.DisplayUnrelatedQualification();
                CoursePrerequisitePopup.IsOpen = true;
            }

        }

        private void CancelCoursePrerequisite(object sender, RoutedEventArgs e)
            => CoursePrerequisitePopup.IsOpen = false;
        private void AddCoursePrerequisite(object sender, RoutedEventArgs e)
        {
            if (_courseListViewModel.SelectedQualification != null)
            {
                CoursePrerequisitePopup.IsOpen = false;
                MessageBox.Show("Prerequisite Successfully Added");
                _courseListViewModel.AddPrerequisite();
            }
        }


        #endregion

        #region Opening
        private void BtnAddOpening(object sender, RoutedEventArgs e)
        {
            var addOpeningView = new AddOpeningView(_openingListViewModel, _openingService, _companyService, _qualificationService);
            addOpeningView.Show();
        }

        private void BtnEditOpening(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_openingListViewModel.SelectedOpening != null)
            {
                var editOpeningView = new EditOpeningView(_openingListViewModel.SelectedOpening, _openingService, _companyService, _qualificationService);
                editOpeningView.Show();
            }
            
        }

        private void BtnDeleteOpening(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_openingListViewModel.SelectedOpening != null)
            {
                var res = MessageBox.Show($"Delete {_openingListViewModel.SelectedOpening.OpeningNumber}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _openingListViewModel.DeleteOpening(_openingListViewModel.SelectedOpening);

            }
        }

        private void NewPlacementApplication(object sender, RoutedEventArgs e)
        {
            if (_openingListViewModel.SelectedOpening != null)
            {
                _openingListViewModel.DisplayPotentialCandidate();
                NewApplicationPopup.IsOpen = true;
            }
        }

        private void CloseApplicationPopup(object sender, RoutedEventArgs e) 
            => NewApplicationPopup.IsOpen = false;

        private void AddPlacementApplication(object sender, RoutedEventArgs e)
        {
            if (_openingListViewModel.SelectedCandidate != null)
            {
                try
                {
                    _openingListViewModel.SubmitPlacement();
                    _placementListViewModel.PendingPlacements.Add(_openingListViewModel.PlacementToAdd);
                    NewApplicationPopup.IsOpen = false;
                    MessageBox.Show($"Placement Successfully Submitted");
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error Placement Submission\n{exception}");
                }
            }
        }



        #endregion

        #region Training Session
        private void BtnAddSession(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            var addSessionView = new AddSessionView(_trainingSessionListViewModel,_trainingSessionService,_courseService);
            addSessionView.Show();
        }
        private void BtnEditSession(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_trainingSessionListViewModel.SelectedTrainingSession != null)
            {
                var editSessionView = new EditSessionView(_trainingSessionListViewModel.SelectedTrainingSession, _trainingSessionService, _courseService);
                editSessionView.Show();
            }
        }

        private void BtnRemoveSession(object sender, RoutedEventArgs e)
        {
            if (_trainingSessionListViewModel.SelectedTrainingSession != null)
            {
                var res = MessageBox.Show($"Delete {_trainingSessionListViewModel.SelectedTrainingSession.SessionId}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _trainingSessionListViewModel.DeleteTrainingSession(_trainingSessionListViewModel.SelectedTrainingSession);
            }
        }

        private void BtnCandidatePopUp(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_trainingSessionListViewModel.SelectedTrainingSession == null) return;

            if (_trainingSessionListViewModel.SelectedTrainingSession.Capacity <= _trainingSessionListViewModel.RegisteredCandidates.Count)
                MessageBox.Show("Training Session is Full");
            else
            {
                _trainingSessionListViewModel.DisplayUnenrolledCandidate();
                CandidatePopUp.IsOpen = true;
            }

        }

        private void BtnEnrollCandidate(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_trainingSessionListViewModel.SelectedCandidate != null)
                {
                    CandidatePopUp.IsOpen = false;
                    MessageBox.Show($"Candidate Successfully Added");
                    _trainingSessionListViewModel.EnrollCandidate();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Candidate:\n{exception}");
            }
        }

        private void BtnCancelRegistration(object sender, RoutedEventArgs e) 
            => CandidatePopUp.IsOpen = false;


        private void BtnDropCandidate(object sender, RoutedEventArgs e)
        {
            if (_trainingSessionListViewModel.SelectedRegistration != null)
            {
                var res = MessageBox.Show($"Delete {_trainingSessionListViewModel.SelectedRegistration.CandidateName}?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _trainingSessionListViewModel.DropCandidate();
            }
        }


        private void BtnAddSchedule(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_trainingSessionListViewModel.SelectedTrainingSession != null)
            {
                var addScheduleView = new AddSessionScheduleView(_scheduleService, _trainingSessionService, _trainingSessionListViewModel);
                addScheduleView.Show();
            }
        }

        private void BtnEditSchedule(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            if (_trainingSessionListViewModel.SelectedTrainingSession != null)
            {
                var editScheduleView = new EditSessionScheduleView(_scheduleService, _trainingSessionListViewModel);
                editScheduleView.Show();
            }
        }

        private void BtnDeleteSession(object sender, RoutedEventArgs e)
        {
            if (_trainingSessionListViewModel.SelectedTrainingSession != null)
            {
                var res = MessageBox.Show($"Delete {_trainingSessionListViewModel.SelectedSchedule.ScheduleId}?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.No)
                    _trainingSessionListViewModel.DeleteSchedule();
            }
        }

        #endregion

        

        

        
        
        
        
        #region Placement
        private void BtnQualifyPlacement(object sender, RoutedEventArgs e)
        {
            if (_placementListViewModel.SelectedPendingPlacement != null)
            {
                try
                {
                    MessageBox.Show($"{_placementListViewModel.SelectedPendingPlacement.CandidateNumber} Successfully Qualified");
                    _placementListViewModel.QualifyPlacement();
                    _jobHistoryListViewModel.JobHistoryList.Add(_placementListViewModel.JobHistoryViewModel);
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error Qualifying Placement:\n{exception}");
                }
            }
        }

        

        #endregion

        
        #region Job History
        private void BtnEditJobHistory(object sender, RoutedEventArgs e)
        {
            if (_jobHistoryListViewModel.SelectedJobHistory!= null)
            {
                var editJobHistoryView = new EditJobHistoryView(_jobHistoryListViewModel.SelectedJobHistory, _jobHistoryService);
                editJobHistoryView.Show();
            }
           
        }

        private void AddJobHistory(object sender, RoutedEventArgs e)
        {
            var addJobHistoryView = new AddJobHistoryView(_jobHistoryService, _jobHistoryListViewModel);
            addJobHistoryView.Show();
        }
        #endregion

        

        
    }
}
