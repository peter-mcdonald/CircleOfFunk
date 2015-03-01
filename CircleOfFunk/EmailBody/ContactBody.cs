using System;
using System.Text;
using CircleOfFunk.Models;

namespace CircleOfFunk.EmailBody
{
    public class ContactBody : IEmailBody
    {
        readonly Contact contact;

        public ContactBody(Contact contact)
        {
            this.contact = contact;
        }

        public string BuildMessageBody()
        {
            var body = new StringBuilder();
            body.AppendLine("Contact Details : ");
            body.AppendLine("From : " + contact.Name);
            body.AppendLine("Email : " + contact.Email);
            body.AppendLine("Phone : " + contact.Phone);
            body.AppendLine("Message : " + Environment.NewLine);
            body.Append(contact.Message);
            return body.ToString();
        }
    }
}