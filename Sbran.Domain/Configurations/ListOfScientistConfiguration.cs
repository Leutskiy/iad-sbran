using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities;

namespace Sbran.Domain.Configurations
{
	public sealed class ListOfScientistConfiguration : IEntityTypeConfiguration<ListOfScientist>
    {
        public ListOfScientistConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "ListOfScientists";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<ListOfScientist> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(listOfScientist => listOfScientist.Id);

            builder.Property(listOfScientist => listOfScientist.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(listOfScientist => listOfScientist.ReportId).HasColumnName("ReportUid");
            builder.Property(listOfScientist => listOfScientist.Email).IsRequired(false).HasColumnName("Email");
            builder.Property(listOfScientist => listOfScientist.FullName).IsRequired(false).HasColumnName("FullName");
            builder.Property(listOfScientist => listOfScientist.WorkPlace).IsRequired(false).HasColumnName("WorkPlace");
            builder.Property(listOfScientist => listOfScientist.PhoneNumber).IsRequired(false).HasColumnName("PhoneNumber");
            builder.Property(listOfScientist => listOfScientist.Position).IsRequired(false).HasColumnName("FileBinary");

            builder
                .HasOne(listOfScientist => listOfScientist.Report)
                .WithMany()
                .HasForeignKey(listOfScientist => listOfScientist.ReportId);
        }
    }
}
