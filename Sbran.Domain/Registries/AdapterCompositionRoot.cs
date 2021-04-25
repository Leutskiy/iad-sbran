using LightInject;

namespace Sbran.Domain.Registries
{
	public class AdapterCompositionRoot : ICompositionRoot
    {
        private const string ConnectionStringName = "PostgreSQLConnection";

        public void Compose(IServiceRegistry serviceRegistry)
        {
            //serviceRegistry.Register<IDomainDbContext>(factory => new DatabaseContext(new DomainContext(nameOrConnectionString: ConnectionStringName)));
            //serviceRegistry.Register<ISystemDbContext>(factory => new DatabaseContext(new SystemContext(nameOrConnectionString: ConnectionStringName)));
        }
    }
}
