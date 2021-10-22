namespace Body4U.Application.Features.Trainers.Queries.MyTrainerProfile
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

    public class MyTrainerProfileQuery : IRequest<Result<MyTrainerProfileOutputModel>>
    {
        public class MyTrainerProfileQueryHandler : IRequestHandler<MyTrainerProfileQuery, Result<MyTrainerProfileOutputModel>>
        {
            private readonly ITrainerRepository trainerRepository;
            private readonly ICurrentUserService currentUserService;

            public MyTrainerProfileQueryHandler(
                ITrainerRepository trainerRepository,
                ICurrentUserService currentUserService)
            {
                this.trainerRepository = trainerRepository;
                this.currentUserService = currentUserService;
            }

            public async Task<Result<MyTrainerProfileOutputModel>> Handle(MyTrainerProfileQuery request, CancellationToken cancellationToken)
            {
                if (currentUserService.TrainerId == null)
                {
                    return Result<MyTrainerProfileOutputModel>.Failure(Unauthorized);
                }

                return await this.trainerRepository.MyTrainerProfile((int)currentUserService.TrainerId, cancellationToken);
            }
        }
    }
}
