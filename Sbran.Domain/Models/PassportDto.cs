using Sbran.Domain.Enums;
using Newtonsoft.Json;
using System;

namespace Sbran.Domain.Models
{
    /// <summary>
    /// Информация о паспортных данных
    /// </summary>
    public sealed class PassportDto
    {
        # region Имя-Фамалия-Отчество (Русский/Английский вариынты)

        /// <summary>
        /// Имя (по-русски)
        /// </summary>
        [JsonProperty("nameRus")]
        public string NameRus { get; set; }

        /// <summary>
        /// Имя (по-английски)
        /// </summary>
        [JsonProperty("nameEng")]
        public string NameEng { get; set; }

        /// <summary>
        /// Фамилия (по-русски)
        /// </summary>
        [JsonProperty("surnameRus")]
        public string SurnameRus { get; set; }

        /// <summary>
        /// Фамилия (по-английски)
        /// </summary>
        [JsonProperty("surnameEng")]
        public string SurnameEng { get; set; }

        /// <summary>
        /// Отчество (по-русски)
        /// </summary>
        [JsonProperty("patronymicNameRus")]
        public string PatronymicNameRus { get; set; }

        /// <summary>
        /// Отчество (по-ангийски)
        /// </summary>
        [JsonProperty("patronymicNameEng")]
        public string PatronymicNameEng { get; set; }

        #endregion

        # region Рождение

        /// <summary>
        /// Место рождения
        /// </summary>
        [JsonProperty("birthPlace")]
        public string BirthPlace { get; set; }

        /// <summary>
        /// Страна рождения
        /// </summary>
        [JsonProperty("birthCountry")]
        public string BirthCountry { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [JsonProperty("birthDate")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [JsonProperty("gender")]
        public Sex? Gender { get; set; }

        /// <summary>
        /// Гражданство (подданство)
        /// </summary>
        [JsonProperty("citizenship")]
        public string Citizenship { get; set; }

        #endregion

        #region Резиденция

        /// <summary>
        /// Местожительство
        /// </summary>
        [JsonProperty("residence")]
        public string Residence { get; set; }

        /// <summary>
        /// Страна постоянного проживания
        /// </summary>
        [JsonProperty("residenceCountry")]
        public string ResidenceCountry { get; set; }

        /// <summary>
        /// Регион в стране постоянного проживания
        /// </summary>
        [JsonProperty("residenceRegion")]
        public string ResidenceRegion { get; set; }

        #endregion

        #region Документ идентификации

        /// <summary>
        /// Документ удостоверяющий личность
        /// </summary>
        [JsonProperty("identityDocument")]
        public string IdentityDocument { get; set; }

        /// <summary>
        /// Место выдачи документа удостоверяющего личность
        /// </summary>
        [JsonProperty("issuePlace")]
        public string IssuePlace { get; set; }

        /// <summary>
        /// Код подразделения выдававшего документ
        /// </summary>
        [JsonProperty("departmentCode")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Дата выдачи документа удостоверяющего личность
        /// </summary>
        [JsonProperty("issueDate")]
        public DateTime? IssueDate { get; set; }

        #endregion
    }
}