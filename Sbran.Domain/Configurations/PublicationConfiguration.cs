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
    public sealed class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public PublicationConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Publications";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(publication => publication.Id);

            builder.Property(publication => publication.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(publication => publication.EmployeeId).HasColumnName("EmployeeUid");
            builder.Property(publication => publication.ScientificAdvisor).IsRequired(false).HasColumnName("ScientificAdvisor");
            builder.Property(publication => publication.TitleOfTheArticle).IsRequired(false).HasColumnName("TitleOfTheArticle");
            builder.Property(publication => publication.Abstract).IsRequired(false).HasColumnName("Abstract");
            builder.Property(publication => publication.Keywords).IsRequired(false).HasColumnName("Keywords");
            builder.Property(publication => publication.MainTextOfTheArticle).IsRequired(false).HasColumnName("MainTextOfTheArticle");
            builder.Property(publication => publication.Literature).IsRequired(false).HasColumnName("Literature");

            builder
                .HasOne(publication => publication.Employee)
                .WithMany()
                .HasForeignKey(publication => publication.EmployeeId);
        }
    }
}
