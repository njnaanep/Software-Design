using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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
    /// Interaction logic for AddSession.xaml
    /// </summary>
    public partial class AddSessionView : Window
    {
        private TrainingSessionListViewModel _trainingSessionListViewModel;
        private TrainingSessionService _trainingSessionService;
        private CourseService _courseService;
        private AddTrainingSessionViewModel _sessionToAdd;

        public AddSessionView()
        {
            InitializeComponent();
        }

        public AddSessionView(TrainingSessionListViewModel trainingSessionListViewModel, TrainingSessionService trainingSessionService, CourseService courseService) :this()
        {
            _trainingSessionListViewModel = trainingSessionListViewModel;
            _trainingSessionService = trainingSessionService;
            _courseService = courseService;

            _sessionToAdd = new AddTrainingSessionViewModel(trainingSessionService, courseService);
            DataContext = _sessionToAdd;
        }


        private void BtnAddSession(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                _sessionToAdd.Add();
                _trainingSessionListViewModel.SessionList.Add(_sessionToAdd.AssociatedSession);
                MessageBox.Show($"Successfully Added Training Session");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Training Session:\n{exception}");
            }
        }

        private void AcceptDecimal(object sender, TextCompositionEventArgs e)
            => e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);

        private void AcceptInt(object sender, TextCompositionEventArgs e)
            => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);


        private void BtnCancelAddSession(System.Object sender, System.Windows.RoutedEventArgs e) => Close();
    }
}
