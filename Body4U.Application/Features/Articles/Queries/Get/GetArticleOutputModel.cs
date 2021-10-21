namespace Body4U.Application.Features.Articles.Queries.Get
{
    using System;

    public class GetArticleOutputModel
    {
        public GetArticleOutputModel(
            int id,
            string title,
            string content,
            string image,
            DateTime datePosted,
            string authorName,
            int articleType,
            int userId,
            string shortBio,
            string authorProfilePicture,
            string authorFacebook,
            string authorInstagram,
            string authorYoutubeChannel)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.Image = image;
            this.DatePosted = datePosted;
            this.AuthorName = authorName;
            this.ArticleType = articleType;
            this.UserId = userId;
            this.ShortBio = shortBio;
            this.AuthorProfilePicture = authorProfilePicture;
            this.AuthorFacebook = authorFacebook;
            this.AuthorInstagram = authorInstagram;
            this.AuthorYoutubeChannel = authorYoutubeChannel;
        }

        public int Id { get; }

        public string Title { get; }

        public string Content { get; }

        public string Image { get; }

        public DateTime DatePosted { get; }

        public string AuthorName { get; }

        public int ArticleType { get; }

        public int UserId { get; }

        public string ShortBio { get; }

        public string AuthorProfilePicture { get; }

        public string AuthorFacebook { get; }

        public string AuthorInstagram { get; }

        public string AuthorYoutubeChannel { get; }
    }
}
