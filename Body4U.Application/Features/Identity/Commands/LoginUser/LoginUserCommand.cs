namespace Body4U.Application.Features.Identity.Commands.LoginUser
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginUserCommand : IRequest<Result<LoginOutputModel>>
    {
        public LoginUserCommand(string email, string password, bool rememberMe)
        {
            this.Email = email;
            this.Password = password;
            this.RememberMe = rememberMe;
        }

        public string Email { get; }

        public string Password { get; }

        public bool RememberMe { get; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentityService identityService;

            public LoginUserCommandHandler(IIdentityService identityService)
                => this.identityService = identityService;

            public async Task<Result<LoginOutputModel>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
                => await this.identityService.Login(command);
        }
    }
}
