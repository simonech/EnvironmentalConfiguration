using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using EnvironmentalConfiguration;

namespace EnvironmentalConfiguration
{
    public class AppSettingsBasedConfigurationReader : IAppSettingsReader
    {
        private const char SEPARATOR = '.';
        private const string ENVKEY = "env";

        private IConfigurationReader _configurationReader;
        public AppSettingsBasedConfigurationReader(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        internal AppSettingsBasedConfigurationReader(): this(new ConfigFileBasedConfigurationReader()) {}

        public NameValueCollection GetAppSettings(string environment=null)
        {
            NameValueCollection collection = new NameValueCollection();
            if (String.IsNullOrWhiteSpace(environment))
                return _configurationReader.AppSettings;
            foreach (var appSetting in _configurationReader.AppSettings.AllKeys)
            {
                if (!appSetting.Equals(ENVKEY))
                {
                    string value = _configurationReader.AppSettings[appSetting];
                    String[] settingParts = appSetting.Split(SEPARATOR);

                    if (settingParts.Length == 1)
                    {
                        if (collection[appSetting] == null)
                            collection.Add(appSetting, value);
                    }
                    else if (settingParts[0].Equals(environment))
                    {
                        if (collection[settingParts[1]] != null)
                            collection.Set(settingParts[1], value);
                        else
                            collection.Add(settingParts[1], value);
                    }
                }
            }
            return collection;
        }

        public string GetEnvironment()
        {
            return _configurationReader.AppSettings[ENVKEY];
        }
    }
}