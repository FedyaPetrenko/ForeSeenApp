using System.Net;
using System.Net.Mail;
using System.Text;

namespace ForeSeen.BusinessLayer.Implementations
{
    public class EmailService
    {
        public bool Send(string smtpUserName, string smtpPassword, string Host,
            int Port, string ToEmail, string _Subject, string _Body)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = Host;
                    smtpClient.Port = Port;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(smtpUserName),
                        Subject = _Subject,
                        Body = _Body,
                        Priority = MailPriority.Normal
                    };
                    msg.To.Add(ToEmail);
                    smtpClient.Send(msg);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
