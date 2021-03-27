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
    public sealed class DepartureConfiguration : IEntityTypeConfiguration<Departure>
    {
        public DepartureConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Departures";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Departure> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(departure => departure.Id);

            builder.Property(departure => departure.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(departure => departure.EmployeeId).HasColumnName("EmployeeUid");
            builder.Property(departure => departure.BasicOfDeparture).IsRequired(false).HasColumnName("BasicOfDeparture");
            builder.Property(departure => departure.CityOfBusiness).IsRequired(false).HasColumnName("CityOfBusiness");
            builder.Property(departure => departure.DateEnd).IsRequired(false).HasColumnName("DateEnd");
            builder.Property(departure => departure.DateStart).IsRequired(false).HasColumnName("DateStart");
            builder.Property(departure => departure.HostOrganization).IsRequired(false).HasColumnName("HostOrganization");
            builder.Property(departure => departure.JustificationOfTheBusiness).IsRequired(false).HasColumnName("JustificationOfTheBusiness");
            builder.Property(departure => departure.PlaceOfResidence).IsRequired(false).HasColumnName("PlaceOfResidence");
            builder.Property(departure => departure.PurposeOfTheTrip).IsRequired(false).HasColumnName("PurposeOfTheTrip");
            builder.Property(departure => departure.SendingCountry).IsRequired(false).HasColumnName("SendingCountry");
            builder.Property(departure => departure.SourceOfFinancing).IsRequired(false).HasColumnName("SourceOfFinancing");
            builder.Property(departure => departure.DepartureStatus).IsRequired(false).HasColumnName("DepartureStatus");
            builder.Property(departure => departure.ReportId).IsRequired(false).HasColumnName("ReportUid");

            builder
                .HasOne(departure => departure.Employee)
                .WithMany()
                .HasForeignKey(departure => departure.EmployeeId);

            builder
                .HasOne(departure => departure.Report)
                .WithMany()
                .HasForeignKey(departure => departure.ReportId);
        }
    }
}
