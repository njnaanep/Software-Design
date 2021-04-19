using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using DataLayer.EfClasses;

namespace TEC_Interface.ViewModel.Qualification
{
    public class QualificationOpeningViewModel
    {
        public string OpeningId { get; set; }
        public string CompanyName { get; set; }
        public string StartingDate { get; set; }
        public string EndDate { get; set; }
        public double HourlyPay { get; set; }

        public QualificationOpeningViewModel(DataLayer.EfClasses.Opening opening)
        {
            OpeningId = opening.OpeningNumber;
            CompanyName = opening.CompanyLink.CompanyName;
            StartingDate = opening.StartingDate.ToShortDateString();
            EndDate = opening.AnticipatedEndDate.ToShortDateString();
            HourlyPay = opening.HourlyPay;
        }
    }

    public class QualificationPrerequisitesViewModel
    {
        public string PrerequisiteId { get; set; }
        public string Course { get; set; }
        public double CourseHours { get; set; }

        public QualificationPrerequisitesViewModel(Prerequisite course)
        {
            PrerequisiteId = course.PrerequisiteId;
            Course = course.CourseLink.CourseName;
            CourseHours = course.CourseLink.CourseHours;
        }

    }

    public class QualificationCertificateViewModel
    {
        public string Candidate { get; set; }
        public string CandidateNum { get; set; }
        public string CertificationDate { get; set; }
        
        public QualificationCertificateViewModel(Certification certification)
        {
            CandidateNum = certification.CandidateNumber;
            Candidate = certification.CandidateLink.CandidateFullName;
            CertificationDate = certification.CertificationDate.ToShortDateString();
        }
    }
}
