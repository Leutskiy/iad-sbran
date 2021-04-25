using Sbran.Domain.Enums;
using System;
using System.Linq;

namespace Sbran.Domain.Entities
{
    /// <summary>
    /// Паспорт
    /// </summary>
    public sealed class Passport
    {
        /// <summary>
        /// Инициализировать паспорт
        /// </summary>
        public Passport()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Имя (по-русски)
        /// </summary>
        public string? NameRus { get; private set; }

        /// <summary>
        /// Имя (по-английски)
        /// </summary>
        public string? NameEng { get; private set; }

        /// <summary>
        /// Фамилия (по-русски)
        /// </summary>
        public string? SurnameRus { get; private set; }

        /// <summary>
        /// Фамилия (по-английски)
        /// </summary>
        public string? SurnameEng { get; private set; }

        /// <summary>
        /// Отчество (по-русски)
        /// </summary>
        public string? PatronymicNameRus { get; private set; }

        /// <summary>
        /// Отчество (по-ангийски)
        /// </summary>
        public string? PatronymicNameEng { get; private set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string? BirthPlace { get; private set; }

        /// <summary>
        /// Страна рождения
        /// </summary>
        public string? BirthCountry { get; private set; }

        /// <summary>
        /// Гражданство (подданство)
        /// </summary>
        public string? Citizenship { get; private set; }

        /// <summary>
        /// Местожительство
        /// </summary>
        public string? Residence { get; private set; }

        /// <summary>
        /// Страна постоянного проживания
        /// </summary>
        public string? ResidenceCountry { get; private set; }

        /// <summary>
        /// Регион в стране постоянного проживания
        /// </summary>
        public string? ResidenceRegion { get; private set; }

        /// <summary>
        /// Документ удостоверяющий личность
        /// </summary>
        public string? IdentityDocument { get; private set; }

        /// <summary>
        /// Место выдачи документа удостоверяющего личность
        /// </summary>
        public string? IssuePlace { get; private set; }

        /// <summary>
        /// Код подразделения выдававшего документ
        /// </summary>
        public string? DepartmentCode { get; private set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? BirthDate { get; private set; }

        /// <summary>
        /// Дата выдачи документа удостоверяющего личность
        /// </summary>
        public DateTime? IssueDate { get; private set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Sex? Gender { get; private set; }

        /// <summary>
        /// Задать имя по-русски
        /// </summary>
        /// <param name="nameRus">Имя по-русски</param>
        public void SetNameRus(string nameRus)
        {
            if (NameRus == nameRus)
            {
                return;
            }

            NameRus = nameRus;
        }

        /// <summary>
        /// Задать имя по-английски
        /// </summary>
        /// <param name="nameEng">Имя по-английски</param>
        public void SetNameEng(string nameEng)
        {
            if (NameEng == nameEng)
            {
                return;
            }

            NameEng = nameEng;
        }

        /// <summary>
        /// Задать фамилия по-русски
        /// </summary>
        /// <param name="surnameRus">Фамилия по-русски</param>
        public void SetSurnameRus(string surnameRus)
        {
            if (SurnameRus == surnameRus)
            {
                return;
            }

            SurnameRus = surnameRus;
        }

        /// <summary>
        /// Задать фамилия по-английски
        /// </summary>
        public void SetSurnameEng(string surnameEng)
        {
            if (SurnameEng == surnameEng)
            {
                return;
            }

            SurnameEng = surnameEng;
        }

        /// <summary>
        /// Задать отчество по-русски
        /// </summary>
        /// <param name="patronymicNameRus">Отчество по-русски</param>
        public void SetPatronymicNameRus(string patronymicNameRus)
        {
            if (PatronymicNameRus == patronymicNameRus)
            {
                return;
            }

            PatronymicNameRus = patronymicNameRus;
        }

        /// <summary>
        /// Задать отчество по-английски
        /// </summary>
        /// <param name="patronymicNameEng">Отчство по-английски</param>
        public void SetPatronymicNameEng(string patronymicNameEng)
        {
            if (PatronymicNameEng == patronymicNameEng)
            {
                return;
            }

            PatronymicNameEng = patronymicNameEng;
        }

        /// <summary>
        /// Задать место рождения
        /// </summary>
        /// <param name="birthPlace">Место рождения</param>
        public void SetBirthPlace(string birthPlace)
        {
            if (BirthPlace == birthPlace)
            {
                return;
            }

            BirthPlace = birthPlace;
        }

        /// <summary>
        /// Задать страну рождения
        /// </summary>
        /// <param name="birthCountry">Страна рождения</param>
        public void SetBirthCountry(string birthCountry)
        {
            if (BirthCountry == birthCountry)
            {
                return;
            }

            BirthCountry = birthCountry;
        }

        /// <summary>
        /// Задать гражданство (подданство)
        /// </summary>
        /// <param name="citizenship">Гражданство (подданство)</param>
        public void SetCitizenship(string citizenship)
        {
            if (Citizenship == citizenship)
            {
                return;
            }

            Citizenship = citizenship;
        }

        /// <summary>
        /// Задать местожительство
        /// </summary>
        /// <param name="residence">Местожительство</param>
        public void SetResidence(string residence)
        {
            if (Residence == residence)
            {
                return;
            }

            Residence = residence;
        }

        /// <summary>
        /// Задать страну проживания
        /// </summary>
        /// <param name="residenceCountry">Страна проживания</param>
        public void SetResidenceCountry(string residenceCountry)
        {
            if (ResidenceCountry == residenceCountry)
            {
                return;
            }

            ResidenceCountry = residenceCountry;
        }

        /// <summary>
        /// Задать регион проживания
        /// </summary>
        /// <param name="residenceRegion">Регион проживания</param>
        public void SetResidenceRegion(string residenceRegion)
        {
            if (ResidenceRegion == residenceRegion)
            {
                return;
            }

            ResidenceRegion = residenceRegion;
        }

        /// <summary>
        /// Задать документ идентифицирующий личность
        /// </summary>
        /// <param name="identityDocument">Документ идентифицирующий личность</param>
        public void SetIdentityDocument(string identityDocument)
        {
            if (IdentityDocument == identityDocument)
            {
                return;
            }

            IdentityDocument = identityDocument;
        }

        /// <summary>
        /// Задать место выдачи документа идентифицирующего личность
        /// </summary>
        /// <param name="issuePlace">Место выдачи документа идентифицирующего личность</param>
        public void SetIssuePlace(string issuePlace)
        {
            if (IssuePlace == issuePlace)
            {
                return;
            }

            IssuePlace = issuePlace;
        }

        /// <summary>
        /// Задать код подразделения выдававшего документ идентифицирующего личность
        /// </summary>
        /// <param name="departmentCode">Код подразделения выдававшего документ идентифицирующего личность</param>
        public void SetDepartmentCode(string departmentCode)
        {
            if (DepartmentCode == departmentCode)
            {
                return;
            }

            DepartmentCode = departmentCode;
        }

        /// <summary>
        /// Задать дату рождения
        /// </summary>
        /// <param name="birthDate">Дата рождения</param>
        public void SetBirthDate(DateTime? birthDate)
        {
            if (BirthDate == birthDate)
            {
                return;
            }

            BirthDate = birthDate;
        }

        /// <summary>
        /// Задать дату выдачи документа идентифицирующего личность
        /// </summary>
        /// <param name="issueDate">Дата выдачи документа идентифицирующего личность</param>
        public void SetIssueDate(DateTime? issueDate)
        {
            if (IssueDate == issueDate)
            {
                return;
            }

            IssueDate = issueDate;
        }

        /// <summary>
        /// Задать пол
        /// </summary>
        /// <param name="gender">Пол</param>
        public void SetGender(Sex? gender)
        {
            if (Gender == gender)
            {
                return;
            }

            Gender = gender;
        }

        public string ToFio()
        {
            var fio = string
                .Join(
                    separator: " ",
                    values: new[] {
                        SurnameRus,
                        NameRus,
                        PatronymicNameRus
                    }.Where(name => name != null))
                .Trim();

            return fio;
        }
    }
}