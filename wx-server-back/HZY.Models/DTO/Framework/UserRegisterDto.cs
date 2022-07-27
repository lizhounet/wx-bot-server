using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Models.DTO.Framework
{
    /// <summary>
    /// 用户注册dto
    /// </summary>
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "昵称不能为空!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "邮箱不能为空!")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "密码不能为空!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "验证码不能为空!")]
        public string VerifyCode { get; set; }
    }
}
