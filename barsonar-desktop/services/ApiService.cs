using barsonar_desktop.classes;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace barsonar_desktop.services
{
    class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string BASE_URL;

        public ApiService()
        {
            DotNetEnv.Env.Load();

            BASE_URL = Environment.GetEnvironmentVariable("API_URL")
                ?? throw new Exception("API_URL not set in .env");

            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(BASE_URL)
            };
        }

        //LOGIN

        public async Task<User> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("/auth/login", new { email, password });
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<JsonElement>(json);

            var user = new User
            {
                Id = res.GetProperty("userId").GetInt32(),
                Email = res.GetProperty("email").GetString() ?? "",
                Role = res.GetProperty("role").GetString() ?? "user"
            };

            var me = await _httpClient.GetAsync("/auth/me");
            me.EnsureSuccessStatusCode();
            var profile = await me.Content.ReadFromJsonAsync<User>();

            if (profile != null)
            {
                user.Username = profile.Username;
                user.Age = profile.Age;
            }

            return user;
        }

        //PHOTO

        public async Task<List<Photo>> GetAllPhotosAsync()
        {
            var response = await _httpClient.GetAsync("/photo/all");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Photo>>() ?? new List<Photo>();
        }

        public async Task ApprovePhotoAsync(int id)
        {
            var response = await _httpClient.PutAsync($"/photo/{id}/approved", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePhotoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/photo/{id}");
            response.EnsureSuccessStatusCode();
        }

        //COMMENT

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var response = await _httpClient.GetAsync("/comment/all");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Comment>>() ?? new List<Comment>();
        }

        public async Task ApproveCommentAsync(int id)
        {
            var response = await _httpClient.PutAsync($"/comment/{id}/approved", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/comment/{id}");
            response.EnsureSuccessStatusCode();
        }

        //NEWS

        public async Task<List<News>> GetAllNewsAsync()
        {
            var response = await _httpClient.GetAsync("/place/news/all");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<News>>() ?? new List<News>();
        }

        public async Task ApproveNewsAsync(int id)
        {
            var response = await _httpClient.PutAsync($"/place/{id}/approve", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteNewsAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/place/news/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
