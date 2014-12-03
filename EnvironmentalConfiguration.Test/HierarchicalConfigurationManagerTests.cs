using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnvironmentalConfiguration.Test
{
    [TestClass]
    public class HierarchicalConfigurationManagerTests
    {
        [TestMethod]
        public void ShouldGetNormalConfigurationKey()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"myKey","myValue"}
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);
            var settings = config.AppSettings;
            string myValue = settings["myKey"];
            Assert.AreEqual(1, settings.Count);
            Assert.AreEqual("myValue",myValue);
        }

        [TestMethod]
        public void ShouldGetEnvironmentConfigurationKey()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"dev.myKey","myValueDev"}
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);
            config.Environment = "dev";
            var settings = config.AppSettings;
            string myValue = settings["myKey"];
            Assert.AreEqual(1, settings.Count);
            Assert.AreEqual("myValueDev", myValue);
        }

        [TestMethod]
        public void ShouldGetEnvironmentConfigurationKeyWhenNormalIsAlsoAvailableAfter()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"dev.myKey","myValueDev"},
                {"myKey","myValue"}
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);
            config.Environment = "dev";
            var settings = config.AppSettings;
            string myValue = settings["myKey"];
            Assert.AreEqual(1, settings.Count);
            Assert.AreEqual("myValueDev", myValue);
        }

        [TestMethod]
        public void ShouldGetEnvironmentConfigurationKeyWhenNormalIsAlsoAvailableBefore()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"myKey","myValue"},    
                {"dev.myKey","myValueDev"}
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);
            config.Environment = "dev";
            var settings = config.AppSettings;
            string myValue = settings["myKey"];
            Assert.AreEqual(1, settings.Count);
            Assert.AreEqual("myValueDev", myValue);
        }

        [TestMethod]
        public void ShouldCombineEnvironmentConfigurationAndNormalConfiguration()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"dev.myKey1","myValue1Dev"},
                {"myKey2","myValue2"}
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);
            config.Environment = "dev";
            var settings = config.AppSettings;
            string myValue1 = settings["myKey1"];
            string myValue2 = settings["myKey2"];
            Assert.AreEqual(2, settings.Count);
            Assert.AreEqual("myValue1Dev", myValue1);
            Assert.AreEqual("myValue2", myValue2);
        }

        [TestMethod]
        public void ShouldIgnoreUnspecifiedEnvironments()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"dev.myKey1","myValue1Dev"},
                {"test.myKey1","myValue1Test"},
                {"myKey2","myValue2"}
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);
            config.Environment = "dev";
            var settings = config.AppSettings;
            string myValue1 = settings["myKey1"];
            string myValue2 = settings["myKey2"];
            Assert.AreEqual(2, settings.Count);
            Assert.AreEqual("myValue1Dev", myValue1);
            Assert.AreEqual("myValue2", myValue2);
        }

        [TestMethod]
        public void ShouldGetEnvFromAppSettingsWhenNoConfigSpecified()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"env","dev"},

            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);

            Assert.AreEqual("dev", config.Environment);
        }

        [TestMethod]
        public void ShouldGetEnvValueFromCodeOverridingAppConfigValueWhenSpecified()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"env","dev"},
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);
            config.Environment = "test";

            Assert.AreEqual("test", config.Environment);
        }

        [TestMethod]
        public void ShouldGetValueForRightEnvironmentWhenEnvIsSpecifiedInAppSettings()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"env","dev"},
                {"dev.myKey1","myValue1Dev"},
                {"test.myKey1","myValue1Test"},
                {"myKey1","myValue1"}
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);

            var settings = config.AppSettings;
            string myValue1 = settings["myKey1"];
            Assert.AreEqual(1, settings.Count);
            Assert.AreEqual("myValue1Dev", myValue1);
        }

        [TestMethod]
        public void ShouldGetAllValuesIfNoEnvironmentIsSpecified()
        {
            NameValueCollection appSettings = new NameValueCollection()
            {
                {"dev.myKey1","myValue1Dev"},
                {"test.myKey1","myValue1Test"},
                {"myKey1","myValue1"}
            };
            MemoryBasedConfigurationReader configurationReader = new MemoryBasedConfigurationReader(appSettings);
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);

            var settings = config.AppSettings;

            Assert.AreEqual(3, settings.Count);
            Assert.AreEqual("myValue1", settings["myKey1"]);
            Assert.AreEqual("myValue1Dev", settings["dev.myKey1"]);
            Assert.AreEqual("myValue1Test", settings["test.myKey1"]);
        }
    }
}
