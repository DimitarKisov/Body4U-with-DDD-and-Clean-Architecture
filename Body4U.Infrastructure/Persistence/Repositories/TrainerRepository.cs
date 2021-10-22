namespace Body4U.Infrastructure.Persistence.Repositories
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Body4U.Application.Features.Trainers;
    using Body4U.Application.Features.Trainers.Queries.MyTrainerProfile;
    using Body4U.Domain.Models.Trainers;
    using Body4U.Infrastructure.Identity.Models;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using static Body4U.Application.Common.GlobalConstants.System;
    using static Body4U.Application.Common.GlobalConstants.Trainer;

    internal class TrainerRepository : DataRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Result<Trainer>> Find(int trainerId, CancellationToken cancellationToken)
        {
            try
            {
                var trainer = await this.Data
                .Trainers
                .FindAsync(new object[] { trainerId }, cancellationToken);

                return trainer != null
                    ? Result<Trainer>.SuccessWith(trainer)
                    : Result<Trainer>.Failure(string.Format(WrongId, trainerId));

            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(TrainerRepository)}.{nameof(this.Find)}", ex);
                return Result<Trainer>.Failure(string.Format(Wrong, nameof(this.Find)));
            }

        }

        public async Task<Result<MyTrainerProfileOutputModel>> MyTrainerProfile(int trainerId, CancellationToken cancellationToken)
        {
            try
            {
                var trainer = await this.Data.Trainers.FindAsync(new object[] { trainerId }, cancellationToken);

                if (trainer == null)
                {
                    return Result<MyTrainerProfileOutputModel>.Failure(string.Format(WrongId, trainerId));
                }

                var result = new MyTrainerProfileOutputModel
                    (
                        trainer.Id,
                        trainer.Bio,
                        trainer.ShortBio,
                        trainer.FacebookUrl,
                        trainer.InstagramUrl,
                        trainer.YoutubeChannelUrl
                    );

                return Result<MyTrainerProfileOutputModel>.SuccessWith(result);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(TrainerRepository)}.{nameof(this.MyTrainerProfile)}", ex);
                return Result<MyTrainerProfileOutputModel>.Failure(string.Format(Wrong, nameof(this.MyTrainerProfile)));
            }
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
