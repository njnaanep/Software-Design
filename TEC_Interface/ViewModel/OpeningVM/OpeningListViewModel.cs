using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using DataLayer.EfClasses;
using ServiceLayer;
using TEC_Interface.ViewModel.Placement;

namespace TEC_Interface.ViewModel.Opening
{
    public class OpeningListViewModel
    {
        private OpeningService _openingService;
        private OpeningViewModel _selectedOpening;

        public ObservableCollection<OpeningViewModel> OpeningList { get; set; }

        public ObservableCollection<OpeningPlacement> OpeningPlacements { get; set; }
            = new ObservableCollection<OpeningPlacement>();

        public OpeningViewModel SelectedOpening
        {
            get => _selectedOpening;
            set
            {
                _selectedOpening = value;
                if (_selectedOpening != null)
                {
                    DisplayOpeningPlacements(_selectedOpening.OpeningNumber);
                }
            }
        }

        private void DisplayOpeningPlacements(string openingNumber)
        {
            OpeningPlacements.Clear();

            var placements = new PlacementService(MainWindow.MainContext).GetOpeningPlacements(openingNumber)
                .Select(c => new OpeningPlacement(c));

            foreach (var application in placements) 
                OpeningPlacements.Add(application);
        }

        


        public OpeningListViewModel(OpeningService openingService)
        {
            _openingService = openingService;

            OpeningList = new ObservableCollection<OpeningViewModel>
                (_openingService.GetOpenings().Select( c => new OpeningViewModel(c)));


            if (OpeningList.Count > 0) 
                SelectedOpening = OpeningList.First();
        }

        public void SearchOpening(string searchString)
        {
            ClearOpeningData();

            var openings = _openingService.SearchOpenings(searchString);

            foreach (var opening in openings)
            {
                var openingViewModel = new OpeningViewModel(opening);

                OpeningList.Add(openingViewModel);
            }
        }

        private void ClearOpeningData()
        {
            OpeningList.Clear();
            OpeningPlacements.Clear();
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchOpening(_searchText);
            }
        }

        public void DeleteOpening(OpeningViewModel opening)
        {
            _openingService.DeleteOpening(opening.OpeningNumber);
            OpeningList.Remove(opening);
        }

        #region Placement Application
        public ObservableCollection<DataLayer.EfClasses.Candidate> CandidateList { get; set; }
         = new ObservableCollection<DataLayer.EfClasses.Candidate>();

        public void DisplayPotentialCandidate()
        {
            CandidateList.Clear();

            var candidates = new CandidateService(MainWindow.MainContext).GetQualifiedCandidate(SelectedOpening.QualificationId);

            foreach (var candidate in candidates) CandidateList.Add(candidate);
        }

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

            var candidates = new CandidateService(MainWindow.MainContext).SearchUnqualifiedCandidate(SelectedOpening.QualificationId,searchText);

            foreach (var candidate in candidates)
                CandidateList.Add(candidate);
        }

        public void SubmitPlacement()
        {
            PlacementToAdd = new DataLayer.EfClasses.Placement
            {
                CandidateNumber = SelectedCandidate.CandidateNumber,
                OpeningNumber = SelectedOpening.OpeningNumber,
                PlacementStatus = "Pending"
            };

            new PlacementService(MainWindow.MainContext).AddPlacement(PlacementToAdd);


            OpeningPlacements.Add(new OpeningPlacement(PlacementToAdd));
            CandidateList.Remove(SelectedCandidate);
        }

        public DataLayer.EfClasses.Placement PlacementToAdd { get; set; }
        #endregion
    }
 }