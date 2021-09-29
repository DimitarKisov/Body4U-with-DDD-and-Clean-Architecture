namespace Body4U.Application.Features.Identity
{
    using Body4U.Domain.Models.Trainers;

    public interface IUser
    {
        void BecomeTrainer(Trainer trainer);
    }
}
