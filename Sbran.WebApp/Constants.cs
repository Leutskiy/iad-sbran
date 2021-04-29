namespace Sbran.WebApp
{
    /// <summary>
    /// Константы
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Схемы
        /// </summary>
        public static class Schemes
        {
            /// <summary>
            /// Системная
            /// </summary>
            public static string System => "system";

            /// <summary>
            /// Доменная
            /// </summary>
            public static string Domain => "domain";

            /// <summary>
            /// Логовая
            /// </summary>
            public static string Log => "log";
        }

		public static class SecurityRoles
		{
            public static string Admin => "Admin";

            public static string Director => "Director";

            public static string Employee => "Employee";
		}
    }
}