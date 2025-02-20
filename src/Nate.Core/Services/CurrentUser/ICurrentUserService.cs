namespace Nate.Core.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string UserName { get; }
        string UserEmail { get; }
    }
}
