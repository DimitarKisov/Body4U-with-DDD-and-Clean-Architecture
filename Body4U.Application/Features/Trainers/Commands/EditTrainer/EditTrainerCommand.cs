namespace Body4U.Application.Features.Trainers.Commands.EditTrainer
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class EditTrainerCommand : IRequest<Result>
    {
        public EditTrainerCommand(
            int trainerId,
            string userId,
            string bio,
            string shortBio,
            string facebookUrl,
            string instagramUrl,
            string youtubeChannelUrl)
        {
            this.TrainerId = trainerId;
            this.UserId = userId;
            this.Bio = bio;
            this.ShortBio = shortBio;
            this.FacebookUrl = facebookUrl;
            this.InstagramUrl = instagramUrl;
            this.YoutubeChannelUrl = youtubeChannelUrl;
        }

        public int TrainerId { get; }

        public string UserId { get; }

        public string Bio { get; }

        public string ShortBio { get; }

        public string FacebookUrl { get; }

        public string InstagramUrl { get; }

        public string YoutubeChannelUrl { get; }

        public class EditTrainerCommandHandler : IRequestHandler<EditTrainerCommand, Result>
        {
            private readonly ITrainerRepository trainerRepository;

            public EditTrainerCommandHandler(ITrainerRepository trainerRepository)
            {
                this.trainerRepository = trainerRepository;
            }

            public async Task<Result> Handle(EditTrainerCommand request, CancellationToken cancellationToken)
            {
                var trainerResult = await this.trainerRepository.Find(request.TrainerId, cancellationToken);
                if (!trainerResult.Succeeded)
                {
                    return trainerResult;
                }

                trainerResult.Data
                    .UpdateBio(request.Bio, request.UserId)
                    .UpdateShortBio(request.ShortBio, request.UserId)
                    .UpdateFacebookUrl(request.FacebookUrl, request.UserId)
                    .UpdateInstagramUrl(request.InstagramUrl, request.UserId)
                    .UpdateYoutubeChannelUrl(request.YoutubeChannelUrl, request.UserId);

                if (trainerResult.Data.Bio != null &&
                    trainerResult.Data.ShortBio != null &&
                    trainerResult.Data.FacebookUrl != null &&
                    trainerResult.Data.InstagramUrl != null &&
                    trainerResult.Data.YoutubeChannelUrl != null)
                {
                    trainerResult.Data.ChangeOpportunityToWrite(true);
                }
                else
                {
                    trainerResult.Data.ChangeOpportunityToWrite(false);
                }

                await this.trainerRepository.Save(trainerResult.Data);

                return trainerResult;
            }
        }
    }
}
