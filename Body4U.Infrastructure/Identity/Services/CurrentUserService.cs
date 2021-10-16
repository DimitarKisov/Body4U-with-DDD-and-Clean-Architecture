namespace Body4U.Infrastructure.Identity.Services
{
    using Body4U.Application.Contracts;
    using Body4U.Infrastructure.Identity.Common;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }

            this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            var trainerId = user.FindFirstValue(CustomClaimTypes.TrainerId);
            if (trainerId != null)
            {
                this.TrainerId = int.Parse(trainerId);
            }
            else
            {
                this.TrainerId = default;
            }
        }

        public string UserId { get; }

        public int? TrainerId { get; }
    }
}
