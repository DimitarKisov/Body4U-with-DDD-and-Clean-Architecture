namespace Body4U.Domain.Models.Articles
{
    using Body4U.Domain.Common;

    public class ArticleType : Enumeration
    {
        public static readonly ArticleType Eating = new ArticleType(1, nameof(Eating));
        public static readonly ArticleType Recepies = new ArticleType(2, nameof(Recepies));
        public static readonly ArticleType Training = new ArticleType(3, nameof(Training));
        public static readonly ArticleType Supplements = new ArticleType(4, nameof(Supplements));

        private ArticleType(int value)
            : this(value, FromValue<ArticleType>(value).Name)
        {
        }

        private ArticleType(int value, string name)
            : base(value, name)
        {
        }
    }
}
