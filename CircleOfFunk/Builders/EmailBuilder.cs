using System.Net;
using System.Net.Mail;
using CircleOfFunk.EmailBody;

namespace CircleOfFunk.Builders
{
    public class EmailBuilder
    {
        private readonly string subject;
        private readonly IEmailBody messageBody;

        public EmailBuilder(string subject, IEmailBody messageBody)
        {
            this.subject = subject;
            this.messageBody = messageBody;
        }

        public void Send()
        {
            var client = GetClient();

            var fromAddress = new MailAddress("circleoffunk.web@gmail.com");
            var toAddress = new MailAddress("info@circleoffunk.com");

            using (var mailMessage = new MailMessage(fromAddress, toAddress))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = messageBody.BuildMessageBody();
                client.Send(mailMessage);
            }
        }

        static SmtpClient GetClient()
        {
            return new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("circleoffunk.web@gmail.com", "3d68K765s3SJ>Xa")
                };
        }
    }
}