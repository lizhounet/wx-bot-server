using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masuit.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace HZY.Infrastructure.Email
{
    /// <summary>
    /// 邮件工具类
    /// </summary>
    public static class EmailUtils
    {
        /// <summary>
        /// 注册 邮箱服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="emailServerConfig">邮件服务配置信息</param>
        public static void AddEmailService(this IServiceCollection services, EmailServerConfig emailServerConfig)
        {
            services.AddSingleton(new EmailService(emailServerConfig.SmtpServer,
                emailServerConfig.SmtpPort,
                emailServerConfig.EnableSsl,
                emailServerConfig.UserName,
                emailServerConfig.PassWord));
        }
      
    }
}
