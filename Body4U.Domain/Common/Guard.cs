﻿namespace Body4U.Domain.Common
{
    using System;
    using System.Text.RegularExpressions;
    using Exceptions;
    using Models;

    public static class Guard
    {
        private const int FileZeroLength = 0;

        public static void AgainstEmptyString<TException>(string value, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            ThrowException<TException>($"{name} cannot be null ot empty.");
        }

        public static void ForStringLength<TException>(string value, int minLength, int maxLength, string name = "Value")
            where TException : BaseDomainException, new()
        {
            AgainstEmptyString<TException>(value, name);

            if (minLength <= value.Length && value.Length <= maxLength)
            {
                return;
            }

            ThrowException<TException>($"{name} must have between {minLength} and {maxLength} symbols.");
        }

        public static void AgaintsEmptyFile<TException>(byte[] file, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (file.Length > FileZeroLength)
            {
                return;
            }

            ThrowException<TException>($"{name} cannot be empty.");
        }

        public static void ForRegexExpression<TException>(string text, string regexPattern, string name = "Value")
            where TException : BaseDomainException, new()
        {
            var isMatch = Regex.IsMatch(text, regexPattern);

            if (isMatch)
            {
                return;
            }

            ThrowException<TException>($"{name} is not a valid url.");
        }

        //public static void AgainstOutOfRange<TException>(int number, int min, int max, string name = "Value")
        //    where TException : BaseDomainException, new()
        //{
        //    if (min <= number && number <= max)
        //    {
        //        return;
        //    }

        //    ThrowException<TException>($"{name} must be between {min} and {max}.");
        //}

        //public static void AgainstOutOfRange<TException>(decimal number, decimal min, decimal max, string name = "Value")
        //    where TException : BaseDomainException, new()
        //{
        //    if (min <= number && number <= max)
        //    {
        //        return;
        //    }

        //    ThrowException<TException>($"{name} must be between {min} and {max}.");
        //}

        public static void Against<TException>(object actualValue, object unexpectedValue, string name = "Value")
            where TException : BaseDomainException, new()
        {
            if (!actualValue.Equals(unexpectedValue))
            {
                return;
            }

            ThrowException<TException>($"{name} must not be {unexpectedValue}.");
        }

        private static void ThrowException<TException>(string message)
            where TException : BaseDomainException, new()
        {
            var exception = new TException
            {
                Error = message
            };

            throw exception;
        }
    }
}
