using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Threading.Tasks;

namespace MediaStorage.Common
{
    public class MailSender
    {
        public async Task Send(string to, string subject, string body)
        {
            MailMessage message = new MailMessage(ConfigurationManager.AppSettings["FromEmail"], to);
            
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;

            using (var smtp = new SmtpClient())
            {
                Console.WriteLine("Sending Email......");

                //smtp.Host = ConfigurationManager.AppSettings["Host"];
                //smtp.EnableSsl = true;
                //NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["Username"], ConfigurationManager.AppSettings["Password"]);
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = NetworkCred;
                //smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                //Console.WriteLine("Sending Email......");

                //await smtp.SendMailAsync(message);
            }
        }
    }
}