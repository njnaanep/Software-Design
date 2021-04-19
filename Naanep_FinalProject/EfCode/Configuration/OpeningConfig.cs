using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class OpeningConfig :IEntityTypeConfiguration<Opening>
    {
        public void Configure(EntityTypeBuilder<Opening> builder)
        {
            builder.ToTable("Opening");

            builder.HasKey(c => c.OpeningNumber);
            builder.Property(c => c.OpeningNumber)
                .HasValueGenerator(typeof(OpeningIdGenerator));

            builder.HasOne(c => c.CompanyLink)
                .WithMany(c => c.Openings)
                .HasForeignKey(c => c.CompanyId);

            builder.HasOne(c => c.QualificationLink)
                .WithMany(c => c.Openings)
                .HasForeignKey(c => c.QualificationId);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

        public class OpeningIdGenerator : ValueGenerator
        {
            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var st = new StringBuilder();

                var idNumSequence = (context.Openings.Count() + 1).ToString();

                st.Append("OPN-");
                st.Append($"{idNumSequence.PadLeft(6, '0')}");

                return st.ToString();
            }

            public override bool GeneratesTemporaryValues => false;
        }
    }
}