using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace services.svc.Extension
{
    public class EmailSystem
    {
        private static string _emailFrom = "hoangtuananh2017602831@gmail.com";
        private static string _password = "jgmpiczjwbukdrtc";
        // tên máy chủ smtp của gmail
        private static string _host = "smtp.gmail.com";
        private static int _port = 587;

        public static void SendMail(string mailTo, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.To.Add(new MailAddress(mailTo));
            mailMessage.From = new MailAddress(_emailFrom);
            mailMessage.BodyEncoding = UTF8Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            SendEmailInBackgroundThread(mailMessage);
        }
        public static void SendEmailInBackgroundThread(MailMessage mailMessage)
        {
            Thread bgThread = new Thread(new ParameterizedThreadStart(Send));
            bgThread.IsBackground = true;
            bgThread.Start(mailMessage);
        }
        public static void Send(Object mailMessage)
        {
            MailMessage ms = (MailMessage)mailMessage;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = _host;
            smtp.Port = _port;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(ms.From.ToString(), _password);
            smtp.Send(ms);
        }
    }
}
