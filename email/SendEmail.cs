using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace email
{
    public class SendEmail : IEmailSender
    {
        private readonly string _sendGridSecret;
        private readonly ILogger<SendEmail> _logger;

        public SendEmail(IConfiguration config, ILogger<SendEmail> logger)
        {
            _sendGridSecret = config.GetValue<string>("SendGrid:SecretKey");
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var client = new SendGridClient(_sendGridSecret);
                var from = new EmailAddress("connerthompson121@gmail.com", "Conner");
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted || 
                    response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Email sent to {email} with subject {subject} at {DateTime.Now}");
                }
                else
                {
                    _logger.LogError($"Failed to send email to {email}. Status code: {response.StatusCode}");
                    throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while sending email to {email}: {ex.Message}");
                throw;
            }
        }
    }
}