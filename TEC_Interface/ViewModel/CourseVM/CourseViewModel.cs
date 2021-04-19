using System.ComponentModel;
using System.Runtime.CompilerServices;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.Course
{
    public class CourseViewModel :INotifyPropertyChanged
    {
        private string _courseId;
        private string _name;
        private string _description;
        private double _hours;

        
        public string CourseId
        {
            get => _courseId;
            internal set
            {
                _courseId = value;
                OnPropertyChanged(nameof(CourseId));
            }
        }

        public string CourseName
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(CourseName));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public double Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
            }
        }

        public CourseViewModel(DataLayer.EfClasses.Course course)
        {
            CourseId = course.CourseId;
            CourseName = course.CourseName;
            Description = course.CourseDescription;
            Hours = course.CourseHours;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}