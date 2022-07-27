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
        private readonly string username;
        private readonly SmtpClient smtpClient;
        public EmailService(string smtpServer,
            int smtpPort,
            bool enableSsl,
            string username,
            string password)
        {
            this.username = username;
            smtpClient = new SmtpClient();
            smtpClient.EnableSsl = enableSsl;//由于使用了授权码必须设置该属性为true
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.UseDefaultCredentials = false; //不和请求一块发送
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);//用户名和密码

        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipient">收件人，多个用,隔开</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        public void SendEmail(string recipient, string subject, string body)
        {
            // 发送人和收件人
            MailMessage mailMessage = new(username, recipient);
            //主题
            mailMessage.Subject = subject;
            //内容
            mailMessage.Body = body;
            //正文编码
            mailMessage.BodyEncoding = Encoding.UTF8;
            //设置为HTML格式
            mailMessage.IsBodyHtml = true;
            //优先级
            mailMessage.Priority = MailPriority.Low;
            // 发送邮件
            smtpClient.Send(mailMessage);
        }
    }
}
