namespace Body4U.Domain.Models.Trainers
{
    using Body4U.Domain.Common;
    using Body4U.Domain.Exceptions;

    public class TrainerVideo : Entity<int>
    {
        private const string regexPattern = @"^(?:https?\:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v\=))([\w-]{10,12})(?:$|\&|\?\#).*";

        internal TrainerVideo(string videoUrl)
        {
            this.Validate(videoUrl);

            this.VideoUrl = videoUrl;
        }

        public string VideoUrl { get; }

        private void Validate(string videoUrl)
            => Guard.ForRegexExpression<InvalidTrainerVideoException>(videoUrl, regexPattern, nameof(this.VideoUrl));
    }
}