using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using CircleOfFunk.EmailBody;
using CircleOfFunk.Models;

namespace CircleOfFunk.Builders
{
    public class EmailBuilder
    {
        readonly Contact contact;
        private readonly IEmailBody messageBody;

        public EmailBuilder(Contact contact, IEmailBody messageBody)
        {
            this.contact = contact;
            this.messageBody = messageBody;
        }

        public void Send()
        {
            var client = GetClient();

            var fromAddress = new MailAddress("circleoffunk.web@gmail.com");
            var toAddress = new MailAddress("info@circleoffunk.com");

            using (var mailMessage = new MailMessage(fromAddress, toAddress))
            {
                mailMessage.Subject = contact.Subject;
                mailMessage.Body = BuildMessageBody();
                client.Send(mailMessage);
            }
        }

        string BuildMessageBody()
        {

            return messageBody.BuildMessageBody();

            //var body = new StringBuilder();
            //body.AppendLine("Contact Details : ");
            //body.AppendLine("From : " + contact.Name);
            //body.AppendLine("Email : " + contact.Email);
            //body.AppendLine("Phone : " + contact.Phone);
            //body.AppendLine("Message : " + Environment.NewLine);
            //body.Append(contact.Message);
            //return body.ToString();
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