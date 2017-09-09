using Hackathon24.Message;
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
        public void SendMail(string longMessage, string p25, string p10, string so2)
        {
            string data = DateTime.Now.ToString("dd.MM.yyyy");
            int period = DateTime.Now.Hour;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("SmogStatus@Katowice.pl");

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Hixy\Documents\hackathon24\Hackathon24\Mails.txt");

            foreach (var line in lines)
                mail.To.Add(line);

            data += period > 10 ? "KATOWICKI ALARM SMOGOWY WIECZORNY" : " KATOWICKI ALARM SMOGOWY PORANNY";
            mail.Subject = data;

            StringBuilder body = new StringBuilder(@"Szanowni Państwo,"); body.AppendLine().AppendLine();
            body.Append(@"Przesyłam aktualne dane dotyczące przekroczenia norm zanieczyszczenia powietrza:"); body.AppendLine().AppendLine();
            
            body.Append(@"Pył zawieszony 2,5: " + p25); body.AppendLine();
            body.Append(@"Pył zawieszony 10: " + p10); body.AppendLine();
            body.Append(@"Dwutlenek siarki: " + so2); body.AppendLine(); body.AppendLine();
            body.Append(@"Komunikat:"); body.AppendLine(); 
            body.Append(longMessage); body.AppendLine().AppendLine();
            body.Append(@"Więcej informacy dostępnych w aplikacji Katowicki Alarm Smogowy."); body.AppendLine().AppendLine();
            body.Append(@"Aktualności: www.polskialarmsmogowy.pl/katowicki-alarm-smogowy,aktualnosci.html"); body.AppendLine(); body.AppendLine();
            body.Append(@"Z poważaniem,"); body.AppendLine();
            body.Append(@"Patryk Białas"); body.AppendLine();
            body.Append(@"Katowicki Alarm Smogowy"); body.AppendLine();
            body.Append(@"kom. 606 739 037"); body.AppendLine();
            body.Append(@"e-mail: patrykbialas@gmail.com"); body.AppendLine();
            body.Append(@"www.facebook.com/oczystepowietrze/ "); body.AppendLine();

            mail.Body = body.ToString();

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
