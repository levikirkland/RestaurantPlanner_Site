using RestaurantPlanner_Site.Interfaces;
using RestaurantPlanner_Site.Models;
using RestSharp;

namespace RestaurantPlanner_Site.Services
{
    public class AccountService
    {

        private readonly ISignUpConfig _signUpConfig;
 
        public AccountService(ISignUpConfig signUpConfig)
        {
            _signUpConfig = signUpConfig;            
        }

        public async Task<bool> CreateAccountAsync(AccountInfo accountInfo)
        {
            string BaseAddress = _signUpConfig.BaseAddress;
            string ApiKey = _signUpConfig.ApiKey;
            var cancellationToken = new CancellationToken();

            //   InsertAccountInfo

            var client = new RestClient(BaseAddress);

            var request = new RestRequest($"/api/AccountInfo/insertaccountinfo", Method.Post).AddJsonBody(accountInfo)
                .AddHeader("accept", "application/json")
                .AddHeader("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");

            var response = await client.PostAsync<NewAccountResponse>(request, cancellationToken);

            if (response.statusCode != 200)
                return false;

            return true;
        }

        public async Task<bool> IsUniqueEmail(string EmailAddress)
        {
            var cancellationToken = new CancellationToken();

            string BaseAddress = _signUpConfig.BaseAddress;
            string ApiKey = _signUpConfig.ApiKey;

            var client = new RestClient(BaseAddress);
            
            var request = new RestRequest($"/api/AccountInfo/issuniqueemail?EmailAddress={EmailAddress}")
                .AddHeader("accept", "application/json")
                .AddHeader("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
            
            var  response = await client.GetAsync<ValueResponse>(request, cancellationToken);

            if (response.statusCode == 200)
                return response.value;

            return response.value;

        }
    }

    public class ValueResponse
    {
        public bool value { get; set; }
        public int statusCode { get; set; }
        public object contentType { get; set; }
    }

    public class NewAccount
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public int state { get; set; }
        public string zipcode { get; set; }
        public int accountType { get; set; }
    }

    public class NewAccountResponse
{
        public NewAccount value { get; set; }
        public int statusCode { get; set; }
        public object contentType { get; set; }
    }
     
}
