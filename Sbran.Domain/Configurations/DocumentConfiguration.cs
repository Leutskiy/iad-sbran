using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public DocumentConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Documents";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(document => document.Id);

            builder.Property(document => document.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(document => document.InvitationId).HasColumnName("InvitationUid");
            builder.Property(document => document.Name).HasColumnName("Name");
            builder.Property(document => document.Content).HasColumnName("Content");
            builder.Property(document => document.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(document => document.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(document => document.DocumentType).HasColumnName("DocumentType");

            builder
                .HasOne(document => document.Invitation)
                .WithMany()
                .HasForeignKey(document => document.InvitationId)
                .HasPrincipalKey(invitation => invitation.Id);
        }
    }
}