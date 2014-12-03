using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentalConfiguration
{
    public static class  Configuration
    {
        private static EnvironmentalConfigurationManager _configurationManager = new EnvironmentalConfigurationManager();

        public static EnvironmentalConfigurationManager ConfigurationManager
        {
            get { return _configurationManager; }
            set { _configurationManager = value; }
        }

        public static NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }
    }
}
