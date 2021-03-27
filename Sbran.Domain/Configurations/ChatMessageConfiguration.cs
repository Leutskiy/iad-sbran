using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities.Chat;
using Sbran.Domain.Entities.System;

namespace Sbran.Domain.Configurations
{
    public sealed class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public ChatMessageConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "ChatMessages";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(messages => messages.Id);

            builder.Property(messages => messages.Id).HasColumnName("MessagesUid").ValueGeneratedNever();

            builder.Property(messages => messages.ChatRoomId).HasColumnName("ChatRoomUid");
            builder.Property(messages => messages.UserId).HasColumnName("UserUid");
            builder.Property(messages => messages.Message).HasColumnName("Message");
            builder.Property(messages => messages.DateTime).HasColumnName("DateTime");

            builder
                .HasOne(messages => messages.ChatRoom)
                .WithMany();
        }
    }
}
