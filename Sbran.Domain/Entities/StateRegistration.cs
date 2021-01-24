using System;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Государственная регистрация
    /// </summary>
    public sealed class StateRegistration
    {
        public StateRegistration()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string? Inn { get; private set; }

        /// <summary>
        /// ОГРНИП
        /// </summary>
        public string? Ogrnip { get; private set; }

        /// <summary>
        /// Задать ИНН
        /// </summary>
        /// <param name="inn">ИНН</param>
        public void SetInn(string? inn)
        {
            if (Inn == inn)
            {
                return;
            }

            Inn = inn;
        }

        /// <summary>
        /// Задать ОГРНИП
        /// </summary>
        /// <param name="ogrnip">ОГРНИП</param>
        public void SetOgrnip(string? ogrnip)
        {
            if (Ogrnip == ogrnip)
            {
                return;
            }

            Ogrnip = ogrnip;
        }
    }
}