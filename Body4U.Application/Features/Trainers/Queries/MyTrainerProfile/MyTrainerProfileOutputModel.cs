namespace Body4U.Application.Features.Trainers.Queries.MyTrainerProfile
{
    public class MyTrainerProfileOutputModel
    {
        public MyTrainerProfileOutputModel(
            int id,
            string? bio,
            string? shortBio,
            string? facebookUrl,
            string? instagramUrl,
            string? youtubeChannelUrl)
        {
            this.Id = id;
            this.Bio = bio;
            this.ShortBio = shortBio;
            this.FacebookUrl = facebookUrl;
            this.InstagramUrl = instagramUrl;
            this.YoutubeChannelUrl = youtubeChannelUrl;
        }

        public int Id { get; }

        public string? Bio { get; }

        public string? ShortBio { get; }

        public string? FacebookUrl { get; }

        public string? InstagramUrl { get; }

        public string? YoutubeChannelUrl { get; }
    }
}
