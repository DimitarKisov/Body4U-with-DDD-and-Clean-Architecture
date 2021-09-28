namespace Body4U.Domain.Models
{
    public class ModelDatas
    {
        public class Article
        {
            public const string ValidTitle = "Valid title";
            public const string Sources = "Travell and Simons myofascial pain and dysfunction: the trigger point manual, upper half of body’’ Vol.1 2nd edn. Baltimore: Williams & Wilkins";

            public const string TitleLessThan5Chars = "Less";
            public const string TitleMoreThan100Chars = "Invalid Title Invalid Title Invalid Title Invalid Title Invalid Title Invalid Title Invalid Title Invalid";
        }

        public class Trainer
        {
            public const string ValidBio = "Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid bio Valid";
            public const string ValidShortBio = "Valid short bio Valid short bio";
            public const string ValidFacebookUrl = "https://www.facebook.com/ValidProfile";
            public const string ValidInstragramUrl = "https://www.instagram.com/ValidProfile";
            public const string ValidYoutubeChannelUrl = "https://www.youtube.com/channel/UCUNC9hmB6I7MvY2w2LF5fUQ";

            public const string BioLessThanMinRequirement = "Invalid Bio";
            public const string BioMoreThanMaxRequirement = "Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio Invalid Bio";
            public const string ShortBioLessThanMinRequirement = "Invalid short bio";
            public const string ShortBioMoreThanMaxRequirement = "Invalid short bio Invalid short bio Invalid short bio Invalid short bio Invalid short bio Invalid short bio Invalid short bio Invalid short bio Invalid short bio Invalid short bio Invalid short bio bio";
            public const string InvalidSocialAccountUrl = "https://www.somesite.com";
        }
    }
}
