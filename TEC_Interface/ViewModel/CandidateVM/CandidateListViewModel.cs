using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using DataLayer.EfClasses;
using DataLayer.EfCode.Configuration;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Candidate
{
    public class CandidateListViewModel
    {
        private CandidateService _candidateService;
        private CandidateViewModel _selectedCandidate;
        private string _searchText;

        public CandidateListViewModel(CandidateService candidateService)
        {
            _candidateService = candidateService;

            CandidateList = new ObservableCollection<CandidateViewModel>(
                _candidateService.GetCandidate().Select(c =>
                    new CandidateViewModel(c)));

            if (CandidateList.Count > 0) 
                SelectedCandidate = CandidateList.First();
        }
        
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchCandidate(_searchText);
            }
        }

        public CandidateViewModel SelectedCandidate
        {
            get => _selectedCandidate;
            set
            {
                _selectedCandidate = value;
                if (_selectedCandidate != null) 
                    DisplayCandidateDataGrid(_selectedCandidate.CandidateNumber);
            }
        }

        #region STORAGE
        public ObservableCollection<CandidateViewModel> CandidateList { get; set; }

        public ObservableCollection<CandidatePlacementViewModel> CandidatePlacementsList { get; set; } =
            new ObservableCollection<CandidatePlacementViewModel>();

        public ObservableCollection<CandidateJobHistoryViewModel> CandidateJobHistoryList { get; set; } =
            new ObservableCollection<CandidateJobHistoryViewModel>();

        public ObservableCollection<CandidateCertificationViewModel> CandidatesCertificationList { get; set; } = 
            new ObservableCollection<CandidateCertificationViewModel>();

        public ObservableCollection<CandidateRegistrationViewModel> CandidateRegistrationList { get; set; } =
            new ObservableCollection<CandidateRegistrationViewModel>();

        #endregion

        #region CandidateDataGrid
        public void DisplayCandidateDataGrid(string candidateNum)
        {
            DisplayCandidateJobHistory(candidateNum);
            DisplayCandidatePlacement(candidateNum);
            DisplayCandidateCertification(candidateNum);
            DisplayCandidateRegistration(candidateNum);
        }

        public void DisplayCandidateJobHistory(string candidateNum)
        {
            CandidateJobHistoryList.Clear();

            var jobHistories = 
                JobHistoryService.GetCandidateJobHistory(candidateNum)
                .Select(c => new CandidateJobHistoryViewModel(c));

            foreach (var history in jobHistories) 
                CandidateJobHistoryList.Add(history);
        }

        public void DisplayCandidatePlacement(string candidateId)
        {
            CandidatePlacementsList.Clear();
            var placements = 
                new PlacementService(MainWindow.MainContext).GetCandidatePlacements(candidateId)
                .Select(c => new CandidatePlacementViewModel(c));

            foreach (var placement in placements) 
                CandidatePlacementsList.Add(placement);
        }

        public void DisplayCandidateCertification(string candidateNum)
        {
            CandidatesCertificationList.Clear();

            var certificates = CertificationService.GetCandidateCertifications(candidateNum)
                .Select(c => new CandidateCertificationViewModel(c));

            foreach (var certificate in certificates) 
                CandidatesCertificationList.Add(certificate);
        }

        public void DisplayCandidateRegistration(string candidateNum)
        {
            CandidateRegistrationList.Clear();

            var registrations = RegistrationService.GetCandidateRegistration(candidateNum)
                .Select(c => new CandidateRegistrationViewModel(c));

            foreach (var registration in registrations) 
                CandidateRegistrationList.Add(registration);

        }

        public void ClearDisplay()
        {
            CandidateList.Clear();
            CandidateJobHistoryList.Clear();
            CandidatePlacementsList.Clear();
            CandidatesCertificationList.Clear();
            CandidateRegistrationList.Clear();
        }

        #endregion


        public void SearchCandidate(string searchString)
        {
            ClearDisplay(); var candidates = _candidateService.SearchCandidate(searchString);


            foreach (var candidate in candidates)
                CandidateList.Add(new CandidateViewModel(candidate));
        }

        public void DeleteCandidate(CandidateViewModel candidate)
        {
            _candidateService.DeleteCandidate(candidate.CandidateNumber);
            CandidateList.Remove(candidate);
        }

        

    }
}