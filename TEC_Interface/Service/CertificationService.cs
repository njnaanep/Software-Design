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

        public IQueryable<Certification> GetCertifications =>
            _context.Certifications.Where(c => c.IsDeleted == false)
                .Include(c => c.CandidateLink)
                .Include(c => c.QualificationLink);

        public IQueryable<Certification> GetCandidateCertifications(string candidateNum) =>
            _context.Certifications.Where(c => c.IsDeleted == false
                && c.CandidateNumber == candidateNum)
                .Include(c => c.QualificationLink);

        public IQueryable<Certification> GetQualificationCertifications(string qualificationCode) =>
            _context.Certifications.Where(c => c.IsDeleted == false
                && c.QualificationCode == qualificationCode)
                .Include(c => c.QualificationLink);

        public void Certification(Certification certification)
        {
            _context.Certifications.Add(certification);
            _context.SaveChanges();
        }

        public void UpdateCertification(string certificationId, string candidateNum, string qualification)
        {
            var certificate = _context.Certifications.Find(certificationId);
            certificate.CandidateNumber = candidateNum;
            certificate.QualificationCode= qualification;

            _context.SaveChanges();
        }

        public void DeleteCertification(string certificationId)
        {
            _context.Certifications.Find(certificationId).IsDeleted = true;
            _context.SaveChanges();
        }
    }
}