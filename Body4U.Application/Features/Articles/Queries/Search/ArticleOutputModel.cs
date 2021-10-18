namespace Body4U.Application.Features.Articles.Queries.Search
{
    using System;

    public class ArticleOutputModel
    {
        public ArticleOutputModel(
            int id,
            string title,
            string content,
            string image,
            DateTime createdOn,
            string author,
            int articleType)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.Image = image;
            this.CreatedOn = createdOn;
            this.Author = author;
            this.ArticleType = articleType;
        }

        public int Id { get; }

        public string Title { get; }

        public string Content { get; }

        public string Image { get; }

        public DateTime CreatedOn { get; }

        public string Author { get; }

        public int ArticleType { get; }
    }
}
