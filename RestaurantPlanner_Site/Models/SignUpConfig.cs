using Microsoft.AspNetCore.DataProtection.KeyManagement;
using RestaurantPlanner_Site.Interfaces;

namespace RestaurantPlanner_Site.Models
{
    public class SignUpConfig : ISignUpConfig
    {
        private readonly IConfiguration _configuration;

        public SignUpConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseAddress
        {
            get
            {
              return  this._configuration["Api:BaseAddress"];
            }
        }
        
        public string ApiKey
        {
            get
            {
                return this._configuration["Api:ApiKey"];
            }
        }

        public IConfigurationSection GetConfigurationSection(string key)
{
            return this._configuration.GetSection(key);
        }
    }
}
