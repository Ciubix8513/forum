using Bwasm.Cookies.Models;

namespace Bwasm.Cookies.Logic
{
    public interface IApiLogic
    {
        Task<string> LoginAsync(LoginModel login);
        Task<(string Message,UserProfileModel? userProfile)> UserProfileAsync();
    }
}