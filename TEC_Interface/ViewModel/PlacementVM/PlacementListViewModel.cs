using System;
using System.Collections.ObjectModel;
using System.Linq;
using DataLayer.EfClasses;
using ServiceLayer;
using TEC_Interface.ViewModel.Company;
using TEC_Interface.ViewModel.TrainingSession;

namespace TEC_Interface.ViewModel.Placement
{
    public class PlacementListViewModel
    {
        private PlacementService _placementService;

        

        private string _searchText;

        public ObservableCollection<PlacementViewModel> PlacementList{ get; set; }

        public ObservableCollection<DataLayer.EfClasses.Placement> PendingPlacements{ get; set; }

        public PlacementViewModel SelectedPlacement { get; set; }

        //public  PlacementListViewModel(PlacementService placementService, JobHistoryService jobHistoryService,JobHistoryListViewModel jobHistoryListViewModel)
        //{
        //    _placementService = placementService;
        //    _historyListViewModel = jobHistoryListViewModel;
        //    _jobHistoryService = jobHistoryService;

        //    PlacementList = new ObservableCollection<PlacementViewModel>(
        //        _placementService.GetPlacements().Select( c => new PlacementViewModel(c)));


        //    PendingPlacements = new ObservableCollection<DataLayer.EfClasses.Placement>(
        //        _placementService.GetPendingPlacements().Select(c =>c ));
        //}

        public PlacementListViewModel(PlacementService placementService)
        {
            _placementService = placementService;

            PlacementList = new ObservableCollection<PlacementViewModel>(
                _placementService.GetPlacements().Select(c => new PlacementViewModel(c)));


            PendingPlacements = new ObservableCollection<DataLayer.EfClasses.Placement>(
                _placementService.GetPendingPlacements().Select(c => c));

            if (PlacementList.Count > 0) 
                SelectedPlacement = PlacementList.First();
        }

        public void SearchPlacements(string searchString)
        {
            PlacementList.Clear();

            var placements = _placementService.SearchPlacements(searchString);

            foreach (var placement in placements)
            {
                var openingViewModel = new PlacementViewModel(placement);

                PlacementList.Add(openingViewModel);
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchPlacements(_searchText);
            }
        }

        public void QualifyPlacement()
        {
            _placementService.QualifyPlacement(SelectedPendingPlacement.PlacementId);
            PlacementList.Add(new PlacementViewModel(SelectedPendingPlacement));


            var jobHistoryToAdd = new JobHistory
            {
                CandidateNumber = SelectedPendingPlacement.CandidateNumber,
                CompanyId = SelectedPendingPlacement.OpeningLink.CompanyLink.CompanyId,
                WorkedFrom = SelectedPendingPlacement.OpeningLink.StartingDate,
            };

            new JobHistoryService(MainWindow.MainContext).AddJobHistory(jobHistoryToAdd);

            JobHistoryViewModel = new JobHistoryViewModel(jobHistoryToAdd);


            PendingPlacements.Remove(SelectedPendingPlacement);
        }

        public JobHistoryViewModel JobHistoryViewModel { get; set; }

        public DataLayer.EfClasses.Placement SelectedPendingPlacement { get; set; }    
    }
}