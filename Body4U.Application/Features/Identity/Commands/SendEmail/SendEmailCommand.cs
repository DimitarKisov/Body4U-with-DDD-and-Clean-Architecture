namespace Body4U.Application.Features.Identity.Commands.SendEmail
{
    using Body4U.Application.Common;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class SendEmailCommand : IRequest<Result>
    {
        public SendEmailCommand(
            string email,
            string confirmationLink,
            string subject,
            string htmlContent)
        {
            this.Email = email;
            this.ConfirmationLink = confirmationLink;
            this.Subject = subject;
            this.HtmlContent = htmlContent;
        }

        public string Email { get; }

        public string ConfirmationLink { get; }

        public string Subject { get; }

        public string HtmlContent { get; }

        public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result>
        {
            private readonly IEmailSender emailSender;

            public SendEmailCommandHandler(IEmailSender emailSender)
                => this.emailSender = emailSender;

            public async Task<Result> Handle(SendEmailCommand request, CancellationToken cancellationToken)
                => await Task.Run(() => this.emailSender.SendEmailAsync(request.Email, request.Subject, request.HtmlContent, cancellationToken));
        }
    }
}
