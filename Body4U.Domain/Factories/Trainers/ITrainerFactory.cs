namespace Body4U.Domain.Factories.Trainers
{
    using Body4U.Domain.Models.Trainers;

    public interface ITrainerFactory : IFactory<Trainer>
    {
        ITrainerFactory WithBio(string bio);

        ITrainerFactory WithShortBio(string shortBio);

        ITrainerFactory WithFacebookUrl(string facebookUrl);

        ITrainerFactory WithInstagramUrl(string instagramUrl);

        ITrainerFactory WithYoutubeChannelUrl(string youtubeChannelUrl);
    }
}
