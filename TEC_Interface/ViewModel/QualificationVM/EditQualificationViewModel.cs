using System.Runtime.InteropServices;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Qualification
{
    public class EditQualificationViewModel
    {
        public QualificationViewModel QualificationToEdit { get; set; }
        private QualificationService _qualificationService;

        public EditQualificationViewModel(QualificationViewModel qualificationToEdit, QualificationService qualificationService)
        {
            _qualificationService = qualificationService;
            QualificationToEdit = qualificationToEdit;

            QualificationId = qualificationToEdit.QualificationId;
            CopyEditableFields(qualificationToEdit);
        }

        private void CopyEditableFields(QualificationViewModel qualificationToEdit)
        {
            Code = qualificationToEdit.QualificationCode;
            Description = qualificationToEdit.QualificationDescription;
        }

        public string QualificationId { get; }

        public string Code { get; set;  }
        public string Description { get; set; }

        public void Edit()
        {
            var updatedQualification = new DataLayer.EfClasses.Qualification
            {
                QualificationId =  QualificationId,
                QualificationCode = Code,
                Description = Description
            };

            QualificationToEdit.QualificationCode = Code;
            QualificationToEdit.QualificationDescription = Description;

            _qualificationService.UpdateQualification(updatedQualification);
        }
    }
}