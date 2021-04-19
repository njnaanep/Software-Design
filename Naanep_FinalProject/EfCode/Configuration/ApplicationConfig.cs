using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class ApplicationConfig : IEntityTypeConfiguration<Placement>
    {
        public void Configure(EntityTypeBuilder<Placement> builder)
        {
            builder.ToTable("Placement");

            builder.HasKey(c => c.PlacementId);
            builder.Property(c => c.PlacementId)
                .HasValueGenerator(typeof(ApplicationIdGenerator));

            builder.Property(c => c.ApplicationDate).ValueGeneratedOnAdd();

            builder.HasOne(c => c.CandidateLink)
                .WithMany(c => c.Applications)
                .HasForeignKey(c => c.CandidateNumber);

            builder.HasOne(c => c.OpeningLink)
                .WithMany(c => c.Applications)
                .HasForeignKey(c => c.OpeningNumber);

            //builder.HasOne(c => c.PlacementLink)
            //    .WithMany(c => c.Applications)
            //    .HasForeignKey(c => c.PlacementId)
            //    .IsRequired(false);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

        public class ApplicationIdGenerator : ValueGenerator
        {
            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var st = new StringBuilder();

                var idNumSequence = (context.Applications.Count() + 1).ToString();

                st.Append("APL-");
                st.Append($"{idNumSequence.PadLeft(6, '0')}");

                return st.ToString();
            }

            public override bool GeneratesTemporaryValues => false;
        }
    }
        
    
}