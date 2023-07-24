using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using TrackingManagment.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Asn1.Ocsp;
using TrackingManagment.Identity;
using Microsoft.AspNetCore.Identity;

namespace TrackingManagment.Repository
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmailService(IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _context= context;
            _userManager= userManager;
        }

        public void SendEmail(EmailModel email)
        {
            var To = email.Username;
            var recieverId = email.UserID;
            var senderId = email.Token;
            var Subject = "Invitation";
            
            var receiverName = _userManager.FindByIdAsync(senderId).Result;
            var userName = receiverName.UserName;
            
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            mail.To.Add(MailboxAddress.Parse(To));
            mail.Subject = Subject;
            var template = Directory.GetCurrentDirectory() + "\\Template\\editorconfig.html";

            //Replace
            StreamReader str = new StreamReader(template);
            var MailText = str.ReadToEnd();
            MailText = MailText.Replace("[senderUserId]", recieverId)
          .Replace("[date]", DateTime.UtcNow.ToString()).Replace("[time]", DateTime.Now.ToShortTimeString()).Replace("[receiverId]", senderId).Replace("[recieverUserId]", userName);
            var emailMessage = new MimeMessage();


            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            emailMessage.Body = builder.ToMessageBody();

            mail.Body = builder.ToMessageBody();            

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }


    }
}