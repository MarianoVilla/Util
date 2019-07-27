using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dager
{
    public static class MailUtil
    {

        public static string SendEmail(string From, List<string> To, NetworkCredential Credentials, string MsgSubject, string MsgBody, bool Html = false, string SmtpCli = "smtp.live.com", int Port = 587, bool SSL = true)
        {
            try
            {
                GeneralUtil.CheckNullArgs(From, To, Credentials, MsgSubject, MsgBody);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress(From);
            To.ForEach(x => mail.To.Add(x));
            mail.Subject = MsgSubject;
            mail.IsBodyHtml = Html;
            mail.Body = MsgBody;
            SmtpServer.Port = Port;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = Credentials;
            SmtpServer.EnableSsl = SSL;
            try
            {
                SmtpServer.Send(mail);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Problema al enviar correo: " + ex.Message;
            }

        }
    }
}
