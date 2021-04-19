using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.VisualBasic.CompilerServices;

namespace DataLayer.EfCode.Configuration
{
    public class JobHistoryConfig : IEntityTypeConfiguration<JobHistory>
    {
        public void Configure(EntityTypeBuilder<JobHistory> builder)
        {
            builder.ToTable("JobHistory");

            builder.HasKey(c => c.HistoryId);
            builder.Property(c => c.HistoryId)
                .HasValueGenerator(typeof(JobHistoryIdGenerator));

            builder.Property(c => c.WorkedTo)
                .IsRequired(false);

            builder.HasOne(c => c.CandidateLink)
                .WithMany(c => c.JobHistories)
                .HasForeignKey(c => c.CandidateNumber);

            builder.HasOne(c => c.CompanyLink)
                .WithMany(c => c.JobHistories)
                .HasForeignKey(c => c.CompanyId);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

        public class JobHistoryIdGenerator : ValueGenerator
        {
            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var stringId = new StringBuilder();

                var idNumSequence = (context.JobHistories.Count() + 1).ToString();

                stringId.Append("HTY-");
                stringId.Append($"{idNumSequence.PadLeft(6, '0')}");

                return stringId.ToString();
            }

            public override bool GeneratesTemporaryValues => false;
        }
    }
}