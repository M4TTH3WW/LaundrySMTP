using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System;


namespace LaundryBL
{
    public class SMTP
    {

        public static void SendEmail(string name, string clWeight)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Matthew's Laundry Shop", "do-not-reply@laundryshop.com"));
            message.To.Add(new MailboxAddress("Client", "matthewpascua22@gmail.com"));
            message.Subject = "New Laundry Order";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Hello, {name}!</h1>" +
                       $"<p>We have successfully processed your laundry, weighing a total of <strong>{clWeight} kg</strong>.</p>" +
                       "<p>Thank you for choosing us!</p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("5c428092e1c889", "8046cdc054a257");

                    client.Send(message);
                    Console.WriteLine("Email sent successfully through Mailtrap.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}
