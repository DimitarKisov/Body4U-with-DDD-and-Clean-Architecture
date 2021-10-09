namespace Body4U.Application.Features.Identity.Commands.ResetPassword
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ResetPasswordCommand : EntityTokenCommand<string>, IRequest<Result>
    {
        public ResetPasswordCommand(string newPassword, string confirmNewPassword)
        {
            this.NewPassword = newPassword;
            this.ConfirmNewPassword = confirmNewPassword;
        }

        public string NewPassword { get; }

        public string ConfirmNewPassword { get; }

        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
        {
            private readonly IIdentityService identityService;

            public ResetPasswordCommandHandler(IIdentityService identityService)
                => this.identityService = identityService;

            public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
                => await this.identityService.ResetPassword(request.Id, request.Token, request);
        }
    }
}
