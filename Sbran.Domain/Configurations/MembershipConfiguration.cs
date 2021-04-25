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
    public sealed class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public MembershipConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Memberships";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(membership => membership.Id);

            builder.Property(membership => membership.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(membership => membership.EmployeeId).HasColumnName("EmployeeUid");
            builder.Property(membership => membership.NameOfCompany).IsRequired(false).HasColumnName("NameOfCompany");
            builder.Property(membership => membership.StatusInTheOrganization).IsRequired(false).HasColumnName("StatusInTheOrganization");
            builder.Property(membership => membership.DateOfEntry).IsRequired(false).HasColumnName("DateOfEntry");
            builder.Property(membership => membership.SiteOfTheOrganization).IsRequired(false).HasColumnName("SiteOfTheOrganization");

            builder
                .HasOne(membership => membership.Employee)
                .WithMany()
                .HasForeignKey(membership => membership.EmployeeId);
        }
    }
}
