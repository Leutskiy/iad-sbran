using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sbran.Domain.Configurations
{
    public sealed class StateRegistrationConfiguration : IEntityTypeConfiguration<StateRegistration>
    {
        public StateRegistrationConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "StateRegistrations";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<StateRegistration> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(stateRegistration => stateRegistration.Id);

            builder.Property(stateRegistration => stateRegistration.Id)
                .HasColumnName("Uid")
                .ValueGeneratedNever();

            builder.Property(stateRegistration => stateRegistration.Inn).IsRequired(false).HasColumnName("INN");
            builder.Property(stateRegistration => stateRegistration.Ogrnip).IsRequired(false).HasColumnName("OGRNIP");
        }
    }
}