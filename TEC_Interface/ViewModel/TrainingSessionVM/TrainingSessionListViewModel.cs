using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using DataLayer.EfClasses;
using DataLayer.EfCode.Configuration;
using ServiceLayer;
using TEC_Interface.ViewModel.TrainingSessionVM;

namespace TEC_Interface.ViewModel.TrainingSession
{
    public class TrainingSessionListViewModel
    {
        private TrainingSessionService _trainingSessionService;
        private string _searchText;
        private TrainingSessionViewModel _selectedTrainingSession;

        private string _searchCandidateString;

        #region STORAGE
        public ObservableCollection<TrainingSessionViewModel> SessionList { get; set; }
        public ObservableCollection<TrainingSessionCandidate> RegisteredCandidates { get; set; }
            = new ObservableCollection<TrainingSessionCandidate>();
        public ObservableCollection<TrainingSessionSchedule> SessionSchedules { get; set; }
            = new ObservableCollection<TrainingSessionSchedule>();

        public ObservableCollection<DataLayer.EfClasses.Candidate> CandidateList { get; set; } 
            = new ObservableCollection<DataLayer.EfClasses.Candidate>();


        #endregion

        public TrainingSessionViewModel SelectedTrainingSession
        {
            get => _selectedTrainingSession;
            set
            {
                _selectedTrainingSession = value;
                if (_selectedTrainingSession != null) 
                    DisplaySessionDataGrid(_selectedTrainingSession.SessionId);
            }
        }



        #region DataGrid
        private void DisplaySessionDataGrid(string sessionId)
        {
            DisplaySessionCandidates(sessionId);
            DisplaySessionSchedule(sessionId);
            DisplayPossibleCandidate(sessionId);
        }

        private void DisplayPossibleCandidate(string sessionId)
        {
            CandidateList.Clear();

            var candidates = new CandidateService(MainWindow.MainContext).GetUnenrolledCandidates(sessionId);

            foreach (var candidate in candidates) 
                CandidateList.Add(candidate);
        }

        private void DisplaySessionSchedule(string sessionId)
        {
            SessionSchedules.Clear();

            var schedules = ScheduleService.GetTrainingSchedule(sessionId)
                .Select(c => new TrainingSessionSchedule(c));

            foreach (var schedule in schedules) 
                SessionSchedules.Add(schedule);
        }


        private void DisplaySessionCandidates(string sessionId)
        {
            RegisteredCandidates.Clear();

            var candidates = RegistrationService.GetTrainingRegistration(sessionId)
                .Select(c => new TrainingSessionCandidate(c));

            foreach (var candidate in candidates) 
                RegisteredCandidates.Add(candidate);

        }
        #endregion

        public TrainingSessionListViewModel(TrainingSessionService trainingSessionService)
        {
            _trainingSessionService = trainingSessionService;

            SessionList = new ObservableCollection<TrainingSessionViewModel>(
                _trainingSessionService.GetTrainingSessions().Select(c =>
                    new TrainingSessionViewModel(c)));
            
            if (SessionList.Count > 0) SelectedTrainingSession = SessionList.First();
        }

        
        public void SearchTrainingSession(string searchString)
        {
            ClearDataGrid();

            var sessions = _trainingSessionService.GetTrainingSessions()
                .Where(c => c.CourseLink.CourseName.Contains(searchString));

            foreach (var trainingSession in sessions)
            {
                var sessionViewModel = new TrainingSessionViewModel(trainingSession);

                SessionList.Add(sessionViewModel);
            }
        }

        private void ClearDataGrid()
        {
            SessionList.Clear();
            RegisteredCandidates.Clear();
            SessionSchedules.Clear();
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchTrainingSession(_searchText);
            }
        }

        public void DeleteTrainingSession(TrainingSessionViewModel session)
        {
            _trainingSessionService.DeleteTrainingSessions(session.SessionId);
            SessionList.Remove(session);
        }


        #region Registration
        public void EnrollCandidate()
        {
            var newRegistration = new RegisteredCandidate
            {
                CandidateNumber = SelectedCandidate.CandidateNumber,
                RegisteredDate = DateTime.Now,
                SessionId = SelectedTrainingSession.SessionId
            };


            new RegistrationService(MainWindow.MainContext).AddRegistration(newRegistration);


            DisplaySessionCandidates(SelectedTrainingSession.SessionId);
            CandidateList.Remove(SelectedCandidate);
        }

        public void DisplayUnenrolledCandidate()
        {
            CandidateList.Clear();

            var candidates = new CandidateService(MainWindow.MainContext).GetUnenrolledCandidates(SelectedTrainingSession.SessionId);

            foreach (var candidate in candidates) 
                CandidateList.Add(candidate);
        }

        public DataLayer.EfClasses.Candidate SelectedCandidate { get; set; }

        public TrainingSessionCandidate SelectedRegistration { get; set; }

        public void DropCandidate()
        {
            new RegistrationService(MainWindow.MainContext).DeleteRegistration(SelectedRegistration.RegistrationId);
            RegisteredCandidates.Remove(SelectedRegistration);
        }

        public string SearchCandidateString
        {
            get => _searchCandidateString;
            set
            {
                _searchCandidateString = value;
                SearchCandidate(_searchCandidateString);
            }
        }

        private void SearchCandidate(string search)
        {
            CandidateList.Clear();

            var unenrolledCandidates =
                new CandidateService(MainWindow.MainContext).SearchUnenrolledCandidates(SelectedTrainingSession.SessionId, search);
            
            foreach (var candidate in unenrolledCandidates) 
                CandidateList.Add(candidate);
        }

        #endregion

        public TrainingSessionSchedule SelectedSchedule { get; set; }

        public void DeleteSchedule()
        {
            new ScheduleService(MainWindow.MainContext).DeleteSchedule(SelectedSchedule.ScheduleId);
            SessionSchedules.Remove(SelectedSchedule);
        }



    }
}