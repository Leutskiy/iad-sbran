using System;

namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Данные по государственной регистрации
    /// </summary>
    public sealed class StateRegistrationResult
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// ОГРНИП
        /// </summary>
        public string Ogrnip { get; set; }
    }
}