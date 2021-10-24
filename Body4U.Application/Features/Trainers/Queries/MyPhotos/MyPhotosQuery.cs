namespace Body4U.Application.Features.Trainers.Queries.MyPhotos
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.Account;

    public class MyPhotosQuery : IRequest<Result<IEnumerable<MyPhotosOutputModel>>>
    {
        public class MyPhotosQueryHandler : IRequestHandler<MyPhotosQuery, Result<IEnumerable<MyPhotosOutputModel>>>
        {
            private readonly ITrainerRepository trainerRepository;
            private readonly ICurrentUserService currentUserService;

            public MyPhotosQueryHandler(
                ITrainerRepository trainerRepository,
                ICurrentUserService currentUserService)
            {
                this.trainerRepository = trainerRepository;
                this.currentUserService = currentUserService;
            }

            public async Task<Result<IEnumerable<MyPhotosOutputModel>>> Handle(MyPhotosQuery request, CancellationToken cancellationToken)
            {
                if (currentUserService.TrainerId == null)
                {
                    return Result<IEnumerable<MyPhotosOutputModel>>.Failure(Unauthorized);
                }

                return await this.trainerRepository.MyPhotos((int)currentUserService.TrainerId, cancellationToken);
            }
        }
    }
}
