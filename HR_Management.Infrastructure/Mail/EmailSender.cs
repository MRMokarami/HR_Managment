using System.Net;
using HR_Management.Application.Contracts.Infrastructure;
using HR_Management.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR_Management.Infrastructure.Mail
{
    public class EmailSender:IEmailSender
    {
        private readonly EmailSetting _emailSetting;
        public EmailSender(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }
        public async Task<bool> SendEmailAsync(Email email)
        {
            var to = new EmailAddress(email.To);
            var from = new EmailAddress()
            {
                Email = _emailSetting.FromAddress,
                Name = _emailSetting.FromName
            };
            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var client = new SendGridClient(_emailSetting.ApiKey);
            var response = await client.SendEmailAsync(message);
            return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;

        }
    }
}
