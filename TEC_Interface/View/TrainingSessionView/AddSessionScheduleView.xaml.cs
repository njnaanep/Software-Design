using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataLayer.EfCode.Configuration;
using ServiceLayer;
using TEC_Interface.ViewModel.TrainingSession;
using TEC_Interface.ViewModel.TrainingSessionVM;

namespace TEC_Interface.View.TrainingSessionView
{
    /// <summary>
    /// Interaction logic for AddSessionScheduleView.xaml
    /// </summary>
    public partial class AddSessionScheduleView : Window
    {
        private AddSessionScheduleViewModel _scheduleToAdd;
        private TrainingSessionListViewModel _sessionListViewModel;

        public AddSessionScheduleView()
        {
            InitializeComponent();
        }

        public AddSessionScheduleView(ScheduleService scheduleService, 
            TrainingSessionService sessionService,
            TrainingSessionListViewModel sessionList) :this()
        {
            _sessionListViewModel = sessionList;

            _scheduleToAdd = new AddSessionScheduleViewModel(scheduleService, sessionList.SelectedTrainingSession);
            DataContext = _scheduleToAdd;
        }

        private void BtnAddSchedule(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show($"Successfully Added Schedule");
                _scheduleToAdd.Add();
                _sessionListViewModel.SessionSchedules.Add(_scheduleToAdd.AssociatedSchedule);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Schedule:\n{exception}");
            }
        }

        private void BtnCancelAddSchedule(System.Object sender, System.Windows.RoutedEventArgs e) => Close();
    }
}
