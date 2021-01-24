using System;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные по иностранному участнику
    /// </summary>
    public sealed class ForeignParticipantResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Данные по паспорту
        /// </summary>
        public PassportResult? Passport { get; set; }
    }
}