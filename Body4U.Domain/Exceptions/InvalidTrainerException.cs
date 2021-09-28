namespace Body4U.Domain.Exceptions
{
    public class InvalidTrainerException : BaseDomainException
    {
        public InvalidTrainerException(string message) => this.Error = message;

        public InvalidTrainerException()
        {
        }
    }
}
