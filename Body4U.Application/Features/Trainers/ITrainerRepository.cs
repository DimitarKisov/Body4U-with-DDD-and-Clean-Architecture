namespace Body4U.Application.Features.Trainers
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Trainers.Queries.MyPhotos;
    using Body4U.Application.Features.Trainers.Queries.MyTrainerProfile;
    using Body4U.Application.Features.Trainers.Queries.MyVideos;
    using Body4U.Domain.Models.Trainers;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITrainerRepository : IRepository<Trainer>
    {
        Task<Result<Trainer>> Find(int trainerId, CancellationToken cancellationToken);

        Task<Result<MyTrainerProfileOutputModel>> MyTrainerProfile(int trainerId, CancellationToken cancellationToken);

        Task<Result<IEnumerable<MyPhotosOutputModel>>> MyPhotos(int trainerId, CancellationToken cancellationToken);

        Task<Result<IEnumerable<MyVideosOutputModel>>> MyVideos(int trainerId, CancellationToken cancellationToken);

        Task<Result> Delete(IUser user, CancellationToken cancellationToken);
    }
}
