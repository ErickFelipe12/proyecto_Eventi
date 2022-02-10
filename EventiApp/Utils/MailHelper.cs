using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace EventiApp.Utils
{
    public class MailHelper
    {
        public static async Task SendMail(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(to));
                message.From = new MailAddress(WebConfigurationManager.AppSettings["SmtpUser"]);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = WebConfigurationManager.AppSettings["SmtpUser"],
                        Password = WebConfigurationManager.AppSettings["SmtpPassWord"]
                    };

                    smtp.Credentials = credential;
                    smtp.Host = WebConfigurationManager.AppSettings["SmtpName"];
                    smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SmtpPort"]);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {

                throw;
            }         
        }

        public static async Task SendMail(List<string> mails, string subject, string body)
        {
            var message = new MailMessage();

            foreach (var to in mails)
            {
                message.To.Add(new MailAddress(to));
            }

            message.From = new MailAddress(WebConfigurationManager.AppSettings["SmtpUser"]);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = WebConfigurationManager.AppSettings["SmtpUser"],
                    Password = WebConfigurationManager.AppSettings["SmtpPassWord"]
                };

                smtp.Credentials = credential;
                smtp.Host = WebConfigurationManager.AppSettings["SmtpName"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SmtpPort"]);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

    }
}