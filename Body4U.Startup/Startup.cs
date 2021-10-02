namespace Body4U.Startup
{
    using Body4U.Application;
    using Body4U.Domain;
    using Body4U.Infrastructure;
    using Body4U.Web;
    using Body4U.Startup.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    #pragma warning disable CS1591
    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddDomain()
                .AddApplication()
                .AddInfrastructure(this.Configuration)
                .AddWebComponents()
                .AddSwagger()
                .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseSwagger();
            }

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                   endpoints.MapControllers());
        }
    }
    #pragma warning restore CS1591
}
