using System.ComponentModel;
using System.Runtime.CompilerServices;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.Qualification
{
    public class QualificationViewModel : INotifyPropertyChanged {
        private string _qualificationCode;
        private string _qualificationDescription;
        private string _qualificationId;

        public string QualificationId
        {
            get => _qualificationId;
            internal set
            {
                _qualificationId = value;
                OnPropertyChanged(nameof(QualificationId));
            }
        }

        public string QualificationCode
        {
            get => _qualificationCode;
            set
            {
                _qualificationCode = value;
                OnPropertyChanged(nameof(QualificationCode));
            }
        }

        public string QualificationDescription
        {
            get => _qualificationDescription;
            set
            {
                _qualificationDescription = value;
                OnPropertyChanged(nameof(QualificationDescription));
            }
        }

        public QualificationViewModel(DataLayer.EfClasses.Qualification qualification)
        {
            QualificationId = qualification.QualificationId;
            QualificationCode = qualification.QualificationCode;
            QualificationDescription = qualification.Description;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}