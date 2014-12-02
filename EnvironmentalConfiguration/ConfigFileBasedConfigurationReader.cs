using System.Collections.Specialized;
using System.Configuration;
using EnvironmentalConfiguration;

namespace EnvironmentalConfiguration
{
    public class ConfigFileBasedConfigurationReader : IConfigurationReader
    {
        public NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }
    }
}