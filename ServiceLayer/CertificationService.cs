using System.Linq;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ServiceLayer
{
    public class CertificationService
    {
        private TecContext _context;

        public CertificationService(TecContext context) => _context = context;


        public static IQueryable<Certification> GetCertifications =>
            new TecContext().Certifications.Where(c => !c.IsDeleted)
                .Include(c => c.CandidateLink)
                .Include(c => c.QualificationLink);

        public static IQueryable<Certification> GetCandidateCertifications(string candidateNum) =>
            GetCertifications.Where(c => c.CandidateNumber == candidateNum);

        public static IQueryable<Certification> GetQualificationCertifications(string qualificationId) =>
            GetCertifications.Where(c => c.QualificationId == qualificationId);

        public void AddCertification(Certification certification)
        {
            _context.Certifications.Add(certification);
            _context.SaveChanges();
        }

        public void UpdateCertification(Certification certification)
        {
            var certificate = _context.Certifications.Find(certification.CertificationId);
            certificate.CandidateNumber = certification.CandidateNumber;
            certificate.QualificationId = certification.QualificationId;

            _context.SaveChanges();
        }
        public void DeleteCertification(string certificationId)
        {
            _context.Certifications.Find(certificationId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}