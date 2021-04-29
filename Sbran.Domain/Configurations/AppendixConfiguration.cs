using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities;

namespace Sbran.Domain.Configurations
{
	public sealed class AppendixConfiguration : IEntityTypeConfiguration<Appendix>
    {
        public AppendixConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Appendixs";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Appendix> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(appendix => appendix.Id);

            builder.Property(appendix => appendix.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(appendix => appendix.ReportId).HasColumnName("DepartureReportUid");
            builder.Property(appendix => appendix.FileBinary).IsRequired(false).HasColumnName("FileBinary");
            builder.Property(appendix => appendix.FileName).IsRequired(false).HasColumnName("FileName");

            builder
                .HasOne(appendix => appendix.Report)
                .WithMany()
                .HasForeignKey(appendix => appendix.ReportId);
        }
    }
}
