using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.Placement
{
    public class PendingPlacementViewModel : INotifyPropertyChanged
    {
        private string _placementId;
        private string _candidateName;
        private string _openingId;

        public string PlacementId
        {
            get => _placementId;
            set
            {
                _placementId = value;
                OnPropertyChanged(nameof(PlacementId));
            }
        }

        public string CandidateName
        {
            get => _candidateName;
            set
            {
                _candidateName = value;
                OnPropertyChanged(nameof(CandidateName));
            }
        }

        public string OpeningId
        {
            get => _openingId;
            set
            {
                _openingId = value;
                OnPropertyChanged(nameof(OpeningId));
            }
        }
        
        public PendingPlacementViewModel(DataLayer.EfClasses.Placement placement)
        {
            PlacementId = placement.PlacementId;
            CandidateName = placement.CandidateLink.CandidateFullName;
            OpeningId = placement.OpeningNumber;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }
    }
}