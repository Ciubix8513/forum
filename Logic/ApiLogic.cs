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
            var list = await JsonSerializer.DeserializeAsync<List<PostsGetDto>>(response.Content.ReadAsStream());
            _logger.Log(LogLevel.Information,list.Count.ToString());
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
            await client.PostAsync($"Rep/RepPost?Id{id}&Reason={reason}", null);
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
    }
}