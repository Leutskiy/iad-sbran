using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class PassportConfiguration : IEntityTypeConfiguration<Passport>
    {
        public PassportConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Passports";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Passport> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(passport => passport.Id);

            builder.Property(passport => passport.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(passport => passport.NameRus).IsRequired(false).HasColumnName("NameRus");
            builder.Property(passport => passport.NameEng).IsRequired(false).HasColumnName("NameEng");
            builder.Property(passport => passport.SurnameRus).IsRequired(false).HasColumnName("SurnameRus");
            builder.Property(passport => passport.SurnameEng).IsRequired(false).HasColumnName("SurnameEng");
            builder.Property(passport => passport.PatronymicNameRus).IsRequired(false).HasColumnName("PatronymicNameRus");
            builder.Property(passport => passport.PatronymicNameEng).IsRequired(false).HasColumnName("PatronymicNameEng");
            builder.Property(passport => passport.BirthDate).IsRequired(false).HasColumnName("BirthDate");
            builder.Property(passport => passport.BirthPlace).IsRequired(false).HasColumnName("BirthPlace");
            builder.Property(passport => passport.BirthCountry).IsRequired(false).HasColumnName("BirthCountry");
            builder.Property(passport => passport.Citizenship).IsRequired(false).HasColumnName("Citizenship");
            builder.Property(passport => passport.Gender).IsRequired(false).HasColumnName("Gender");
            builder.Property(passport => passport.IdentityDocument).IsRequired(false).HasColumnName("IdentityDocument");
            builder.Property(passport => passport.IssueDate).IsRequired(false).HasColumnName("IssueDate");
            builder.Property(passport => passport.IssuePlace).IsRequired(false).HasColumnName("IssuePlace");
            builder.Property(passport => passport.DepartmentCode).IsRequired(false).HasColumnName("DepartmentCode");
            builder.Property(passport => passport.Residence).IsRequired(false).HasColumnName("Residence");
            builder.Property(passport => passport.ResidenceRegion).IsRequired(false).HasColumnName("ResidenceRegion");
            builder.Property(passport => passport.ResidenceCountry).IsRequired(false).HasColumnName("ResidenceCountry");
        }
    }
}