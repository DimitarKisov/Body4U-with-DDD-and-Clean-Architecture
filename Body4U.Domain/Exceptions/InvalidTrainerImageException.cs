namespace Body4U.Domain.Exceptions
{
    public class InvalidTrainerImageException : BaseDomainException
    {
        public InvalidTrainerImageException(string message) => this.Error = message;

        public InvalidTrainerImageException()
        {
        }
    }
}
