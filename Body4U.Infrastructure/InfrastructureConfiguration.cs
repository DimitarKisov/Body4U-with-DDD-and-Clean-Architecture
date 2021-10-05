namespace Body4U.Infrastructure
{
    using Body4U.Application.Contracts;
    using Body4U.Application.Features.Identity;
    using Body4U.Infrastructure.Identity.Models;
    using Body4U.Infrastructure.Identity.Services;
    using Body4U.Infrastructure.Persistence;
    using Body4U.Infrastructure.Persistence.Seeders;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Serilog;
    using System;
    using System.Linq;
    using System.Text;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDatabase(configuration)
                .AddRepositories()
                .AddIdentity(configuration)
                .SeedIdentityData(configuration);

        internal static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext)
                            .Assembly.FullName)));

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRepository<>)))
                    .AsMatchingInterface()
                    .WithTransientLifetime());

        internal static IServiceCollection AddIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var secret = configuration.GetSection("JwtSettings")["Secret"];

            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IJwtTokenGeneratorService, JwtTokenGeneratorService>();

            return services;
        }

        internal static IServiceCollection SeedIdentityData(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var dbContext = (ApplicationDbContext)serviceProvider.GetService(typeof(ApplicationDbContext)))
            {
                if (!(dbContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)!.Exists())
                {
                    dbContext.Database.Migrate();
                }

                if (!dbContext.Users.Any())
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            new ApplicationDbContextSeeder(configuration).SeedAsync(dbContext, serviceProvider).GetAwaiter().GetResult();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"{nameof(InfrastructureConfiguration)}.{nameof(SeedIdentityData)}", ex);
                        }
                    }
                }
            }

            return services;
        }
    }
}
