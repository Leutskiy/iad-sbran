using System;
using System.Security.Cryptography;

namespace Sbran.Domain.Entities.System
{
    /// <summary>
    /// Профиль
    /// </summary>
    public sealed class Profile
    {
        /// <summary>
        /// Первоначальная инициализация
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="ordinalNumber">Порядковый номер</param>
        public Profile()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Фотография
        /// </summary>
        public byte[]? Photo { get; private set; }

        /// TODO: это коллекция, нужно переделать
        /// <summary>
        /// Коллекция веб-страниц
        /// </summary>
        public string? WebPages { get; private set; }

        /// <summary>
        /// Установить фото
        /// </summary>
        /// <param name="photo">Фото</param>
        public void SetPhoto(byte[] photo)
        {
            if (Photo != null && CreateMd5(Photo) == CreateMd5(photo))
            {
                return;
            }

            Photo = photo;
        }

        /// <summary>
        /// Установить веб-сраницы
        /// </summary>
        /// <param name="webpages">Веб-страницы</param>
        public void SetWebPages(string webpages)
        {
            if (WebPages == webpages)
            {
                return;
            }

            WebPages = webpages;
        }

        /// <summary>
        /// Создать MD5 хэш по контенту
        /// </summary>
        /// <param name="content">Контент</param>
        /// <returns>MD5 хэш контента</returns>
        private static string CreateMd5(byte[] content)
        {
			using var md5Hasher = MD5.Create();
			var md5Hash = md5Hasher.ComputeHash(content);
			return Convert.ToBase64String(md5Hash);
		}
    }
}