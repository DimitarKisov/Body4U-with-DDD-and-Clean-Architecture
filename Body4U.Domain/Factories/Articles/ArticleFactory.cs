namespace Body4U.Domain.Factories.Articles
{
    using Body4U.Domain.Exceptions;
    using Body4U.Domain.Models.Articles;

    internal class ArticleFactory : IArticleFactory
    {
        private string title = default!;
        private byte[] image = default!;
        private string content = default!;
        private ArticleType articleType = default!;
        private string sources = default!;

        private bool isTitleSet = false;
        private bool isImageSet = false;
        private bool isContentSet = false;
        private bool isArticleTypeSet = false;

        public Article Build()
        {
            if (!isTitleSet || !isImageSet || !isContentSet || !isArticleTypeSet)
            {
                throw new InvalidArticleException("Title, image, content and type must have a value.");
            }

            return new Article(this.title, this.image, this.content, this.sources, this.articleType);
        }

        public IArticleFactory WithTitle(string title)
        {
            this.title = title;
            this.isTitleSet = true;

            return this;
        }

        public IArticleFactory WithImage(byte[] image)
        {
            this.image = image;
            this.isImageSet = true;

            return this;
        }

        public IArticleFactory WithContent(string content)
        {
            this.content = content;
            this.isContentSet = true;

            return this;
        }

        public IArticleFactory WithType(ArticleType articleType)
        {
            this.articleType = articleType;
            this.isArticleTypeSet = true;

            return this;
        }

        public IArticleFactory WithSources(string sources)
        {
            this.sources = sources;

            return this;
        }
    }
}
