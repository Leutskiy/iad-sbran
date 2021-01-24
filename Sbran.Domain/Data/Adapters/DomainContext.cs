using Sbran.Domain.Configurations;
using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sbran.Domain.Data.Adapters
{
    /// <summary>
    /// Контекст домена
    /// </summary>
    public sealed class DomainContext : DbContext
    {
		public DbSet<Alien> Aliens { get; set; }

		public DbSet<Contact> Contacts { get; set; }

		public DbSet<Document> Documents { get; set; }

		public DbSet<Employee> Employees { get; set; }

		public DbSet<Passport> Passports { get; set; }

		public DbSet<Invitation> Invitations { get; set; }

		public DbSet<Organization> Organizations { get; set; }

		public DbSet<VisitDetail> VisitDetails { get; set; }

		public DbSet<StateRegistration> StateRegistrations { get; set; }

		public DbSet<ForeignParticipant> ForeignParticipants { get; set; }

        /// <summary>
        /// Конструктор контекста домена
        /// </summary>
        public DomainContext(DbContextOptions<DomainContext> options)
            : base(options)
        {
            SchemaName = Constants.Schemes.Domain;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Наименование схемы
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        /// Наименование строки подключения
        /// </summary>
        public string ConnectionStringName { get; private set; }

        /// <summary>
        /// Вызов после создания модели
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterDomainModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Зарегистрировать доменные модели
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        private void RegisterDomainModels(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlienConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ContactConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new DocumentConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new PassportConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new InvitationConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new VisitDetailConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new StateRegistrationConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ForeignParticipantConfiguration(SchemaName));
        }
    }
}