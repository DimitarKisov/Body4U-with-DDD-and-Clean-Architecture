namespace Body4U.Application.Features
{
    public class EntityTokenCommand<T>
    {
        public T Id { get; set; } = default!;

        public T Token { get; set; } = default!;
    }

    public static class EntityTokenCommandExtensions
    {
        public static TCommand SetId<TCommand, T>(this TCommand command, T id)
            where TCommand : EntityTokenCommand<T>
        {
            command.Id = id;
            return command;
        }

        public static TCommand SetToken<TCommand, T>(this TCommand command, T token)
            where TCommand : EntityTokenCommand<T>
        {
            command.Token = token;
            return command;
        }
    }
}
