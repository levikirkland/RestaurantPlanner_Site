using Newtonsoft.Json;
using RestaurantPlanner_Site.Models;
using RestSharp;
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
        private static RestClient _restClient = new RestClient();   

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

        public async Task<bool> CreateNewSuperAdmin(AccountInfo accountInfo)
        {
            var serialized = JsonConvert.SerializeObject(accountInfo);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7277/AccountInfo/CreateNewSuperAdminUser") { Version = new Version(2, 0) };
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

        public async Task<bool> IsUniqueEmail(string EmailAddress)
        {
            
            var request = new RestRequest($"https://localhost:7277/AccountInfo/IsUniqueEmail?EmailAddress={EmailAddress}");
            var response = await _restClient.ExecuteGetAsync(request);
            if (!response.IsSuccessful)
            {
                //Logic for handling unsuccessful response
            }

            Console.WriteLine(response.Content);

            return Convert.ToBoolean(response.Content);
        }
    }
}
