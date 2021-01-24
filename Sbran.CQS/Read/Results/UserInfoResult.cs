namespace Sbran.CQS.Read.Results
{
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public sealed class UserInfoResult
    {
        /// <summary>
        /// Профиль
        /// </summary>
        public ProfileResult Profile { get; set; }

        /// <summary>
        /// Краткое наименование организации
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public string Fio { get; set; }

        /// <summary>
        /// Факс сотрудника
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Электронная почта сотрудника
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Мобильный номер телефона сотрудника
        /// </summary>
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Научная степень сотрудника
        /// </summary>
        public string AcademicDegree { get; set; }

        /// <summary>
        /// Научное звание сотрудника
        /// </summary>
        public string AcademicRank { get; set; }

        /// <summary>
        /// Образование сотрудника
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// Место работы сотрудника
        /// </summary>
        public string WorkPlace { get; set; }

        /// <summary>
        /// Должность сотрдуника
        /// </summary>
        public string Position { get; set; }
    }
}