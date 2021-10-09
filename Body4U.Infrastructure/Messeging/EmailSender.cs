namespace Body4U.Infrastructure.Messeging
{
    using Body4U.Application.Common;
    using Body4U.Application.Features.Identity;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Mail;
    using System.Threading;

    using static Body4U.Application.Common.GlobalConstants.Account;
    using static Body4U.Application.Common.GlobalConstants.System;

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;
        private bool sendSuccessfuly = false;
        private bool sendCompleted = false;

        public EmailSender(IConfiguration configuration)
            => this.configuration = configuration;

        public Result SendEmailAsync(string to, string subject, string htmlContent, CancellationToken cancellationToken)
        {
            try
            {
                var host = this.configuration.GetSection("MailSettings")["Host"];
                var port = int.Parse(this.configuration.GetSection("MailSettings")["Port"]);
                var username = this.configuration.GetSection("MailSettings").GetSection("Credentials")["UserName"];
                var password = this.configuration.GetSection("MailSettings").GetSection("Credentials")["Password"];

                var client = new SmtpClient()
                {
                    Host = host,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(username, password)
                };

                var displayName = this.configuration.GetSection("MailSettings").GetSection("Credentials")["Displayname"];

                var fromEmail = new MailAddress(username, displayName);
                var toEmail = new MailAddress(to);
                var message = new MailMessage
                {
                    From = fromEmail,
                    Subject = subject,
                    Body = htmlContent,
                    IsBodyHtml = true
                };
                message.To.Add(toEmail);

                client.SendCompleted += ClientSendComplete;
                client.SendAsync(message, cancellationToken);

                while (!sendCompleted)
                {
                }

                return sendSuccessfuly 
                    ? Result.Success
                    : Result.Failure(EmailProblem);
            }
            catch (Exception ex)
            {
                Log.Error($"{nameof(EmailSender)}.{nameof(this.SendEmailAsync)}", ex);
                return Result.Failure(string.Format(Wrong, nameof(this.SendEmailAsync)));
            }
        }

        private void ClientSendComplete(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Log.Error($"{nameof(EmailSender)}.{nameof(this.ClientSendComplete)}", e.Error);
                sendCompleted = true;
                return;
            }

            sendSuccessfuly = true;
            sendCompleted = true;
        }
    }
}
