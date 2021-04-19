using DataLayer.EfClasses;

namespace TEC_Interface.ViewModel.Candidate
{
    public class CandidateCertificationViewModel
    {
        public string CertificationId { get; set; }
        public string Qualification { get; set; }
        public string CertificationDate { get; set; }

        public CandidateCertificationViewModel(Certification qualification)
        { 
            Qualification = qualification.QualificationLink.QualificationCode;
            CertificationDate = qualification.CertificationDate.ToShortDateString();
            CertificationId = qualification.CertificationId;
        }


    }
}