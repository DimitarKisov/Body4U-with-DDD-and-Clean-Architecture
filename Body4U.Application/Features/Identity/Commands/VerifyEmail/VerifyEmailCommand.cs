namespace Body4U.Application.Features.Identity.Commands.VerifyEmail
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class VerifyEmailCommand : IRequest<Result>
    {
        public string UserId { get; set; } = default!;

        public string Token { get; set; } = default!;

        public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result>
        {
            private readonly IIdentityService identityService;

            public VerifyEmailCommandHandler(IIdentityService identityService)
                => this.identityService = identityService;

            public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
                => await this.identityService.VerifyEmail(request);
        }
    }
}
