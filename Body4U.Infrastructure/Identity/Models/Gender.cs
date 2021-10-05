namespace Body4U.Infrastructure.Identity.Models
{
    using Body4U.Domain.Common;

    public class Gender : Enumeration
    {
        public static readonly Gender Male = new Gender(1, nameof(Male));
        public static readonly Gender Female = new Gender(2, nameof(Female));
        public static readonly Gender Other = new Gender(3, nameof(Other));

        private Gender(int value)
            : this(value, FromValue<Gender>(value).Name)
        {
        }

        private Gender(int value, string name)
            : base(value, name)
        {
        }
    }
}
