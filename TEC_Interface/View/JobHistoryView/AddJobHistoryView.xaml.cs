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
using ServiceLayer;
using TEC_Interface.ViewModel.Company;
using TEC_Interface.ViewModel.Course;

namespace TEC_Interface.View.JobHistoryView
{
    /// <summary>
    /// Interaction logic for AddJobHistoryView.xaml
    /// </summary>
    public partial class AddJobHistoryView : Window
    {
        private AddJobHistoryViewModel _jobHistoryToAdd;
        private readonly JobHistoryListViewModel _jobHistoryListViewModel;

        public AddJobHistoryView()
        {
            InitializeComponent();
        }

        public AddJobHistoryView(JobHistoryService jobHistoryService, JobHistoryListViewModel jobHistoryListViewModel) : this()
        {
            _jobHistoryListViewModel = jobHistoryListViewModel;
            _jobHistoryToAdd = new AddJobHistoryViewModel(jobHistoryService);
            DataContext = _jobHistoryToAdd;
        }

        private void OpenCandidatePopup(object sender, RoutedEventArgs e) => CandidatePopup.IsOpen = true;

        private void OpenCompanyPopup(object sender, RoutedEventArgs e) => CompanyPopup.IsOpen = true;

        private void Close(object sender, RoutedEventArgs e) => Close();

        private void AddJobHistory(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show($"Successfully Added Job History");
                _jobHistoryToAdd.Add();
                _jobHistoryListViewModel.JobHistoryList.Add(_jobHistoryToAdd.AssociatedJobHistory);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error adding Job History:\n{exception}");

            }
        }
    }
}
