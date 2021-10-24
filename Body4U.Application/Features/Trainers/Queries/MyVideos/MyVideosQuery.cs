namespace Body4U.Application.Features.Trainers.Queries.MyVideos
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

    public class MyVideosQuery : IRequest<Result<IEnumerable<MyVideosOutputModel>>>
    {
        public class MyVideosQueryHandler : IRequestHandler<MyVideosQuery, Result<IEnumerable<MyVideosOutputModel>>>
        {
            private readonly ITrainerRepository trainerRepository;
            private readonly ICurrentUserService currentUserService;

            public MyVideosQueryHandler(
                ITrainerRepository trainerRepository,
                ICurrentUserService currentUserService)
            {
                this.trainerRepository = trainerRepository;
                this.currentUserService = currentUserService;
            }

            public async Task<Result<IEnumerable<MyVideosOutputModel>>> Handle(MyVideosQuery request, CancellationToken cancellationToken)
            {
                if (currentUserService.TrainerId == null)
                {
                    return Result<IEnumerable<MyVideosOutputModel>>.Failure(Unauthorized);
                }

                return await this.trainerRepository.MyVideos((int)currentUserService.TrainerId, cancellationToken);
            }
        }
    }
}
