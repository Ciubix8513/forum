using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Api.Dtos;
using Bwasm.Cookies.Models;

namespace Bwasm.Cookies.Logic
{
    public class ApiLogic : IApiLogic
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ApiLogic> _logger;
        public ApiLogic(IHttpClientFactory httpClientFactory, ILogger<ApiLogic> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<PostsGetDto>> GetPosts(int parent)
        {
            var client = _httpClientFactory.CreateClient("API");
            string payload = $"?Id={parent}";
            var response = await client.GetAsync("/Post/GetPosts" + payload);
            if (!response.IsSuccessStatusCode)
                return null;
            var list = await response.Content.ReadFromJsonAsync<List<PostsGetDto>>();
            return list;

        }

        //Make Login api call on route /Auth/login with content login
        public async Task<string> LoginAsync(LoginModel login)
        {
            //Create http client
            var client = _httpClientFactory.CreateClient("API");
            //Create payload
            string payload = JsonSerializer.Serialize(login);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            //Make the request
            var response = await client.PostAsync("/Auth/login", content);
            if (response.IsSuccessStatusCode)
                return "Success";
            return "Failed";
        }

        public async Task<string> LogoutAsync()
        {
            var client = _httpClientFactory.CreateClient("API");
            var response = await client.PostAsync("Auth/logout", null);
            if (response.IsSuccessStatusCode)
                return "Success";
            return "Failed";
        }

        public async Task<(string Message, UserProfileModel? userProfile)> UserProfileAsync()
        {
            var client = _httpClientFactory.CreateClient("API");
            var response = await client.GetAsync("Auth/user-profile");
            if (response.IsSuccessStatusCode)
                return ("Success", await response.Content.ReadFromJsonAsync<UserProfileModel>());
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return ("Unauthorized", null);
            return ("Failed", null);
        }

        public async Task<string> AddForm(FormAddDto dto)
        {
            var client = _httpClientFactory.CreateClient("API");
            string payload = JsonSerializer.Serialize(dto);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Reg/AddForm", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task RepPost(int id, string reason)
        {
            var client = _httpClientFactory.CreateClient("API");
            await client.PostAsync($"Rep/RepPost?Id={id}&Reason={reason}", null);
        }

        public async Task RepUser(int id, string reason)
        {
            var client = _httpClientFactory.CreateClient("API");
            await client.PostAsync($"Rep/RepUser?Id={id}&Reason={reason}", null);
        }

        public async Task<PostsGetDto> GetPost(int Id) => await (await _httpClientFactory.CreateClient("API")
            .GetAsync($"Post/GetPost?id={Id}")).Content.ReadFromJsonAsync<PostsGetDto>();

        public async Task AddPost(string contents, int parentId)
        {
            var client = _httpClientFactory.CreateClient("API");
            string payload = JsonSerializer.Serialize(new PostAddDto(contents, parentId));
            await client.PostAsync("Post/AddPost", new StringContent(
                payload,
                Encoding.UTF8,
                "application/json"));
        }

        public async Task DeletePost(int id)
        {
            PostEditDto dto = new(id, null);
            var client = _httpClientFactory.CreateClient("API");
            string payload = JsonSerializer.Serialize(dto);
            await client.PostAsync("Post/EditPost",new StringContent(
                payload,
                Encoding.UTF8,
                "application/json"
            ));
        }

        public async Task EditPost(int id, string content)
        {
            PostEditDto dto = new(id, content);
            var client = _httpClientFactory.CreateClient("API");
            string payload = JsonSerializer.Serialize(dto);
            await client.PostAsync("Post/EditPost",new StringContent(
                payload,
                Encoding.UTF8,
                "application/json"
            ));
        }

        public async Task<List<PostsGetDto>> GetPostsUser(int Id)
        {
            var client =_httpClientFactory.CreateClient("API");
           return await client.GetFromJsonAsync<List<PostsGetDto>>($"Post/GetPostsUser?Id={Id}");
        }

        public async Task EditBio(int id, string content)
        {
            var client = _httpClientFactory.CreateClient("API");
            await client.PostAsync($"User/SetBio?Id={id}&Contents={content}",null);
        }

        public async Task<UserDataDto> GetUser(int id)
        {
            var client = _httpClientFactory.CreateClient("API");
            var response = await client.GetAsync($"User/GetUser?id={id}");
            return await response.Content.ReadFromJsonAsync<UserDataDto>();
        }

        public async Task<List<FormGetDto>> GetForms() => await (await _httpClientFactory.CreateClient("API")
            .GetAsync("Reg/GetForms")).Content.ReadFromJsonAsync<List<FormGetDto>>();

        public async Task DeleteForm(int id) => await _httpClientFactory.CreateClient("API")
            .PostAsync($"Reg/RemoveForm?Id={id}", null);

        public async Task AddUser(int id) => await _httpClientFactory.CreateClient("API")
            .PostAsync($"Reg/AddUser?Id={id}",null);

        public async Task<List<RepDto>> GetPostReports() => await (await _httpClientFactory.CreateClient("API")
            .GetAsync("Rep/Get/RepPost")).Content.ReadFromJsonAsync<List<RepDto>>();

        public async Task<List<RepDto>> GetUserReports() => await (await _httpClientFactory.CreateClient("API")
            .GetAsync("Rep/Get/RepUser")).Content.ReadFromJsonAsync<List<RepDto>>();

        public async Task DeletePostReport(int id) => await _httpClientFactory.CreateClient("API")
            .PostAsync($"Rep/Delete/RepPost?Id={id}",null);

        public async Task DeleteUserReport(int id)=> await _httpClientFactory.CreateClient("API")
            .PostAsync($"Rep/Delete/RepUser?Id={id}",null);
    }
}