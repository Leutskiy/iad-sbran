using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sbran.Domain.Entities.Chat;

namespace Sbran.Domain.Configurations
{
    public sealed class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public ChatRoomConfiguration(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string TableName => "ChatRooms";

        public string SchemaName { get; private set; }

        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.ToTable(TableName, SchemaName);

            builder.HasKey(chatroom => chatroom.Id);

            builder.Property(chatroom => chatroom.Id).HasColumnName("ChatRoomUid").ValueGeneratedNever();
        }
    }
}