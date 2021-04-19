using System.Linq;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class TrainingSessionConfig :IEntityTypeConfiguration<TrainingSession>
    {
        public void Configure(EntityTypeBuilder<TrainingSession> builder)
        {
            builder.ToTable("TrainingSession");

            builder.HasKey(c => c.SessionId);
            builder.Property(c => c.SessionId)
                .HasValueGenerator(typeof(SessionIdGenerator));

                builder.HasOne(c => c.CourseLink)
                .WithMany(c => c.TrainingSessions)
                .HasForeignKey(c => c.CourseId)
                .IsRequired(false);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

        public class SessionIdGenerator : ValueGenerator
        {
            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var st = new StringBuilder();

                var idNumSequence = (context.TrainingSessions.Count() + 1).ToString();

                st.Append("TRA-");
                st.Append($"{idNumSequence.PadLeft(6, '0')}");

                return st.ToString();
            }

            public override bool GeneratesTemporaryValues => false;
        }
    }
}