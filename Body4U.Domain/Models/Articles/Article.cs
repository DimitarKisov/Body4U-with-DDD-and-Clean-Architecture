namespace Body4U.Domain.Models.Articles
{
    using Body4U.Domain.Common;
    using Body4U.Domain.Exceptions;
    using System;

    using static Body4U.Domain.Models.ModelConstants.Article;

    public class Article : Entity<int>, IAggregateRoot
    {
        public Article(
            string title,
            byte[] image,
            string content,
            string? sources,
            ArticleType articleType)
        {
            this.Validate(title, content, image);

            this.Title = title;
            this.Image = image;
            this.Content = content;
            this.Sources = sources;
            this.ArticleType = articleType;
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = null;
            this.ModifiedBy = null;
        }

        protected Article(
            string title,
            byte[] image,
            string content,
            string? sources)
        {
            this.Title = title;
            this.Image = image;
            this.Content = content;
            this.Sources = sources;
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = null;
            this.ModifiedBy = null;

            this.ArticleType = default!;
        }

        public string Title { get; private set; }

        public byte[] Image { get; private set; }

        public string Content { get; private set; }

        public string? Sources { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public DateTime? ModifiedOn { get; private set; }

        public string? ModifiedBy { get; private set; }

        public ArticleType ArticleType { get; private set; }

        #region State mutation methods
        public Article UpdateTitle(string title, string userId)
        {
            this.ValidateTitle(title);

            this.Title = title;
            Modification(userId);

            return this;
        }

        public Article UpdateImage(byte[] image, string userId)
        {
            //TODO: Тъй като преди това е IFormFile и там си има проверки, не съм сигурен на дали ще ни е нужна тази проверка
            this.ValidateImage(image);

            this.Image = image;
            Modification(userId);

            return this;
        }

        public Article UpdateContent(string content, string userId)
        {
            this.ValidateContent(content);

            this.Content = content;
            Modification(userId);

            return this;
        }

        public Article UpdateSources(string sources, string userId)
        {
            var isEmpty = this.ValidateSources(sources);

            if (!isEmpty)
            {
                this.Sources = sources;
                Modification(userId);
            }

            return this;
        }

        private void Modification(string userId)
        {
            this.ModifiedOn = DateTime.Now;
            this.ModifiedBy = userId;
        }
        #endregion

        #region Validations
        private void Validate(string title, string content, byte[] image)
        {
            this.ValidateTitle(title);
            this.ValidateContent(content);
            this.ValidateImage(image);
        }

        private void ValidateTitle(string title)
        {
            Guard.AgainstEmptyString<InvalidArticleException>(title, nameof(this.Title));

            Guard.ForStringLength<InvalidArticleException>(title, MinTitleLength, MaxTitleLength, nameof(this.Title));
        }

        //TODO: Тъй като преди това е IFormFile и там си има проверки, не съм сигурен на дали ще ни е нужна тази проверка
        private void ValidateImage(byte[] image)
        {
            Guard.AgaintsEmptyFile<InvalidArticleException>(image, nameof(this.Image));
        }

        private void ValidateContent(string content)
        {
            Guard.AgainstEmptyString<InvalidArticleException>(content, nameof(this.Content));

            Guard.ForStringLength<InvalidArticleException>(content, MinContentLength, MaxContentLength, nameof(this.Content));
        }
        
        private bool ValidateSources(string sources)
        {
            if (!string.IsNullOrWhiteSpace(sources))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
