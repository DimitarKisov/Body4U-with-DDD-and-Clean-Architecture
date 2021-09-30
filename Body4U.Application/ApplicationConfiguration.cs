namespace Body4U.Application
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services
                .AddMediatR(Assembly.GetExecutingAssembly());
    }
}
