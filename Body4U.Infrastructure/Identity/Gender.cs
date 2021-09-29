namespace Body4U.Infrastructure.Identity
{
    using Body4U.Domain.Common;

    public class Gender : Enumeration
    {
        private static readonly Gender Male = new Gender(1, nameof(Male));
        private static readonly Gender Female = new Gender(2, nameof(Female));
        private static readonly Gender Other = new Gender(3, nameof(Other));

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
