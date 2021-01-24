using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class VisitDetailConfiguration : IEntityTypeConfiguration<VisitDetail>
    {
        public VisitDetailConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "VisitDetails";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<VisitDetail> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(visitDetail => visitDetail.Id);

            builder.Property(visitDetail => visitDetail.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(visitDetail => visitDetail.Goal).IsRequired(false).HasColumnName("Goal");
            builder.Property(visitDetail => visitDetail.Country).IsRequired(false).HasColumnName("Country");
            builder.Property(visitDetail => visitDetail.VisitingPoints).IsRequired(false).HasColumnName("VisitingPoints");
            builder.Property(visitDetail => visitDetail.VisaType).IsRequired(false).HasColumnName("VisaType");
            builder.Property(visitDetail => visitDetail.VisaCity).IsRequired(false).HasColumnName("VisaCity");
            builder.Property(visitDetail => visitDetail.VisaCountry).IsRequired(false).HasColumnName("VisaCountry");
            builder.Property(visitDetail => visitDetail.VisaMultiplicity).IsRequired(false).HasColumnName("VisaMultiplicity");
            builder.Property(visitDetail => visitDetail.PeriodDays).IsRequired(false).HasColumnName("PeriodDays");
            builder.Property(visitDetail => visitDetail.ArrivalDate).IsRequired(false).HasColumnName("ArrivalDate");
            builder.Property(visitDetail => visitDetail.DepartureDate).IsRequired(false).HasColumnName("DepartureDate");
        }
    }
}