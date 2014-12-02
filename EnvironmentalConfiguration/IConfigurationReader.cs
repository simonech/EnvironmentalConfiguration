using System.Collections.Specialized;

namespace EnvironmentalConfiguration
{
    public interface IConfigurationReader
    {
        NameValueCollection AppSettings { get; }
    }

    public class MemoryBasedConfigurationReader : IConfigurationReader
    {
        public MemoryBasedConfigurationReader(NameValueCollection appSettings)
        {
            AppSettings = appSettings;
        }

        public NameValueCollection AppSettings { get; private set; }
    }
}