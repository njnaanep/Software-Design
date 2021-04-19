using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ServiceLayer;
using TEC_Interface.ViewModel.Course;

namespace TEC_Interface.View.Course
{
    /// <summary>
    /// Interaction logic for AddCourse.xaml
    /// </summary>
    public partial class AddCourseView : Window
    {
        public AddCourseView()
        {
            InitializeComponent();
        }

        private CourseService _courseService;
        private CourseListViewModel _courseListViewModel;
        private AddCourseViewModel _courseToAdd;

        public AddCourseView(CourseService courseService, CourseListViewModel courseListViewModel) : this()
        { 
            _courseService = courseService;
            _courseListViewModel = courseListViewModel;
            _courseToAdd = new AddCourseViewModel(courseService);
            DataContext = _courseToAdd;
        }

        private void BtnAddCourse(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                _courseToAdd.Add();
                _courseListViewModel.CourseList.Add(_courseToAdd.AssociatedCourse);
                MessageBox.Show("Successfully Added Course");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Course:\n{exception}");
            }
        }

        private void BtnCancelAddCourse(System.Object sender, System.Windows.RoutedEventArgs e) => Close();

        private void AcceptDecimal(object sender, TextCompositionEventArgs e) 
            => e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);
    }
}
