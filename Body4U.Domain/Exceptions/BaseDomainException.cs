namespace Body4U.Domain.Exceptions
{
    using System;

    public abstract class BaseDomainException : Exception
    {
        private string? message;

        public string Error
        {
            get => this.message ?? base.Message;
            set => this.message = value;
        }
    }
}
