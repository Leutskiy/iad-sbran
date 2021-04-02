using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sbran.Domain.Configurations
{
    public sealed class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public ReportConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Reports";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(report => report.Id);

            builder.Property(report => report.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(report => report.MainPart).HasColumnName("MainPart");
            builder.Property(report => report.Findings).IsRequired(false).HasColumnName("Findings");
            builder.Property(report => report.Suggestion).IsRequired(false).HasColumnName("Suggestion");
            builder.Property(report => report.ForeignInterest).IsRequired(false).HasColumnName("ForeignInterest");
            builder.Property(report => report.Status).IsRequired(true).HasColumnName("Status").HasDefaultValue(false);

            builder
                .HasMany(report => report.ListOfScientists)
                .WithOne();

            builder
                .HasMany(report => report.Appendices)
                .WithOne();
        }
    }
}
