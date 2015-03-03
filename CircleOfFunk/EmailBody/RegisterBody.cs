using System;
using System.Text;
using CircleOfFunk.Models;

namespace CircleOfFunk.EmailBody
{
    public class RegisterBody : IEmailBody
    {
        private readonly Register register;

        public RegisterBody(Register register)
        {
            this.register = register;
        }

        public string BuildMessageBody()
        {
            var body = new StringBuilder();
            body.AppendLine("Add the following address to the subscription emails please");
            body.AppendLine(register.Email);
            body.Append(Environment.NewLine);
            body.Append("Thanks. The website");

            return body.ToString();
        }
    }
}