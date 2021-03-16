using ICS.Application.Commands.Read;
using Sbran.CQS.Read;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using LightInject;

namespace Sbran.CQS.Registries
{
    public class CommandCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ForeignParticipantReadCommand>();
            serviceRegistry.Register<ParticipantChatReadCommand>();
            serviceRegistry.Register<UserReadCommand>();
            serviceRegistry.Register<ProfileWriteCommand>();
            serviceRegistry.Register<InvitationWriteCommand>();
            serviceRegistry.Register<AlienWriteCommand>();
            serviceRegistry.Register<EmployeeWriteCommand>();

            serviceRegistry.Register<EmployeeReadCommand>();

            serviceRegistry.Register<ChatMessageWriteCommand>();
            serviceRegistry.Register<ChatMessageFileWriteCommand>();

            serviceRegistry.Register<IReadCommand<AlienResult>, AlienReadCommand>();
            serviceRegistry.Register<IReadCommand<ProfileResult>, ProfileReadCommand>();
            serviceRegistry.Register<IReadCommand<ContactResult>, ContactReadCommand>();
            serviceRegistry.Register<IReadCommand<DocumentResult>, DocumentReadCommand>();
            serviceRegistry.Register<IReadCommand<PassportResult>, PassportReadCommand>();

            serviceRegistry.Register<IChatRoomReadCommand, ChatRoomReadCommand>();
            serviceRegistry.Register<IChatMessageReadCommand, ChatMessageReadCommand>();
            serviceRegistry.Register<IReadCommand<ChatMessageFileResult>, ChatMessageFileReadCommand>();

            serviceRegistry.Register<IReadCommand<InvitationResult>, InvitationReadCommand>();
            serviceRegistry.Register<IReadCommand<VisitDetailResult>, VisitDetailReadCommand>();
            serviceRegistry.Register<IReadCommand<OrganizationResult>, OrganizationReadCommand>();
            serviceRegistry.Register<IReadCommand<StateRegistrationResult>, StateRegistrationReadCommand>();
            serviceRegistry.Register<IReadCommand<ForeignParticipantResult>, ForeignParticipantReadCommand>();

        }
    }
}