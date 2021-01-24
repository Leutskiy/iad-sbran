using Sbran.Domain.Services;
using Sbran.Domain.Services.Contracts;
using LightInject;

namespace Sbran.Domain.Registries
{
    public class ServiceCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IClock, SystemClock>();
            serviceRegistry.Register<IIdGenerator, IdGenerator>();
            serviceRegistry.Register<IUserInfoProvider, UserInfoProvider>();
        }
    }
}
