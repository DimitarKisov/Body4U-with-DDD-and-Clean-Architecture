namespace Body4U.Domain.Factories.Trainers
{
    using Body4U.Domain.Models.Trainers;

    internal class TrainerFactory : ITrainerFactory
    {
        public Trainer Build()
            => new Trainer(default!, default!, default!, default!, default!);
    }
}
