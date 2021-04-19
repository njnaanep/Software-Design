using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class PlacementConfig :IEntityTypeConfiguration<Placement>
    {
        public void Configure(EntityTypeBuilder<Placement> builder)
        {
            builder.ToTable("Placement");

            builder.HasKey(c => c.PlacementId);
            builder.Property(c => c.PlacementId)
                .HasValueGenerator(typeof(PlacementIdGenerator));

            builder.HasOne(c => c.OpeningLink)
                .WithMany(c => c.Placements)
                .HasForeignKey(c => c.OpeningNumber);
            
            builder.HasOne(c => c.CandidateLink)
                .WithMany(c => c.Placements)
                .HasForeignKey(c => c.CandidateNumber);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

        public class PlacementIdGenerator : ValueGenerator
        {
            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var st = new StringBuilder();

                var idNumSequence = (context.Placements.Count() + 1).ToString();

                st.Append("PLC-");
                st.Append($"{idNumSequence.PadLeft(6, '0')}");

                return st.ToString();
            }

            public override bool GeneratesTemporaryValues => false;
        }
    }
}