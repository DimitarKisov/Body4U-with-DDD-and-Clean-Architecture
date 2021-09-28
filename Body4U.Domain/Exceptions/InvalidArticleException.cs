namespace Body4U.Domain.Exceptions
{
    public class InvalidArticleException : BaseDomainException
    {
        public InvalidArticleException(string message) => this.Error = message;

        public InvalidArticleException()
        {
        }
    }
}
