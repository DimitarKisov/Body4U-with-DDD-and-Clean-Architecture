namespace Body4U.Application.Features.Identity
{
    using Body4U.Domain.Common;
    using Body4U.Domain.Models.Trainers;

    public interface IUser : IAggregateRoot
    {
        void BecomeTrainer(Trainer trainer);

        void DeleteTrainer();
    }
}
