namespace Nate.Core.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId => "admin";

        public string UserName => "admin";

        public string UserEmail => "admin@unknow.com";
    }
}
