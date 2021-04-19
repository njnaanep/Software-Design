using System.ComponentModel;
using System.Runtime.CompilerServices;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.Company
{
    public class CompanyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _companyId;
        private string _companyName;
        private string _companyAddress;


        public string CompanyId
        {
            get => _companyId;
            internal set
            {
                _companyId = value;
                OnPropertyChanged(nameof(CompanyId));
            }
        }

        public string CompanyName
        {
            get=> _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        public string CompanyAddress    
        {
            get => _companyAddress;
            set
            {
                _companyAddress = value;
                OnPropertyChanged(nameof(CompanyAddress));
            }
        }

        public CompanyViewModel(string companyId, string companyName, string companyAddress)
        {
            CompanyId = companyId;
            CompanyName = companyName;
            CompanyAddress = companyAddress;
        }

        public CompanyViewModel(DataLayer.EfClasses.Company company)
        {
            CompanyId = company.CompanyId;
            CompanyName = company.CompanyName;
            CompanyAddress = company.CompanyAddress;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}