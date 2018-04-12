using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Utilities.Helpers
{
    public class Mail
    {
        public static string SendMail(string strMailID_To, string strBody, string strSubject, string strUserName, string strPwd)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(strUserName);
                // mail.To.Add(strMailID_To);
                mail.To.Add(strMailID_To);
                mail.Subject = strSubject;
                mail.Body = strBody;
                mail.Body.ToLowerInvariant();
                mail.IsBodyHtml = true;

                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(strUserName, strPwd);
                    smtp.Timeout = 20000;
                }
                smtp.Send(mail);
                return "OK";
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
