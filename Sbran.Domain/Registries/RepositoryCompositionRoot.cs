using Sbran.Domain.Data.Repositories;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Repositories;
using Sbran.Domain.Repositories.Contracts;
using LightInject;

namespace Sbran.Domain.Registries
{
    public class RepositoryCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IUserRepository, UserRepository>();
            serviceRegistry.Register<IProfileRepository, ProfileRepository>();

            serviceRegistry.Register<IAlienRepository, AlienRepository>();
            serviceRegistry.Register<IContactRepository, ContactRepository>();
            serviceRegistry.Register<IPassportRepository, PassportRepository>();
            serviceRegistry.Register<IEmployeeRepository, EmployeeRepository>();
            serviceRegistry.Register<IDocumentRepository, DocumentRepository>();
            serviceRegistry.Register<IInvitationRepository, InvitationRepository>();
            serviceRegistry.Register<IVisitDetailRepository, VisitDetailRepository>();
            serviceRegistry.Register<IOrganizationRepository, OrganizationRepository>();
            serviceRegistry.Register<IStateRegistrationRepository, StateRegistrationRepository>();
            serviceRegistry.Register<IForeignParticipantRepository, ForeignParticipantRepository>();
            serviceRegistry.Register<IChatRoomRepository, ChatRoomRepository>();
            serviceRegistry.Register<IChatRoomListRepository, ChatRoomListRepository>();
            serviceRegistry.Register<IMessagesRepository, MessagesRepository>();
        }
    }
}