namespace Body4U.Domain.Models.Articles
{
    using Body4U.Domain.Exceptions;
    using FluentAssertions;
    using System;
    using Xunit;

    using static Body4U.Domain.Models.ModelDatas.Article;

    public class ArticleSpecs
    {
        [Theory]
        [InlineData(Sources)]
        [InlineData("")]
        public void ValidArticleShouldNotThrowException(string sources)
        {
            Action act = () => ArticleCreator(sources);

            act.Should().NotThrow<InvalidArticleException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(TitleLessThan5Chars)]
        [InlineData(TitleMoreThan100Chars)]
        public void InvalidTitleShouldThrowException(string title)
        {
            Action act = () => new Article(title, ImageCreator(), ContentCreator(), "", ArticleType.Eating);

            act.Should().Throw<InvalidArticleException>();
        }

        [Fact]
        public void InvalidImageShouldThrowException()
        {
            Action act = () => new Article(ValidTitle, new byte[0], ContentCreator(), "", ArticleType.Eating);

            act.Should().Throw<InvalidArticleException>();
        }

        [Fact]
        public void InvalidContentShouldThrowException()
        {
            var arr = new string[3] { new string('s', 49), new string('s', 25001), "" };

            for (int i = 0; i < arr.Length; i++)
            {
                Action act = () => new Article(ValidTitle, ImageCreator(), arr[i], "", ArticleType.Eating);

                act.Should().Throw<InvalidArticleException>();
            }
        }

        [Fact]
        public void UpdateTitleShouldWorkCorrectly()
        {
            var article = ArticleCreator();

            var currentTitle = article.Title;

            article.UpdateTitle("New valid title", "someGuid");

            var result = currentTitle == article.Title;

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateImageShoudlWorkCorrectly()
        {
            var article = ArticleCreator();

            var currentImage = article.Image;

            article.UpdateImage(new byte[1], "someGuid");

            var result = currentImage == article.Image;

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateContentShouldWorkCorrectly()
        {
            var article = ArticleCreator();

            var currentContent = article.Content;

            article.UpdateContent(ContentCreator() + "some content", "someGuid");

            var result = currentContent == article.Content;

            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateSourcesShouldWorkCorrectly()
        {
            var article = ArticleCreator();

            var currentSources = article.Sources;

            article.UpdateSources("some sources", "someGuid");

            var result = currentSources == article.Sources;

            result.Should().BeFalse();
        }

        private Article ArticleCreator(string sources = default!)
            => new Article(ValidTitle, ImageCreator(), ContentCreator(), sources, ArticleType.Eating);

        private byte[] ImageCreator()
        {
            var ch = 3; //number of channels (ie. assuming 24 bit RGB in this case)
            Random rnd = new Random();

            int imageByteSize = 50 * 100 * ch;

            byte[] imageData = new byte[imageByteSize]; //your image data buffer
            rnd.NextBytes(imageData);

            return imageData;
        }

        private string ContentCreator() => new string('s', 100);
    }
}
