using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using TrackingManagment.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Asn1.Ocsp;
using TrackingManagment.Identity;

namespace TrackingManagment.Repository
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public EmailService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context= context;
        }

        public void SendEmail(EmailModel email)
        {
            var userId = _context.Users.Find(email.UserID);
            var To = userId.Email;
            var Subject = "Invitation to my crud";
           
            if(userId==null) { return ; }

            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            mail.To.Add(MailboxAddress.Parse(To));
            mail.Subject = Subject;
            var template = Directory.GetCurrentDirectory() + "\\Template\\editorconfig.html";
            using StreamReader stream = new StreamReader(template);
            mail.Body = new TextPart(TextFormat.Html) { Text = stream.ReadToEnd() };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }


    }
}