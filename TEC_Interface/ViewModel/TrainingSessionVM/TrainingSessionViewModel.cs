using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.TrainingSession
{
    public class TrainingSessionViewModel : INotifyPropertyChanged
    {
        private string _sessionId;
        private string _courseName;
        private double _fee;
        private uint _capacity;

        public string SessionId
        {
            get => _sessionId;
            internal set
            {
                _sessionId = value;
                OnPropertyChanged(nameof(SessionId));
            }
        }
        public string CourseName
        {
            get => _courseName;
            set
            {
                _courseName = value;
                OnPropertyChanged(nameof(CourseName));
            }
        }
        public double Fee  
        {
            get => _fee;
            set
            {
                _fee = value;
                OnPropertyChanged(nameof(Fee));
            }
        }

        public uint Capacity
        {
            get => _capacity;
            set
            {
                _capacity = value;
                OnPropertyChanged(nameof(Capacity));
            }
        }


        public TrainingSessionViewModel(DataLayer.EfClasses.TrainingSession session)
        {
            SessionId = session.SessionId;
            CourseName = session.CourseLink.CourseName;
            Fee = session.SessionFee;
            Capacity = session.SessionCapacity;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}