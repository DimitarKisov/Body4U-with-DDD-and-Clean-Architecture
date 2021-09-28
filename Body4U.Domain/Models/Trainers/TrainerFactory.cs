namespace Body4U.Domain.Models.Trainers
{
    using Body4U.Domain.Exceptions;
    using Body4U.Domain.Factories.Trainers;

    internal class TrainerFactory : ITrainerFactory
    {
        private string bio = default!;
        private string shortBio = default!;
        private string facebookUrl = default!;
        private string instagramUrl = default!;
        private string youtubeChannelUrl = default!;

        private bool isBioSet = false;
        private bool isShortBioSet = false;

        public Trainer Build()
        {
            if (!isBioSet || !isShortBioSet)
            {
                throw new InvalidTrainerException("Bio and short bio must have value.");
            }

            return new Trainer(this.bio, this.shortBio, this.facebookUrl, this.instagramUrl, this.youtubeChannelUrl);
        }

        public ITrainerFactory WithBio(string bio)
        {
            this.bio = bio;
            this.isBioSet = true;

            return this;
        }

        public ITrainerFactory WithShortBio(string shortBio)
        {
            this.shortBio = shortBio;
            this.isShortBioSet = true;

            return this;
        }

        public ITrainerFactory WithFacebookUrl(string facebookUrl)
        {
            this.facebookUrl = facebookUrl;

            return this;
        }

        public ITrainerFactory WithInstagramUrl(string instagramUrl)
        {
            this.instagramUrl = instagramUrl;

            return this;
        }

        public ITrainerFactory WithYoutubeChannelUrl(string youtubeChannelUrl)
        {
            this.youtubeChannelUrl = youtubeChannelUrl;

            return this;
        }
    }
}
