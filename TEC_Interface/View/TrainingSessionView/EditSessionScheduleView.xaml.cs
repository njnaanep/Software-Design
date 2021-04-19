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
    /// Interaction logic for EditSessionScheduleView.xaml
    /// </summary>
    public partial class EditSessionScheduleView : Window
    {
        private EditSessionScheduleViewModel _scheduleToEdit;

        public EditSessionScheduleView()
        {
            InitializeComponent();
        }

        public EditSessionScheduleView(ScheduleService scheduleService, TrainingSessionListViewModel trainingSessionListViewModel) : this()
        {
            _scheduleToEdit = new EditSessionScheduleViewModel(scheduleService, trainingSessionListViewModel.SelectedSchedule);
            DataContext = _scheduleToEdit;
        }

        private void BtnCancelScheduleEdit(object sender, RoutedEventArgs e) => Close();

        private void BtnScheduleSaveChange(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show($"Successfully Edited {_scheduleToEdit.ScheduleToEdit.SessionId}");
                _scheduleToEdit.Edit();
                Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Editing Schedule:\n{exception}");
            }
        }
    }
}
