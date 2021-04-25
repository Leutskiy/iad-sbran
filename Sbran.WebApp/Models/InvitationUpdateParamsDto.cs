using System;

namespace Sbran.WebApp.Models
{
    /// <summary>
    /// Параметры создания приглашения
    /// </summary>
    public sealed class InvitationUpdateParamsDto
    {
        /// <summary>
        /// Идентификатор приглашения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цель поездки
        /// </summary>
        public string VisitGoal { get; set; }

        /// <summary>
        /// Имя иностранца
        /// </summary>
        public string AlienName { get; set; }

        /// <summary>
        /// Страна поездки
        /// </summary>
        public string VisitCountry { get; set; }
    }
}