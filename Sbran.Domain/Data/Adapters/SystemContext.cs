using Sbran.Domain.Configurations;
using Sbran.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;

namespace Sbran.Domain.Data.Adapters
{
	/// <summary>
	/// Контекст системы
	/// </summary>
	public class SystemContext : DbContext
    {
		public DbSet<Profile> Profiles { get; set; }

		public DbSet<User> Users { get; set; }


        /// <summary>
        /// Конструктор контекста системы
        /// </summary>
        /// <param name="connectionString">Строка подключения</param>
        public SystemContext(DbContextOptions<SystemContext> options)
            : base(options)
        {
            SchemaName = Constants.Schemes.System;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Наименование строки подключения
        /// </summary>
        public string ConnectionStringName { get; private set; }

        /// <summary>
        /// Наименование схемы
        /// </summary>
        public string SchemaName { get; private set; }

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
        /// Зарегистрировать системные модели
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        private void RegisterDomainModels(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration(SchemaName));
            modelBuilder.ApplyConfiguration(new ProfileConfiguration(SchemaName));
        }
    }
}