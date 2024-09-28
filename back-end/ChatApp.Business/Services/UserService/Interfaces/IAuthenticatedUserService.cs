namespace ChatApp.Business.Services.UserService.Interfaces
{
    public interface IAuthenticatedUserService
    {
        int GetAuthenticatedUserId();
        string GetAuthenticatedUsername();
    }
}