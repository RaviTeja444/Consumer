using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompany.Messaging;
using System.Net;
using System.Net.Mail;

namespace ConsumerService
{
    public class RegisterCustomerConsumer : IConsumer<IRegisterCustomer>
    {
        public Task Consume(ConsumeContext<IRegisterCustomer> context)
        {
            IRegisterCustomer newCustomer = context.Message;

            var fromAddress = new MailAddress("", "");
            var toAddress = new MailAddress("", "");
            const string fromPassword = "";
            const string subject = "Test";
            string body = "Hie this is a test message"+newCustomer.Name;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                EnableSsl = true
            };
            
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return Task.FromResult(context.Message);
        }
    }
}
