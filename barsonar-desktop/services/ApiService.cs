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
    }
}
