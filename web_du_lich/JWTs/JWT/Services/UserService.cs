using BCrypt.Net;
using JWT.Data;
using JWT.Entity;
using JWT.Model;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace JWT.Services
{
    public interface IUserService
    {
        string login(Users users);
        ExcutionResultAuth GetByUser(string user);
        ExcutionResultAuth ChangPassword(UserChangPasswordModel model,string userId);
        ExcutionResultAuth ForgotPassword();
        ExcutionResultAuth Insert(Users users);
        Task SendMail(MailContent mailContent);
        Task SendMailAsyn(string mailTo, string subject, string body);
    }
    public class UserService : IUserService
    {
        private static IUserDataProvider provider
        {
            get
            {
                if (_provider == null)
                    _provider = ObjectFactory.getInstance<IUserDataProvider>();
                return _provider;
            }
        }
        private readonly MailSettings _mailSetting;
        private static IUserDataProvider _provider;
        private Ijwt _ijwt;
        // IOptions use retrieve data
        public UserService(Ijwt ijwt,IOptions<MailSettings> mailSettings)
        {
            _ijwt = ijwt;
            _mailSetting = mailSettings.Value;
        }
        public string login(Users users)
        {
            List<Users> list = new List<Users>();
            list = provider.getUser(users.UserName);
      
            if (list == null)
                return null;
            foreach(var u in list)
            {
                var checkHashPass = BCrypt.Net.BCrypt.Verify(users.Password, u.Password);
                if (checkHashPass)
                {
                    var token = _ijwt.createToken(new UserTokenRequest()
                    {
                        UserName = u.UserName
                    });
                    return token;
                }
            }

            //if (!list.Any(x => x.userName == user.userName && x.passWord == user.passWord))
            //    return null;
            //foreach(var u in list)
            //{
            //    if(u.passWord == user.passWord)
            //    {
            //        var token = _ijwt.createToken(new UserTokenRequest() 
            //        {
            //            UserName = u.userName,
            //            EmployeeCode = u.EmployeeCode
            //        });
            //        return token;
            //    }
            //}
            return null;

        }

        public ExcutionResultAuth ChangPassword(UserChangPasswordModel model,string userId)
        {
            ExcutionResultAuth result = new ExcutionResultAuth();
            try
            {
                if(model != null && model.NewPassword == model.ConfirmPassword)
                {
                    //create salt
                    string salt = BCrypt.Net.BCrypt.GenerateSalt(SaltRevision.Revision2A);
                    //create hash
                    var hashPass = BCrypt.Net.BCrypt.HashPassword(model.NewPassword,salt);
                    var user = provider.getEmpCode(userId);
                    if(user != null)
                    {
                        foreach(var u in user)
                        {
                            var passwordVerify = BCrypt.Net.BCrypt.Verify(model.OldPassword,u.passWord);
                            if (passwordVerify)
                            {
                                u.passWord = hashPass;
                                provider.UpdatePassword(u);
                            }
                            else
                            {
                                throw new Exception("Mật khẩu cũ không đúng. Bạn hãy kiểm tra lại!");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Thông tin đăng nhập không hợp lệ!");
                    }
                   
                }
                else
                {
                    result.Message = "Xac nhan mat khau chua chinh xac";
                }
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message; 
            }
            return result;
        }

        public ExcutionResultAuth ForgotPassword()
        {
            ExcutionResultAuth result = new ExcutionResultAuth();
            string subject = "test send mail";
            //string body = "This is test send mail";
            //
            string body = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border - collapse:separate; width: 100 %; padding: 0; background - color:#ffffff\">" + Environment.NewLine;
            body += "<tbody>" + Environment.NewLine;
            body += "<tr>" + Environment.NewLine;
            body += "	<td style = \"font-family:sans-serif;font-size:12px;vertical-align:top\"> &nbsp;</td>" + Environment.NewLine;

            body += "		 <td style = \"font-family:sans-serif;font-size:12px;vertical-align:top;display:block;margin:0 auto;max-width:800px;padding:0;width:800px\">" + Environment.NewLine;

            body += "			  <div style = \"box-sizing:border-box;display:block;Margin:0;max-width:800px;padding:0\">" + Environment.NewLine;

            body += "		 <div bgcolor = \"#074B80\" style = \"font-size:20px;padding:20px 40px;color:#ffffff;border-bottom:5px solid #fe9703; text-align: center; background: rgb(7, 75, 128);\"> DCV - DATA COMMUNICATION OF VIET NAM </div>" + Environment.NewLine;

            body += "			 <table style = \"border-collapse:separate;width:100%;background:#ffffff;border-radius:3px\">" + Environment.NewLine;

            body += "			  <tbody><tr>" + Environment.NewLine;

            body += "			 <td style = \"font-family:sans-serif;font-size:12px;vertical-align:top;box-sizing:border-box;padding:5%;padding-bottom:0\">" + Environment.NewLine;

            body += "			 <table border = \"0\" cellpadding = \"0\" cellspacing = \"0\" style = \"border-collapse:separate;width:100%\">" + Environment.NewLine;

            body += "			 <tbody><tr>" + Environment.NewLine;

            body += "			 <td style = \"font-family:sans-serif;font-size:12px;vertical-align:top\">" + Environment.NewLine;

            body += "			 <h1 style = \"font-weight:bold;font-size:18px;line-height:24px;color:#2e6cf6;margin-bottom:24px;margin-top:-15px\"> Xin chào </h1>" + Environment.NewLine;

            body += "			  <div style = \"background:rgba(233,234,247,0.5);margin-bottom:0px;text-align:center;padding-top:24px\">" + Environment.NewLine;

            body += "			 <p style = \"font-weight:500;margin-top:0;margin-bottom:0px;font-size:14px;line-height:20px;color:#121e28\">" + Environment.NewLine;

            body += "			 <b> <span> Bạn đã yêu cầu lấy lại mật khẩu! </span> </b>" + Environment.NewLine;

            body += "			 <br>" + Environment.NewLine;

            body += "		 <br>" + Environment.NewLine;

            body += "			 <a style = \"font-size:14px;line-height:24px;padding:8px 20px;font-weight:bold;text-decoration:none;margin-bottom:0;display:inline-block;background:#2e6cf6;color:#fff;margin-bottom:17px\" href = \"#\" target = \"_blank\"> LẤY LẠI MẬT KHẨU </a>" + Environment.NewLine;

            body += "			 </div>" + Environment.NewLine;

            body += "		 </td>" + Environment.NewLine;

            body += "	  </tr>" + Environment.NewLine;

            body += "		 </tbody></table>" + Environment.NewLine;

            body += "		 </td>" + Environment.NewLine;

            body += "		 </tr>" + Environment.NewLine;

            body += "				 <tr>" + Environment.NewLine;

            body += "			 <td style = \"font-family:sans-serif;font-size:12px;vertical-align:top;background:#fafbfb;padding:3% 5%\">" + Environment.NewLine;

            body += "		 <p style = \"font-weight:600;font-size:13px;line-height:18px;margin-top:0;margin-bottom:4px\"> KHÔNG CHIA SẺ</p>" + Environment.NewLine;

            body += "			 <p style = \"color:#71787e;font-weight:normal;font-size:12px;line-height:18px;margin-top:0;margin-bottom:10px; text-align: justify;\"> Email này chứa một mã bảo mật của DCV.eContract, vui lòng không chia sẻ email hoặc mã bảo mật này với người khác.</p>" + Environment.NewLine;

            body += "			 <p style = \"font-weight:600;font-size:13px;line-height:18px;margin-top:0;margin-bottom:4px\"> GIỚI THIỆU VỀ DCV.eContract </p>" + Environment.NewLine;

            body += "			 <p style = \"color:#71787e;font-weight:normal;font-size:12px;line-height:18px;margin-top:0;margin-bottom:10px; text-align: justify;\"> DCV.eContract giúp bạn ký tài liệu điện tử trong vài phút. Sản phẩm an toàn, bảo mật, đảm bảo tính pháp lý.Cho dù bạn đang ở văn phòng, ở nhà, đang đi trên đường hay bất kỳ đất nước nào trên thế giới, DCV.eContract sẽ cung cấp giải pháp và dịch vụ đến với bạn.</p>" + Environment.NewLine;

            body += "		 <p style = \"font-weight:600;font-size:13px;line-height:18px;margin-top:0;margin-bottom:4px\"> CÂU HỎI VỀ TÀI LIỆU</p>" + Environment.NewLine;

            body += "		 <p style = \"color:#71787e;font-weight:normal;font-size:12px;line-height:18px;margin-top:0;margin-bottom:0; text-align: justify;\"> Nếu bạn cần sửa đổi hoặc có câu hỏi về nội dung trong tài liệu, vui lòng liên hệ với người gửi bằng cách gửi email trực tiếp cho họ. Nếu bạn gặp sự cố khi ký tài liệu, vui lòng truy cập trang Trợ giúp.</p>" + Environment.NewLine;

            body += "		 </td>" + Environment.NewLine;

            body += "		 </tr>" + Environment.NewLine;

            body += "		 </tbody></table>" + Environment.NewLine;

            body += "		 </div>" + Environment.NewLine;

            body += "			 </td>" + Environment.NewLine;

            body += "			 <td style = \"font-family:sans-serif;font-size:12px;vertical-align:top\"> &nbsp;</td>" + Environment.NewLine;

            body += "			  </tr>" + Environment.NewLine;

            body += "	 </tbody>" + Environment.NewLine;
            body += "   </table> ";
            //
            string mailTo = "anh1202199916@gmail.com";
            EmailSystem.SendMail(mailTo, subject, body);
            return result;
        }

        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail);
            email.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();
            builder.TextBody = @"Hey Alice,

                                        What are you up to this weekend? Monica is throwing one of her parties on
                                        Saturday and I was hoping you could make it.

                                        Will you be my +1?

                                        -- Joey
                                        ";
            // image from local
            var image = builder.LinkedResources.Add(@"C:\Users\Admin\Pictures\Saved Pictures\Capture.PNG");
            image.ContentId = MimeUtils.GenerateMessageId();
            builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
                                        <p>What are you up to this weekend? Monica is throwing one of her parties on
                                        Saturday and I was hoping you could make it.<br>
                                        <p>Will you be my +1?<br>
                                        <p>-- Joey<br>
                                        <img src=""https://otaku2d.com/wp-content/uploads/2020/11/haki-zoro-4.jpg"" />
                                        <center><img src=""cid:{0}""></center>", image.ContentId);
            //Đính ke  
            builder.Attachments.Add(@"C:\Users\Admin\Desktop\DOC\password.txt");
            email.Body = builder.ToMessageBody();

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
            await SendMail(new MailContent(){
                To = mailTo,
                Subject = subject,
                Body = body
            });
        }

        public ExcutionResultAuth Insert(Users users)
        {
            ExcutionResultAuth rowaffected = new ExcutionResultAuth();
            try
            {
                var now = DateTime.Now;
                var param = provider.GetById(users);
                if(param.Count == 0)
                {
                    users.CreatedAt = now;
                    string salt = BCrypt.Net.BCrypt.GenerateSalt(SaltRevision.Revision2A);
                    var hashPass = BCrypt.Net.BCrypt.HashPassword(users.Password, salt);
                    users.Password = hashPass;
                    rowaffected = provider.Insert(users);
                }
                if(rowaffected.ErrorCode == 1)
                {
                    rowaffected.Message = "Insert failed";
                }
            }
            catch (Exception)
            {
                rowaffected.ErrorCode = 2;

            }
            return rowaffected;
        }

        public ExcutionResultAuth GetByUser(string user)
        {
            ExcutionResultAuth result = new ExcutionResultAuth();
            try
            {
                var param = provider.getUser(user);
                result.Data = param;
            }
            catch (Exception e)
            {
                result.ErrorCode = 1;
                result.Message = e.Message;
            }
            return result;
        }
    }
}
