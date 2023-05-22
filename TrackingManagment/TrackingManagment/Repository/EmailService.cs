using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using TrackingManagment.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Asn1.Ocsp;

namespace TrackingManagment.Repository
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration) 
        { 
                 _configuration = configuration;
        }

        public void SendEmail(Email email)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.Subject = email.Subject;
            var template = Directory.GetCurrentDirectory() + "\\Template\\Email.html";
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
