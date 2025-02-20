using Nate.Core.Services.CurrentUser;

namespace Nate.Data.EntityFrameworkCore.Sample.Services
{
    public class CurrentUserSampleService : ICurrentUserService
    {
        public string UserId => "19EF26BE-7024-4634-8BBF-15FC29FE5110";

        public string UserName => "wanghj";

        public string UserEmail => "wanghj@163.com";
    }
}
