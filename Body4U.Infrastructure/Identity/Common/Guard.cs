namespace Body4U.Infrastructure.Identity.Common
{
    using Body4U.Infrastructure.Identity.Exceptions;
    using Body4U.Infrastructure.Identity.Models;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class Guard
    {
        private const int FileZeroLength = 0;

        public static void AgainstEmptyString<TException>(string value, string name = "Value")
            where TException : InvalidApplicationUserException, new()
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            ThrowException<TException>($"{name} cannot be null ot empty.");
        }

        public static void ForStringLength<TException>(string value, int minLength, int maxLength, string name = "Value")
            where TException : InvalidApplicationUserException, new()
        {
            AgainstEmptyString<TException>(value, name);

            if (minLength <= value.Length && value.Length <= maxLength)
            {
                return;
            }

            ThrowException<TException>($"{name} must have between {minLength} and {maxLength} symbols.");
        }

        public static void AgaintsEmptyFile<TException>(byte[] file, string name = "Value")
            where TException : InvalidApplicationUserException, new()
        {
            if (file.Length > FileZeroLength)
            {
                return;
            }

            ThrowException<TException>($"{name} cannot be empty.");
        }

        public static void ForValidGender<TException>(Gender gender, string name = "Value")
            where TException : InvalidApplicationUserException, new()
        {
            var validGenderValues = Domain.Common.Enumeration.GetAll<Gender>();

            if (!validGenderValues.Any(x => x.Value == gender.Value))
            {
                ThrowException<TException>($"{name} is invalid.");
            }
        }

        public static void AgainstOutOfRange<TException>(int number, int min, int max, string name = "Value")
            where TException : InvalidApplicationUserException, new()
        {
            if (min <= number && number <= max)
            {
                return;
            }

            ThrowException<TException>($"{name} must be between {min} and {max}.");
        }

        public static void ForRegexExpression<TException>(string text, string regexPattern, string name = "Value")
            where TException : InvalidApplicationUserException, new()
        {
            var isMatch = Regex.IsMatch(text, regexPattern);

            if (isMatch)
            {
                return;
            }

            ThrowException<TException>($"{name} is not a valid url.");
        }

        private static void ThrowException<TException>(string message)
            where TException : InvalidApplicationUserException, new()
        {
            var exception = new TException
            {
                Error = message
            };

            throw exception;
        }
    }
}
