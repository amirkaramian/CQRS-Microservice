using Microsoft.Extensions.Logging;
using Payscrow.Notifications.Api.Application.Enumerations;
using Payscrow.Notifications.Api.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Notifications.Api.Infrastructure.Email.Mailtrap
{
    public class MailtrapEmailNotificationService : IEmailNotificationService
    {
        private readonly ILogger _logger;

        public MailtrapEmailNotificationService(ILogger<MailtrapEmailNotificationService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetEmailContent(Guid tenantId, EmailMessageType emailMessageType, Dictionary<string, object> values)
        {
            await Task.CompletedTask;
            return "";
        }

        public async Task SendAsync(Guid tenantId, string to, string subject, string message)
        {
            try
            {
                var client = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("430a4ed99f6fe0", "c57a82fef40f0e"),
                    EnableSsl = true
                };

                await client.SendMailAsync("development@bellasavings.com", to, subject, message).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while sending mailtrap message.");
            }
        }

        public async Task SendAsync(Guid tenantId, EmailMessageType emailMessageType, string to, string subject, Dictionary<string, object> values)
        {
            try
            {
                var client = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("430a4ed99f6fe0", "c57a82fef40f0e"),
                    EnableSsl = true
                };

                var message = GetMessageBody(tenantId, emailMessageType, values);

                await client.SendMailAsync("development@payscrow.net", to, subject, message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while sending mailtrap message.");
            }
        }

        private string GetMessageBody(Guid tenantId, EmailMessageType emailMessageType, Dictionary<string, object> values)
        {
            var sb = new StringBuilder();

            //string name = values["name"]?.ToString();
            //string amount = values["amount"]?.ToString();

            switch (emailMessageType)
            {
                case EmailMessageType.PaymentInvite:
                    sb.Append("<h4>You have been invite to make a payment for:</h4>");
                    string amount = values["Amount"]?.ToString();
                    string buyerLink = values["buyerResponseLink"]?.ToString();
                    sb.Append(amount);
                    sb.AppendFormat("<a href='{0}'>Click this link to proceed.</a>", buyerLink);
                    break;

                case EmailMessageType.GuestDeal:
                    sb.Append("<h4>You will need to verify the invite you just sent on payscrow using the verification code below:</h4>");
                    string code = values["code"].ToString();
                    sb.AppendFormat("<h3>Code: {0}</h3>", code);
                    break;

                case EmailMessageType.EmailVerificationCode:
                    sb.Append("<h4>You will need to verify email on payscrow using the verification code below:</h4>");
                    string verificationCode = values["code"].ToString();
                    sb.AppendFormat("<h3>Code: {0}</h3>", verificationCode);
                    break;

                case EmailMessageType.DealCreatedAndVerified:
                    string buyerTransactionLink = values["buyerLink"].ToString();
                    //string sponsorName = values["sponsorName"].ToString();
                    sb.AppendFormat("<p>Hi, Congratulations on your new deal creation. Please share this link to your customers {0}</p>", buyerTransactionLink);
                    break;

                case EmailMessageType.MarketPlaceEscrowCode:
                    sb.Append("<h4>You will be asked by the merchant to provide this code after the transaction is completed:</h4>");
                    string escrowCode = values["code"].ToString();
                    sb.AppendFormat("<h3>Code: {0}</h3>", escrowCode);
                    break;
                //case EmailMessageType.PaymentConfirmation:
                ////var name = values["name"];
                ////string amount = values["amount"].ToString();
                ////string packageNumber = values["packageNumber"].ToString();
                ////sb.AppendFormat("Hi {0}, We have received NGN {1}, been deposit payment into your wallet", name, amount);
                //break;

                case EmailMessageType.SystemGeneratedUser:
                    sb.Append("<h4>Your brand new account with payscrow has been created with the following credentials:</h4>");
                    string password = values["password"].ToString();
                    sb.AppendFormat("<h3>Password: {0}</h3>", password);
                    break;
            }
            return sb.ToString();
        }
    }
}