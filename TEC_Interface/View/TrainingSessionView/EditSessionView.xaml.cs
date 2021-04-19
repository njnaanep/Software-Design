using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
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
using TEC_Interface.ViewModel.TrainingSession;
using TEC_Interface.ViewModel.TrainingSessionVM;

namespace TEC_Interface.View.TrainingSession
{
    /// <summary>
    /// Interaction logic for EditSession.xaml
    /// </summary>
    public partial class EditSessionView : Window
    {
        private EditSessionViewModel _sessionToEdit;

        public EditSessionView()
        {
            InitializeComponent();
        }


        public EditSessionView(TrainingSessionViewModel sessionToEdit, TrainingSessionService sessionService, CourseService courseService) : this()
        {
            _sessionToEdit = new EditSessionViewModel(sessionService, courseService, sessionToEdit);
            DataContext = _sessionToEdit;
        }

        private void SaveSessionChange(object sender, RoutedEventArgs e)
        {
            try
            {
                _sessionToEdit.Edit();
                MessageBox.Show($"Successfully Edited {_sessionToEdit.SessionToEdit.SessionId}");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Editing Training Session{exception}");
            }
        }

        private void AcceptDecimal(object sender, TextCompositionEventArgs e)
            => e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);

        private void AcceptInt(object sender, TextCompositionEventArgs e)
            => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);


        private void CancelSessionChange(object sender, RoutedEventArgs e) => Close();
    }
}
