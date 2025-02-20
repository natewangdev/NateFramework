using Nate.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Nate.Validation.Sample
{
    public class UserDto
    {
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "用户名")]
        public string Username { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [EmailAddress(ErrorMessage = "{0}格式不正确")]
        public string Email { get; set; }

        [ChineseOnly(ErrorMessage = "请输入中文名称")]
        [Display(Name = "中文名")]
        public string ChineseName { get; set; }

        [Display(Name = "数量")]
        [MinValue(0)]
        public decimal Amount { get; set; }

        [RequiredIf("IsVip", true, ErrorMessage = "VIP用户{0}必须填写")]
        [Display(Name = "地址")]
        public string? Address { get; set; }

        public bool IsVip { get; set; }

        [Display(Name = "手机号码")]
        [Phone(ErrorMessage = "{0}格式不正确")]
        public string PhoneNumber { get; set; }
        [Range(0, 100)]
        public int Age { get; set; }
    }
}
