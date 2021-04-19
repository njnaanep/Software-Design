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
using Exception = System.Exception;

namespace TEC_Interface.View.Course
{
    /// <summary>
    /// Interaction logic for EditCourse.xaml
    /// </summary>
    public partial class EditCourseView : Window
    {
        public EditCourseView()
        {
            InitializeComponent();
        }

        private EditCourseViewModel _editCourse;
        public EditCourseView(CourseViewModel editCourse, CourseService courseService) : this()
        {
            _editCourse = new EditCourseViewModel(editCourse,courseService);
            DataContext = _editCourse;
        }

        private void BtnSaveCourseChange(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                _editCourse.Edit();
                MessageBox.Show($"Course Successfully Edited");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error editing the course:\n{exception}");
            }
        }

        private void BtnCancelEdit(System.Object sender, System.Windows.RoutedEventArgs e) => Close();

        private void AcceptDecimal(object sender, TextCompositionEventArgs e) 
            => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }
}
