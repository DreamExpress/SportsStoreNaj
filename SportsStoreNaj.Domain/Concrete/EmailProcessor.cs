using SportsStoreNaj.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStoreNaj.Domain.Entities;

namespace SportsStoreNaj.Domain.Concrete
{
    public class EmailSettings {
        public string MailTo = "order@example.com";
        public string MailFrom = "sportsstore@example.com";
        public bool UseSsl = true;
        public string UserName = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "Smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\temp\sports_store_emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new System.Net.Mail.SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailSettings.UserName,emailSettings.Password);

                

                StringBuilder body = new StringBuilder().AppendLine("A new order has been submitted").AppendLine("---").AppendLine("Items:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("\r\n{0}*{1} => subtotal:{2:c2}", line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("\r\nTotal order value:{0:c2}", cart.ComputeValue()).AppendLine("---").AppendLine("Ship to:").AppendLine(shippingDetails.Name).AppendLine(shippingDetails.Line1).AppendLine(shippingDetails.Line2 ?? "").AppendLine(shippingDetails.Line3 ?? "").AppendLine(shippingDetails.City).AppendLine(shippingDetails.State ?? "").AppendLine(shippingDetails.Country).AppendLine(shippingDetails.Zip).AppendLine("---").AppendFormat("Gift wrap:{0}", shippingDetails.GiftWrap ? "Yes" : "No");

                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(emailSettings.MailFrom, emailSettings.MailTo, "New order submitted!", body.ToString());

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                    mailMessage.BodyEncoding = Encoding.Unicode;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
