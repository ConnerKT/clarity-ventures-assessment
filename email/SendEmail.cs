using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace email;

public class SendEmail : IEmailSender
	{
        private string SendGridSecret { get; set; }
        public SendEmail(IConfiguration _config) //can be configured differently. potentially only slightly more secure than just passing the string
        {
			SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
            var client = new SendGridClient(SendGridSecret);
			var from = new EmailAddress("connerthompson121@gmail.com", "Conner"); //should match your sender account on Sendgrid
			var to = new EmailAddress(email);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
           
			var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body);

            return;
		}
	}
