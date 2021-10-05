namespace Body4U.Application.Features.Identity.Commands.ChangePassword
{
    using Body4U.Application.Common;
    using Body4U.Application.Contracts;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ChangePasswordCommand : IRequest<Result>
    {
        public ChangePasswordCommand(
            string oldPassword,
            string newPassword,
            string confirmNewPassword)
        {
            this.OldPassword = oldPassword;
            this.NewPassword = newPassword;
            this.ConfirmNewPassword = confirmNewPassword;
        }

        public string OldPassword { get; }

        public string NewPassword { get; }

        public string ConfirmNewPassword { get; }

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
        {
            private readonly IIdentityService identityService;
            private readonly ICurrentUserService currentUserService;

            public ChangePasswordCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
            {
                this.identityService = identityService;
                this.currentUserService = currentUserService;
            }

            public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
                => await this.identityService.ChangePassword(request, currentUserService.UserId);
        }
    }
}
