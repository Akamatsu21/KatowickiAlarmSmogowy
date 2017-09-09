using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon24
{
    public class SendEmail
    {
        public static void SendMail()
        {
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("zaket123@gmail.com");
                mail.To.Add("woziemniak@gmail.com");
                mail.Subject = "Smog";
                mail.Body = "Dusisz sie uwaga";
                SmtpServer.Port = 587;

                SmtpServer.Credentials = new System.Net.NetworkCredential("zaket123", "adminPL123");

                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                SmtpServer.Send(mail);
            }
        }
    }
}
