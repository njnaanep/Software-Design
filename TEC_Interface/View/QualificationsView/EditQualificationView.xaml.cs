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
using TEC_Interface.ViewModel.Qualification;

namespace TEC_Interface.View.Qualifications
{
    /// <summary>
    /// Interaction logic for EditQualification.xaml
    /// </summary>
    public partial class EditQualificationView : Window
    {
        public EditQualificationView()
        {
            InitializeComponent();
        }

        private EditQualificationViewModel _editQualification;

        public EditQualificationView(QualificationViewModel editQualification, QualificationService qualificationService) : this()
        {
            _editQualification = new EditQualificationViewModel(editQualification, qualificationService);
            DataContext = _editQualification;
        }

        private void BtnSaveQualificationChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                _editQualification.Edit();
                MessageBox.Show($"Qualification Successfully Edited");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"There was an error editing the qualification:\n{exception}");
            }
        }

        private void BtnCancelChange(object sender, RoutedEventArgs e) => Close();
    }
}
