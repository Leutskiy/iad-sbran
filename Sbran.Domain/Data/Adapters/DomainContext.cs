using Sbran.Domain.Configurations;
using Sbran.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Sbran.Domain.Entities.Chat;

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
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomList> ChatRoomLists { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatMessageFile> ChatMessageFiles { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<ConsularOffice> ConsularOffices { get; set; }
        public DbSet<InternationalAgreement> InternationalAgreements { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<ScientificInterests> ScientificInterests { get; set; }
        public DbSet<Appendix> Appendixs { get; set; }
        public DbSet<Report> Reports { get; set; }

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
            modelBuilder.ApplyConfiguration(new ChatRoomConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ChatRoomListConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ChatMessageFileConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new DepartureConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ConsularOfficeConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new InternationalAgreementConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new MembershipConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new PublicationConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ScientificInterestsConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new AppendixConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ReportConfiguration(SchemaName));
        }
    }
}