namespace Body4U.Domain.Exceptions
{
    public class InvalidTrainerVideoException : BaseDomainException
    {
        public InvalidTrainerVideoException(string message) => this.Error = message;

        public InvalidTrainerVideoException()
        {

        }
    }
}
