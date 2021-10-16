namespace Body4U.Application.Contracts
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        int? TrainerId { get; }
    }
}
