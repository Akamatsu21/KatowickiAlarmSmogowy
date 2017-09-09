using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
            string data = DateTime.Now.ToString("dd.MM.yyyy");
            int period = DateTime.Now.Hour;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("SmogStatus@Katowice.pl");

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Hixy\Documents\hackathon24\Hackathon24\Mails.txt");

            foreach (var line in lines)
                mail.To.Add(line);

            data += period > 10 ? " Wieczorny" : " Poranny";
            mail.Subject = data + " stan powietrza";

            StringBuilder body = new StringBuilder(@"Szanowni Państwo,"); body.AppendLine().AppendLine();
            body.Append(@"Przesyłam aktualne dane z aplikacji Katowicki Alarm Smogowy"); body.AppendLine().AppendLine();
            body.Append(@"Długi komentarz"); body.AppendLine().AppendLine();
            body.Append(@"Na podstawie stanu http://smog.imgw.pl/content/dust"); body.AppendLine().AppendLine();
            body.Append(@"link do aktualności http://www.polskialarmsmogowy.pl/katowicki-alarm-smogowy,aktualnosci.html"); body.AppendLine(); body.AppendLine();

            body.Append(@"Z poważaniem,"); body.AppendLine();
            body.Append(@"Patryk Białas"); body.AppendLine();
            body.Append(@"Katowicki Alarm Smogowy"); body.AppendLine();
            body.Append(@"kom. 606 739 037"); body.AppendLine();
            body.Append(@"e-mail: patrykbialas@gmail.com"); body.AppendLine();
            body.Append(@"https://www.facebook.com/oczystepowietrze/ "); body.AppendLine();


            mail.Body = body.ToString();


            //Attachment img = new Attachment(@"images\\smog.png");
            //mail.Attachments.Add(img);
            SmtpServer.Port = 587;

            SmtpServer.Credentials = new System.Net.NetworkCredential("patrykbialas", "patram17");

            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            SmtpServer.Send(mail);
        }

        private static DataTable makeDataTableFromSheetName(string filename, string sheetName)
        {
            System.Data.OleDb.OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(
            "Provider=Microsoft.ACE.OLEDB.12.0; " +
            "data source='" + filename + "';" +
            "Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\" ");

            DataTable dtImport = new DataTable();
            System.Data.OleDb.OleDbDataAdapter myImportCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [" + sheetName + "$]", myConnection);
            myImportCommand.Fill(dtImport);
            return dtImport;
        }



    }
}
