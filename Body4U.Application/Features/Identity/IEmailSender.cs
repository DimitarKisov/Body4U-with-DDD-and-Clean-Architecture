namespace Body4U.Application.Features.Identity
{
    using Body4U.Application.Common;
    using System.Threading;

    public interface IEmailSender
    {
        Result SendEmailAsync(string to, string subject, string htmlContent, CancellationToken cancellationToken);
    }
}
