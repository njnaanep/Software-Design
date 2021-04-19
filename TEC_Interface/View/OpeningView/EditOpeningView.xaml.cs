using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ServiceLayer;
using TEC_Interface.ViewModel.CandidateVM;
using TEC_Interface.ViewModel.Opening;

namespace TEC_Interface.View.OpeningView
{
    /// <summary>
    /// Interaction logic for EditOpening.xaml
    /// </summary>
    public partial class EditOpeningView : Window
    {
        private EditOpeningViewModel _openingToEdit;

        public EditOpeningView()
        {
            InitializeComponent();
        }

        public EditOpeningView(OpeningViewModel editOpening, OpeningService openingService, CompanyService companyService, QualificationService qualificationService ) : this()
        {
            _openingToEdit = new EditOpeningViewModel(editOpening, openingService, companyService, qualificationService);
            DataContext = _openingToEdit;
        }

        private void BtnSaveOpeningChange(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                _openingToEdit.Edit();
                MessageBox.Show($"Successfully Edited {_openingToEdit.OpeningToEdit.OpeningNumber}");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Editing Opening:\b{exception}");
            }
        }

        private void AcceptDecimal(object sender, TextCompositionEventArgs e)
            => e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);


        private void BtnCancelOpeningChange(System.Object sender, System.Windows.RoutedEventArgs e) => Close();

        private void SelectCompany(System.Object sender, System.Windows.RoutedEventArgs e) => CompanyPopup.IsOpen = true;

        private void SelectQualification(object sender, RoutedEventArgs e) => QualificationPopUp.IsOpen = true;
    }
}
