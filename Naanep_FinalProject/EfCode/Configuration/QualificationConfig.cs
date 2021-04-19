using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class QualificationConfig : IEntityTypeConfiguration<Qualification>
    {
        public void Configure(EntityTypeBuilder<Qualification> builder)
        {
            builder.ToTable("Qualification");

            builder.HasKey(c => c.QualificationId);
            builder.Property(c => c.QualificationId)
                .HasValueGenerator(typeof(QualificationIdGenerator));

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);


        }
    }

    public class QualificationIdGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;
        protected override object NextValue(EntityEntry entry)
        {
            using var context = new TecContext();

            var stringId = new StringBuilder();

            var idNumSequence = (context.Qualifications.Count() + 1).ToString();

            stringId.Append("QLN-");
            stringId.Append($"{idNumSequence.PadLeft(6, '0')}");

            return stringId.ToString();
        }


    }
}