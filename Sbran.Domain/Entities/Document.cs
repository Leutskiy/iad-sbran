using Sbran.Domain.Enums;
using System;

namespace Sbran.Domain.Entities
{
	/// <summary>
	/// Документ
	/// </summary>
	public sealed class Document
    {
		private Document()
		{
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Инициализировать документ
        /// </summary>
        public Document(
            Invitation invitation,
            string name,
            byte[] content,
            DocumentType documentType) : this()
        {
            InvitationId = invitation.Id;
            Invitation = invitation;

            Name = name;
            Content = content;
            DocumentType = documentType;

            CreatedDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идентификатор приглашения
        /// </summary>
        public Guid InvitationId { get; private set; }

		public Invitation Invitation { get; private set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        public byte[] Content { get; private set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public DocumentType DocumentType { get; private set; }

        #region TODO: сделать как shadow properties

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; private set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        #endregion
    }
}