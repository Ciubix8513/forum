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
        Task RepUser(int id, string reason);
        Task AddPost(string contents, int parentId);
        Task DeletePost(int id);        
        Task EditPost(int id,string content);        
        Task<List<PostsGetDto>> GetPostsUser(int Id);
        Task EditBio(int id,string content);
        Task<UserDataDto> GetUser(int id);
    }
}