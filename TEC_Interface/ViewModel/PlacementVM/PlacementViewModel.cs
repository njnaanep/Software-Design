using System.ComponentModel;
using System.Runtime.CompilerServices;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.Placement
{
    public class PlacementViewModel : INotifyPropertyChanged
    {
        private string _placementId;
        private string _openingNum;
        private string _candidateName;
        private string _company;
        private string _status;
        private string _companyId;
        private string _candidateNum;

        public string PlacementId
        {
            get => _placementId;
            internal set
            {
                _placementId = value;
                OnPropertyChanged(nameof(PlacementId));
            }
        }

        public string OpeningNum
        {
            get => _openingNum;
            set
            {
                _openingNum = value;
                OnPropertyChanged(nameof(OpeningNum));
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

        public string Company
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged(nameof(Company));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string CompanyId
        {
            get => _companyId;
            set
            {
                _companyId = value;
                OnPropertyChanged(nameof(CompanyId));
            }
        }

        public string CandidateNum
        {
            get => _candidateNum;
            set
            {
                _candidateNum = value;
                OnPropertyChanged(nameof(CandidateNum));
            }
        }

        public PlacementViewModel(DataLayer.EfClasses.Placement placement)
        {
            PlacementId = placement.PlacementId;
            OpeningNum = placement.OpeningNumber;
            CandidateName = placement.CandidateLink.CandidateFullName;
            Company = placement.OpeningLink.CompanyLink.CompanyName;
            Status = placement.PlacementStatus;
            CompanyId = placement.OpeningLink.CompanyLink.CompanyId;
            CandidateNum = placement.CandidateNumber;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}