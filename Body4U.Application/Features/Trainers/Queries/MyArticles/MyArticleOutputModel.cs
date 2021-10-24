namespace Body4U.Application.Features.Trainers.Queries.MyArticles
{
    using System;

    public class MyArticleOutputModel
    {
        public MyArticleOutputModel(
            int id,
            string image,
            string title,
            string content,
            DateTime datePosted,
            int articleType)
        {
            this.Id = id;
            this.Image = image;
            this.Title = title;
            this.Content = content;
            this.DatePosted = datePosted;
            this.ArticleType = articleType;
        }

        public int Id { get; }

        public string Image { get; }

        public string Title { get; }

        public string Content { get; }

        public DateTime DatePosted { get; }

        public int ArticleType { get; }
    }
}
