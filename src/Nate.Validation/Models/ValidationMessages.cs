namespace Nate.Validation.Models
{
    public static class ValidationMessages
    {
        public const string Required = "此字段为必填项";
        public const string MaxLength = "输入长度不能超过{0}个字符";
        public const string MinLength = "输入长度不能少于{0}个字符";
        public const string Range = "输入值必须在{0}到{1}之间";
        public const string Email = "请输入有效的电子邮件地址";
        public const string Phone = "请输入有效的电话号码";
    }
}
