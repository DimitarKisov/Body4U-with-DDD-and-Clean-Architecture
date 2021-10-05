namespace Body4U.Infrastructure.Identity.Exceptions
{
    using System;

    public class InvalidApplicationUserException : Exception
    {
        private string? message;

        public string Error
        {
            get => this.message ?? base.Message;
            set => this.message = value;
        }
    }
}
