namespace Body4U.Infrastructure.Persistence.Repositories
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Trainers;
    using Body4U.Domain.Models.Trainers;
    using Body4U.Infrastructure.Identity.Models;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;

    internal class TrainerRepository : DataRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Result> Delete(IUser user, CancellationToken cancellationToken)
        {
            try
            {
                var castedUser = user as ApplicationUser;
                var trainer = castedUser!.Trainer;

                castedUser.DeleteTrainer();

                this.Data.Trainers.Remove(trainer!);
                await this.Data.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(TrainerRepository)}.{nameof(this.Delete)}", ex);
                return Result.Failure(string.Format(Wrong, nameof(this.Delete)));
            }
        }
    }
}
