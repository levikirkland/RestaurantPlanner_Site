using Newtonsoft.Json;
using RestaurantPlanner_Site.Models;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace RestaurantPlanner_Site.Services
{
    public class AccountService
    {
        private static HttpClient _httpClient = new HttpClient();

        public async Task<bool> CreateAccountAsync(AccountInfo accountInfo)
        {
            var serialized = JsonConvert.SerializeObject(accountInfo);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7277/AccountInfo/InsertAccountInfo") { Version = new Version(2, 0) };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serialized);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var createdMovie = JsonConvert.DeserializeObject<AccountInfo>(content);
                return true;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
