using System.Collections.Specialized;

namespace EnvironmentalConfiguration
{
    public interface IAppSettingsReader
    {
        NameValueCollection GetAppSettings(string environment=null);
        string GetEnvironment();
    }
}