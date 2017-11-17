using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Threading.Tasks;


namespace LogiCon.Utilities
{
    public class EmailGenerator
    {
        //public async static Task SendEmailAsync(string email, string subject, string message)
        //{
        //    try
        //    {
        //        var _email = "tkpr256@gmail.com";
        //        var _epass = ConfigurationManager.AppSettings["Emailpassword"];
        //        var _dispname = "RMA";
        //        MailMessage mymessage = new MailMessage();
        //        mymessage.To.Add(email);
        //        mymessage.From = new MailAddress(_email);
        //        mymessage.Subject = subject;
        //        mymessage.Body = "Hello SMTP";
        //        mymessage.IsBodyHtml = true;

        //        using (SmtpClient smtp = new SmtpClient())
        //        {
        //            smtp.EnableSsl = true;
        //            smtp.Host = "smtp.office365.com";
        //            smtp.Port = 995;
        //            smtp.UseDefaultCredentials = false;
        //            smtp.Credentials = new NetworkCredential(_email, _epass);
        //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
        //            await smtp.SendMailAsync(mymessage);

        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        Configuration config;
        public EmailGenerator()
        {
            config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        }

        public bool SendMail(MailMessage msg)
        {

            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            msg.From = new MailAddress(settings.Smtp.From, "RMA");
            SmtpClient client = new SmtpClient(settings.Smtp.Network.Host);
            client.Port = settings.Smtp.Network.Port;
            client.EnableSsl = settings.Smtp.Network.EnableSsl;
            client.Timeout = 900000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = settings.Smtp.Network.DefaultCredentials;
            client.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
            client.Send(msg);

            return true;
        }

        public bool ConfigMail(string to, string cc, string bcc, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.CC.Add(new MailAddress(cc));
            msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(to));
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsCc"]))
                msg.CC.Add(new MailAddress("sg.rma@ezy-corp.com"));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);
        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body, byte[] attBytes, string attachmentName)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            Attachment att = new Attachment(new MemoryStream(attBytes), attachmentName + ".Pdf");
            msg.Attachments.Add(att);

            return SendMail(msg);
        }

        public bool ConfigMail(string to, string bcc, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));

            msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string cc, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.CC.Add(new MailAddress(cc));

            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }
    }
}