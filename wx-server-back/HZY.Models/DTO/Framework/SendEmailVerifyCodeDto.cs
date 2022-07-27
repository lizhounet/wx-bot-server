using System.ComponentModel.DataAnnotations;

namespace HZY.Models.DTO.Framework
{
    /// <summary>
    /// 发送邮箱验证码 dto
    /// </summary>
    public class SendEmailVerifyCodeDto
    {
        [Required(ErrorMessage = "邮箱不能为空!")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "ts不能为空")]
        public long Ts { get; set; }
    }
}
