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
using TEC_Interface.ViewModel.Company;

namespace TEC_Interface.View.JobHistory
{
    /// <summary>
    /// Interaction logic for AddJobHistory.xaml
    /// </summary>
    public partial class EditJobHistoryView : Window
    {
        private EditJobHistoryViewModel _editJobHistory;

        public EditJobHistoryView()
        {
            InitializeComponent();
        }

        public EditJobHistoryView(JobHistoryViewModel historyToChange, JobHistoryService historyService) : this()
        {
            _editJobHistory = new EditJobHistoryViewModel(historyToChange, historyService);
            DataContext = _editJobHistory;
        }

        private void BtnSaveHistoryChange(object sender, RoutedEventArgs e)
        {
            try
            {
                _editJobHistory.Edit();
                MessageBox.Show($"Save Changes");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Editing Job History:\t{exception}");
            }
        }

        private void CancelChange(object sender, RoutedEventArgs e) => Close();
        private void AcceptInt(object sender, TextCompositionEventArgs e)
            => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }
}
