using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities.System;

namespace Sbran.Domain.Configurations
{
    public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Employees";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(employee => employee.Id);

            builder.Property(employee => employee.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(employee => employee.UserId).HasColumnName("UserUid");
            builder.Property(employee => employee.ContactId).IsRequired(false).HasColumnName("ContactUid");
            builder.Property(employee => employee.ManagerId).IsRequired(false).HasColumnName("ManagerUid");
            builder.Property(employee => employee.PassportId).IsRequired(false).HasColumnName("PassportUid");
            builder.Property(employee => employee.OrganizationId).IsRequired(false).HasColumnName("OrganizationUid");
            builder.Property(employee => employee.StateRegistrationId).IsRequired(false).HasColumnName("StateRegistrationUid");
            builder.Property(employee => employee.AcademicRank).IsRequired(false).HasColumnName("AcademicRank");
            builder.Property(employee => employee.AcademicDegree).IsRequired(false).HasColumnName("AcademicDegree");
            builder.Property(employee => employee.Education).IsRequired(false).HasColumnName("Education");
            builder.Property(employee => employee.Position).IsRequired(false).HasColumnName("Position");
            builder.Property(employee => employee.WorkPlace).IsRequired(false).HasColumnName("WorkPlace");

            builder
                .HasOne(employee => employee.Manager)
                .WithMany()
                .HasForeignKey(employee => employee.ManagerId);
                //.HasPrincipalKey<User>(user => user.Id);

            builder
                .HasOne(employee => employee.Contact)
                .WithOne()
                .HasForeignKey<Employee>(employee => employee.ContactId)
                .HasPrincipalKey<Contact>(contact => contact.Id);

            builder
                .HasOne(employee => employee.Passport)
                .WithOne()
                .HasForeignKey<Employee>(employee => employee.PassportId)
                .HasPrincipalKey<Passport>(passport => passport.Id);

            builder
                .HasOne(employee => employee.Organization)
                .WithOne()
                .HasForeignKey<Employee>(employee => employee.OrganizationId)
                .HasPrincipalKey<Organization>(organization => organization.Id);

            builder
                .HasOne(employee => employee.StateRegistration)
                .WithOne()
                .HasForeignKey<Employee>(employee => employee.StateRegistrationId)
                .HasPrincipalKey<StateRegistration>(stateRegistration => stateRegistration.Id);

            builder
                .HasMany(employee => employee.Invitations)
                .WithOne();
        }
    }
}