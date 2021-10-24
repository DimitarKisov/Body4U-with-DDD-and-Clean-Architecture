namespace Body4U.Application.Features.Trainers.Commands.EditTrainer
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class EditTrainerCommand : IRequest<Result>
    {
        public EditTrainerCommand(
            string? bio,
            string? shortBio,
            string? facebookUrl,
            string? instagramUrl,
            string? youtubeChannelUrl)
        {
            this.Bio = bio;
            this.ShortBio = shortBio;
            this.FacebookUrl = facebookUrl;
            this.InstagramUrl = instagramUrl;
            this.YoutubeChannelUrl = youtubeChannelUrl;
        }

        public string? Bio { get; }

        public string? ShortBio { get; }

        public string? FacebookUrl { get; }

        public string? InstagramUrl { get; }

        public string? YoutubeChannelUrl { get; }

        public class EditTrainerCommandHandler : IRequestHandler<EditTrainerCommand, Result>
        {
            private readonly ITrainerRepository trainerRepository;
            private readonly ICurrentUserService currentUserService;

            public EditTrainerCommandHandler(
                ITrainerRepository trainerRepository,
                ICurrentUserService currentUserService)
            {
                this.trainerRepository = trainerRepository;
                this.currentUserService = currentUserService;
            }

            public async Task<Result> Handle(EditTrainerCommand request, CancellationToken cancellationToken)
            {
                var trainerResult = await this.trainerRepository.Find((int)currentUserService.TrainerId!, cancellationToken);
                if (!trainerResult.Succeeded)
                {
                    return trainerResult;
                }

                trainerResult.Data
                    .UpdateBio(request.Bio!, currentUserService.UserId)
                    .UpdateShortBio(request.ShortBio!, currentUserService.UserId)
                    .UpdateFacebookUrl(request.FacebookUrl!, currentUserService.UserId)
                    .UpdateInstagramUrl(request.InstagramUrl!, currentUserService.UserId)
                    .UpdateYoutubeChannelUrl(request.YoutubeChannelUrl!, currentUserService.UserId);

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
