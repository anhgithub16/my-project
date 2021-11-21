using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using services.svc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace services.svc.Services
{
    public interface ISendMailServices
    {
        Task SendMail(MailContent mailContent);
        Task SendMailAsyn(string mailTo, string subject, string body);
    }
    public class SendMailServices : ISendMailServices
    {

        private readonly MailSetting _mailSetting;
        public SendMailServices(IOptions<MailSetting> mailSettings)
        {
            _mailSetting = mailSettings.Value;
        }

        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail);
            email.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();
            //builder.TextBody = @"Hey Alice,

            //                            What are you up to this weekend? Monica is throwing one of her parties on
            //                            Saturday and I was hoping you could make it.

            //                            Will you be my +1?

            //                            -- Joey
            //                            ";
            //// image from local
            //var image = builder.LinkedResources.Add(@"C:\Users\Admin\Pictures\Saved Pictures\Capture.PNG");
            //image.ContentId = MimeUtils.GenerateMessageId();
            //builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
            //                            <p>What are you up to this weekend? Monica is throwing one of her parties on
            //                            Saturday and I was hoping you could make it.<br>
            //                            <p>Will you be my +1?<br>
            //                            <p>-- Joey<br>
            //                            <img src=""https://otaku2d.com/wp-content/uploads/2020/11/haki-zoro-4.jpg"" />
            //                            <center><img src=""cid:{0}""></center>", image.ContentId);
            ////Đính ke  
            //builder.Attachments.Add(@"C:\Users\Admin\Desktop\DOC\password.txt");

            //email.Body = builder.ToMessageBody();

            //smtp
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                // SecureSocketOptions cung cấp 1 cách xác định SSL và/ hoặc TLS
                smtp.Connect(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSetting.Mail, _mailSetting.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception)
            {

                throw;
            }
            smtp.Disconnect(true);
        }

        public async Task SendMailAsyn(string mailTo, string subject, string body)
        {
            await SendMail(new MailContent()
            {
                To = mailTo,
                Subject = subject,
                Body = body
            });
        }
    }
}
