using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

namespace TEC_Interface.View
{
    /// <summary>
    /// Interaction logic for AddCandidates.xaml
    /// </summary>
    public partial class AddCandidatesView : Window
    {
        private readonly CandidateListViewModel _candidateListViewModel;
        private readonly CandidateService _candidateService;

        public AddCandidatesView()
        {
            InitializeComponent();
        }

        private AddCandidateViewModel _candidateToAdd;

        public AddCandidatesView(CandidateListViewModel candidateListViewModel, CandidateService candidateService) : this()
        {
            _candidateListViewModel = candidateListViewModel;
            _candidateService = candidateService;
            _candidateToAdd = new AddCandidateViewModel(candidateService);
            DataContext = _candidateToAdd;
        }

        private void BtnAddCandidate(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                _candidateToAdd.Add();
                _candidateListViewModel.CandidateList.Add(_candidateToAdd.AssociatedCandidate);
                MessageBox.Show($"Successfully Added Candidate");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Candidate:\n{exception}");
            }
            
        }

        
        private void CancelAdd(System.Object sender, System.Windows.RoutedEventArgs e) => Close();

        private void AcceptInt(object sender, TextCompositionEventArgs e) 
            => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }
}
