using Microsoft.Extensions.Options;
using StoreAndDeliver.BusinessLayer.Constants;
using StoreAndDeliver.BusinessLayer.Options;
using StoreAndDeliver.BusinessLayer.Resources;
using StoreAndDeliver.DataLayer.Models;
using System.Net;
using System.Net.Mail;
using System.Resources;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailServiceOptions _emailServiceDetails;

        public EmailService(IOptions<EmailServiceOptions> options)
        {
            _emailServiceDetails = options.Value;
        }

        public async Task SendEmail(AppUser user, string url, string language)
        {
            MailAddress addressFrom = new MailAddress(_emailServiceDetails.EmailAddress, "Store&Deliver");
            MailAddress addressTo = new MailAddress(user.Email);
            MailMessage message = new MailMessage(addressFrom, addressTo);

            ResourceManager resourceManager = GetResourceManager(language);

            message.Subject = resourceManager.GetString("EmailSubject");
            message.IsBodyHtml = true;
            string htmlString = @$"<html>
                      <body style='background-color: #f7f1d5; 
                        padding: 15px; border-radius: 15px; 
                        box-shadow: 5px 5px 15px 5px #9F9F9F;
                        font-size: 16px;'>
                      <p>{resourceManager.GetString("Hello")} {user.UserName},</p>
                      <p>{resourceManager.GetString("EmailMainContent")}</p>
                      <a href={url}>{resourceManager.GetString("ConfirmAccount")}</a>
                         <p>{resourceManager.GetString("ThankYou")},<br>-Store&Deliver</br></p>
                      </body>
                      </html>
                     ";
            message.Body = htmlString;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_emailServiceDetails.EmailAddress, _emailServiceDetails.Password),
                EnableSsl = true
            };
            await smtp.SendMailAsync(message);
        }

        private static ResourceManager GetResourceManager(string language)
        {
            return language switch
            {
                Languages.UKRAINAN => new ResourceManager("StoreAndDeliver.BusinessLayer.Resources.ua", typeof(ua).Assembly),
                Languages.ENGLISH => new ResourceManager("StoreAndDeliver.BusinessLayer.Resources.en", typeof(en).Assembly),
                Languages.RUSSIAN => new ResourceManager("StoreAndDeliver.BusinessLayer.Resources.ru", typeof(ru).Assembly),
                _ => new ResourceManager("StoreAndDeliver.BusinessLayer.Resources.en", typeof(en).Assembly),
            };
        }
    }
}
