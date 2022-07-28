using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Infrastructure.Email
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public class EmailService
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly bool enableSsl;
        private readonly string username;
        private readonly string password;
        public EmailService(string smtpServer,
            int smtpPort,
            bool enableSsl,
            string username,
            string password)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.enableSsl = enableSsl;
            this.username = username;
            this.password = password;


        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipient">收件人，多个用,隔开</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        public void SendEmail(string recipient, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(username, username));
            message.To.Add(new MailboxAddress(recipient, recipient));
            message.Subject = subject; //邮件标题
            var builder = new BodyBuilder
            {
                TextBody = body//正文
            };
            message.Body = builder.ToMessageBody();
            using var client = new MailKit.Net.Smtp.SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect(smtpServer, smtpPort, true);//网易、QQ支持 25(未加密)，465和587(SSL加密）
            client.Authenticate(username, password);
            client.Send(message);//发送邮件
        }
    }
}
