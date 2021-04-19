using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DataLayer.EfCode.Configuration
{
    public class CandidateConfig : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("Candidate");

            builder.HasKey(c => c.CandidateNumber);
            builder.Property(c => c.CandidateNumber)
                .HasValueGenerator((typeof(CandidateIdGenerator)));
            
            builder.Property(c => c.CandidateMiddleName).IsRequired(false);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

       
        public class CandidateIdGenerator : ValueGenerator
        {
            public override bool GeneratesTemporaryValues => false;

            protected override object NextValue(EntityEntry entry)
            {
                using var context = new TecContext();

                var stringId = new StringBuilder();
                
                var idNumSequence = (context.Candidates.Count() + 1).ToString();
                
                stringId.Append("CDT-");
                stringId.Append($"{idNumSequence.PadLeft(6, '0')}");

                return stringId.ToString();
            }
        }

    }
}