using System.Net;
using System.Net.Http;

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
    }
}
