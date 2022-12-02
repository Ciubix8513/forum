using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Bwasm.Cookies.Models;

namespace Bwasm.Cookies.Logic
{
    public class ApiLogic : IApiLogic
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiLogic(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
    }
}