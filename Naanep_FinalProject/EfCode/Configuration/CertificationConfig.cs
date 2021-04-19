using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class CertificationConfig : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.ToTable("Certification");

            builder.HasKey(c => c.CertificationId);
            builder.Property(c => c.CertificationId)
                .HasValueGenerator(typeof(CertificationIdGenerator));

            builder.HasOne(c => c.CandidateLink)
                .WithMany(c => c.Certifications)
                .HasForeignKey(c => c.CandidateNumber);

            builder.HasOne(c => c.QualificationLink)
                .WithMany(c => c.Certifications)
                .HasForeignKey(c => c.QualificationId);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

        public class CertificationIdGenerator : ValueGenerator
        {
            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var stringId = new StringBuilder();

                var idSequence = (context.Certifications.Count() + 1).ToString();

                stringId.Append("CRT-");
                stringId.Append($"{idSequence.PadLeft(6, '0')}");

                return stringId.ToString();
            }

            public override bool GeneratesTemporaryValues => false;
        }
    }




}