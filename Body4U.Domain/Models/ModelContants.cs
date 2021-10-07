namespace Body4U.Domain.Models
{
    public class ModelContants
    {
        public class User
        {
            public const string PhoneNumberRegex = @"^(\+)?(359|0)8[789]\d{1}(|-| )\d{3}(|-| )\d{3}$";
            public const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            public const int MinFirstNameLength = 3;
            public const int MaxFirstNameLength = 50;
            public const int MinLastNameLength = 3;
            public const int MaxLastNameLength = 50;
            public const int MinAge = 5;
            public const int MaxAge = 80;
            public const int MinPasswordLength = 6;
            public const int MaxPasswordLength = 20;
        }

        public class Article
        {
            public const int MinTitleLength = 5;
            public const int MaxTitleLength = 100;
            public const int MinContentLength = 50;
            public const int MaxContentLength = 25000;
        }

        public class Trainer
        {
            public const int MinBioLength = 200;
            public const int MaxBioLength = 500;
            public const int MinShortBioLenght = 30;
            public const int MaxShortBioLength = 200;
            public const string FacebookUrlRegex = @"(?:(?:http|https):\/\/)?(?:www.)?facebook.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[?\w\-]*\/)?(?:profile.php\?id=(?=\d.*))?([\w.\-]*)?";
            public const string InstragramUrlRegex = @"(?:(?:http|https):\/\/)?(?:www\.)?(?:instagram\.com|instagr\.am)\/([A-Za-z0-9-_\.]+)";
            public const string YoutubeChannelUrlRegex = @"((http|https):\/\/|)(www\.|)youtube\.com\/(channel\/|user\/)[a-zA-Z0-9\-]{1,}";
        }
    }
}
