namespace Body4U.Application.Features.Trainers
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using Body4U.Application.Features.Identity;
    using Body4U.Domain.Models.Trainers;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITrainerRepository : IRepository<Trainer>
    {
        Task<Result> Delete(IUser user, CancellationToken cancellationToken);
    }
}
