using Newtonsoft.Json.Linq;
using ShortUrlNet.Models;
using System.IO;

namespace ShortUrlNet.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private Configuration _configuration;

        public ConfigurationService()
        {
            _configuration = JObject.Parse(File.ReadAllText("./AppData/configuration.json"))
                .ToObject<Configuration>();
        }

        public Configuration GetConfiguration()
        {
            return _configuration;
        }
    }
}
