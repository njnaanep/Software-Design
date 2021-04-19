using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class RegisteredCandidateConfig : IEntityTypeConfiguration<RegisteredCandidate>
    {
        public void Configure(EntityTypeBuilder<RegisteredCandidate> builder)
        {
            builder.ToTable("RegisteredCandidate");

            builder.HasKey(c => c.RegistrationId);
            builder.Property(c => c.RegistrationId)
                .HasValueGenerator(typeof(RegistrationIdGenerator));

            builder.HasOne(c => c.CandidateLink)
                .WithMany(c => c.RegisteredCandidates)
                .HasForeignKey(c => c.CandidateNumber);
            builder.HasOne(c => c.TrainingSessionLink)
                .WithMany(c => c.RegisteredCandidates)
                .HasForeignKey(c => c.SessionId);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        }

        public class RegistrationIdGenerator : ValueGenerator
        {
            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var st = new StringBuilder();

                var idNumSequence = (context.RegisteredCandidates.Count() + 1).ToString();

                st.Append("REG-");
                st.Append($"{idNumSequence.PadLeft(6, '0')}");

                return st.ToString();
            }

            public override bool GeneratesTemporaryValues => false;
        }
    }
}