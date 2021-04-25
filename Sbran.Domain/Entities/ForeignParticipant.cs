using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Иностранный сопровождающий
    /// </summary>
    public sealed class ForeignParticipant
    {
        /// <summary>
        /// Инициализировать иностранного сопровождающего
        /// </summary>
        public ForeignParticipant()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор паспорта
        /// </summary>
        public Guid? PassportId { get; private set; }

		public Passport? Passport { get; private set; }

        /// <summary>
        /// Задать паспортные данные
        /// </summary>
        /// <param name="passportId">Паспортные данные</param>
        public void SetPassport(Passport passport)
        {
            if (Passport?.Id == passport.Id)
            {
                return;
            }

            PassportId = passport.Id;
            Passport = passport;
        }
    }
}