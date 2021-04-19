using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace DataLayer.EfClasses
{
    public class Qualification
    {
        public string QualificationId { get; set; }

        public string QualificationCode { get; set; }

        public string Description { get; set; }

        public ICollection<Prerequisite> Prerequisites { get; set; }
        public ICollection<Certification> Certifications { get; set; }
        public ICollection<Opening> Openings { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}