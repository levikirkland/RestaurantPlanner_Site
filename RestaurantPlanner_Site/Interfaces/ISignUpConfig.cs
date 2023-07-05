namespace RestaurantPlanner_Site.Interfaces
{
    public interface ISignUpConfig
    {
        public string BaseAddress { get;  }
        public string ApiKey { get;  }

        IConfigurationSection GetConfigurationSection(string Key);
    }
}
