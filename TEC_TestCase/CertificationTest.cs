using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataLayer.EfClasses;
using NUnit.Framework;

namespace TEC_TestCase
{
    [TestFixture]
    public class CertificationTest
    {
        [Test]
        public void AddCertificate_WithGeneratedKeys()
        {
            using var context = new TecContext();

            var newCert = new Certification
            {
                CandidateNumber = new ForeignKeys().GetRandomCandidate(),
                QualificationId = new ForeignKeys().GetRandomQualification(),
                CertificationDate = new DateTime(2020, 1, 1)
            };

            context.Add(newCert);
            context.SaveChanges();

            Console.WriteLine($"ID: {newCert.CertificationId}\tCandidate Number: {newCert.CandidateNumber}\tQualification Code: {newCert.QualificationId}");
        }
        
        
        public void RemoveCert()
        {
            using var context = new TecContext();

            //context.Remove(context.Certifications.Find("CRT-000001"));
            context.SaveChanges();

        }

        [Test]
        public static void GenerateCertifications()
        {
            var certificates =
                Certification_ReadFromCSV(
                    @"C:\Users\Admin\Desktop\2nd Sem '19\Software Design\Project\Data\Certification.csv");
            using var context = new TecContext();
            foreach (var certification in certificates)
            {
                context.Add(certification);
                context.SaveChanges();

            }
        }

        private static IList<Certification> Certification_ReadFromCSV(string location)
        {
            var sr = new StreamReader(location);

            var certificationList = new List<Certification>();

            string s = sr.ReadLine();
            var foreignKey = new ForeignKeys();
            while (s != null)
            {

                var newCert = new Certification
                {
                    CandidateNumber = foreignKey.GetRandomCandidate(),
                    QualificationId = foreignKey.GetRandomQualification(),
                    CertificationDate = Convert.ToDateTime(s)
                };

                certificationList.Add(newCert);

                s = sr.ReadLine();
            }

            return certificationList;
        }

        
        [Test]
        public void GetRandom_CandidateAndQualification()
        {
            Console.WriteLine($"Candidate Number: {new ForeignKeys().GetRandomCandidate()}\tQualification Code: {new ForeignKeys().GetRandomQualification()}");

        }
    }
}