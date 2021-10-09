namespace Body4U.Application.Features.Identity.Commands.ForgotPassword
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ForgotPasswordCommand : IRequest<Result<ForgotPasswordOutputModel>>
    {
        public ForgotPasswordCommand(string email)
            => this.Email = email;

        public string Email { get; }

        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<ForgotPasswordOutputModel>>
        {
            private readonly IIdentityService identityService;

            public ForgotPasswordCommandHandler(IIdentityService identityService)
                => this.identityService = identityService;

            public async Task<Result<ForgotPasswordOutputModel>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
                => await this.identityService.ForgotPassword(request);
        }
    }
}
