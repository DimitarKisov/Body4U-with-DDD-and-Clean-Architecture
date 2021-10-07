namespace Body4U.Application.Features.Identity.Commands.SendEmailConfirmation
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class SendEmailConfirmationCommand : IRequest<Result>
    {
        public SendEmailConfirmationCommand(string email, string confirmationLink)
        {
            this.Email = email;
            this.ConfirmationLink = confirmationLink;
        }

        public string Email { get; }

        public string ConfirmationLink { get; }

        public class SendEmailConfirmationCommandHandler : IRequestHandler<SendEmailConfirmationCommand, Result>
        {
            private readonly IEmailSender emailSender;

            public SendEmailConfirmationCommandHandler(IEmailSender emailSender)
                => this.emailSender = emailSender;

            public async Task<Result> Handle(SendEmailConfirmationCommand request, CancellationToken cancellationToken)
            {
                var subject = "Email Confirmation";
                var htmlContent = $"<p>За да потвърдите, моля кликлнете <a href=\"{request.ConfirmationLink}\">ТУК</a></p>";
                
                var result = Task.Run(() => this.emailSender.SendEmailAsync(request.Email, subject, htmlContent, cancellationToken));

                return await result;
            }
        }
    }
}
