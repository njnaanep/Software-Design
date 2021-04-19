using System;
using System.Collections.Generic;
using System.Security.AccessControl;
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
using TEC_Interface.ViewModel.Candidate;
using TEC_Interface.ViewModel.CandidateVM;

namespace TEC_Interface.View
{
    /// <summary>
    /// Interaction logic for EditCandidate.xaml
    /// </summary>
    public partial class EditCandidateView : Window
    {
        public EditCandidateView()
        {
            InitializeComponent();
        }

        private EditCandidateViewModel _editCandidate;

        public EditCandidateView(CandidateViewModel editCandidate, CandidateService candidateService)  : this()
        {
            _editCandidate = new EditCandidateViewModel(editCandidate, candidateService);
            DataContext = _editCandidate;
        }

        private void BtnSaveChange(object sender, RoutedEventArgs e)
        {
            try
            {
                _editCandidate.Edit();
                MessageBox.Show($"Candidate Successfully Edited:");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error editing the the data:\n{exception}");
            }
            
            
        }

        private void BtnCancelChange(object sender, RoutedEventArgs e) => Close();

        private void AcceptInt(object sender, TextCompositionEventArgs e) 
            => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }
}
