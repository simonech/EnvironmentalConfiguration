using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvironmentalConfiguration;

namespace EnvironmentalConfiguration
{
    public class EnvironmentalConfigurationManager
    {

        private readonly IAppSettingsReader _appSettingsReader;

        public EnvironmentalConfigurationManager(IAppSettingsReader appSettingsReader)
        {
            _appSettingsReader = appSettingsReader;
            Environment = _appSettingsReader.GetEnvironment();
        }

        public EnvironmentalConfigurationManager()
            : this(new AppSettingsBasedConfigurationReader()) {}

        /// <summary>
        /// The list of configurations
        /// </summary>
        public NameValueCollection AppSettings
        {
            get { return _appSettingsReader.GetAppSettings(Environment); }
        }

        /// <summary>
        /// The Environment for which configuration is needed
        /// </summary>
        public string Environment { get; set; }
    }
}
