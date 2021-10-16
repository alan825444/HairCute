using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Haircute.Models;

namespace Haircute.Models
{
    public class registFunction
    {
        public async Task SendEmail()
        {
            var apiKey = cDiction.SENDGRID_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("alan825444@gmail.com", "Example User");
            var to = new EmailAddress("alan825444@gmail.com", "Example User");
            var subject = "Sending with Twilio SendGrid is Fun";
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}