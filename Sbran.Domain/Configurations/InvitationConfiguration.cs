using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public InvitationConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "Invitations";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(invitation => invitation.Id);

            builder.Property(invitation => invitation.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(invitation => invitation.AlienId).HasColumnName("AlienUid");
            builder.Property(invitation => invitation.EmployeeId).HasColumnName("EmployeeUid");
            builder.Property(invitation => invitation.VisitDetailId).IsRequired(false).HasColumnName("VisitDetailUid");
            builder.Property(invitation => invitation.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(invitation => invitation.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(invitation => invitation.Status).HasColumnName("Status");

            builder
                .HasOne(invitation => invitation.Alien)
                .WithMany()
                .HasForeignKey(invitation => invitation.AlienId)
                .HasPrincipalKey(alien => alien.Id);

            builder
                .HasOne(invitation => invitation.Employee)
                .WithMany(employee => employee.Invitations)
                .HasForeignKey(invitation => invitation.EmployeeId)
                .HasPrincipalKey(employee => employee.Id);

            builder
                .HasOne(invitation => invitation.VisitDetail)
                .WithOne()
                .HasForeignKey<Invitation>(invitation => invitation.VisitDetailId)
                .HasPrincipalKey<VisitDetail>(visitDetail => visitDetail.Id);
        }
    }
}