using System.ComponentModel;
using System.Runtime.CompilerServices;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.Opening
{
    public class OpeningViewModel : INotifyPropertyChanged
    {
        private string _openingNumber;
        private string _companyName;
        private string _qualificationCode;
        private string _startingDate;
        private string _anticipatedEndDate;
        private double _hourlyPay;
        private string _qualificationId;

        public string OpeningNumber
        {
            get=> _openingNumber;
            internal set
            {
                _openingNumber = value;
                OnPropertyChanged(nameof(OpeningNumber));
            }
        }

        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        public string QualificationId
        {
            get => _qualificationId;
            set
            {
                _qualificationId = value;
                OnPropertyChanged(nameof(QualificationId));
            }
        }

        public string Qualification
        {
            get => _qualificationCode;
            set
            {
                _qualificationCode = value;
                OnPropertyChanged(nameof(Qualification));
            }
        }

        public string StartingDate
        {
            get => _startingDate;
            set
            {
                _startingDate = value;
                OnPropertyChanged(nameof(StartingDate));
            }
        }

        public string EndDate
        {
            get => _anticipatedEndDate;
            set
            {
                _anticipatedEndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public double HourlyPay
        {
            get => _hourlyPay;
            set
            {
                _hourlyPay = value;
                OnPropertyChanged(nameof(HourlyPay));
            }
        }

        public OpeningViewModel(DataLayer.EfClasses.Opening opening)
        {
            OpeningNumber = opening.OpeningNumber;
            CompanyName = opening.CompanyLink.CompanyName;
            QualificationId = opening.QualificationId;
            Qualification = opening.QualificationLink.QualificationCode;
            StartingDate = opening.StartingDate.ToShortDateString();
            EndDate = opening.AnticipatedEndDate.ToShortDateString();
            HourlyPay = opening.HourlyPay;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}