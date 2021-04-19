using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ServiceLayer;
using TEC_Interface.ViewModel.Opening;
using TEC_Interface.ViewModel.OpeningVM;

namespace TEC_Interface.View.OpeningView
{
    /// <summary>
    /// Interaction logic for AddOpening.xaml
    /// </summary>
    public partial class AddOpeningView : Window
    {
        private readonly OpeningListViewModel _openingListViewModel;
        private readonly OpeningService _openingService;
        private readonly CompanyService _companyService;
        private readonly QualificationService _qualificationService;
        private AddOpeningViewModel _openingToAdd;

        public AddOpeningView()
        {
            InitializeComponent();
        }

        public AddOpeningView(OpeningListViewModel openingListViewModel, 
            OpeningService openingService, 
            CompanyService companyService, 
            QualificationService qualificationService) :this()
        {
            _openingListViewModel = openingListViewModel;
            _openingService = openingService;
            _companyService = companyService;
            _qualificationService = qualificationService;

            _openingToAdd = new AddOpeningViewModel(_openingService, _companyService, _qualificationService);
            DataContext = _openingToAdd;
        }

        private void BtnCancelAddOpening(System.Object sender, System.Windows.RoutedEventArgs e) => Close();

        private void BtnAddOpening(System.Object sender, System.Windows.RoutedEventArgs e)
        {

            try
            {
                MessageBox.Show($"Successfully Added Opening");
                _openingToAdd.Add();
                _openingListViewModel.OpeningList.Add(_openingToAdd.AssociatedOpening);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error Adding Opening:\n{exception}");
            }

           
        }

        private void AcceptDecimal(object sender, TextCompositionEventArgs e)
            => e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);

        private void OpenCompanyPopup(object sender, RoutedEventArgs e) => CompanyPopup.IsOpen = true;

        private void OpenQualificationPopup(object sender, RoutedEventArgs e) => QualificationPopup.IsOpen = true;
    }
}
