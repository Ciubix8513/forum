using Bwasm.Cookies.Models;
using Api.Dtos;

namespace Bwasm.Cookies.Logic
{
    public interface IApiLogic
    {
        Task<string> LoginAsync(LoginModel login);
        Task<string> LogoutAsync();
        Task<(string Message,UserProfileModel? userProfile)> UserProfileAsync();
        Task<List<PostsGetDto>> GetPosts(int parent);
        Task<PostsGetDto> GetPost(int Id);
        Task<string> AddForm(FormAddDto dto); 
        Task RepPost(int id, string reason);
        
    }
}