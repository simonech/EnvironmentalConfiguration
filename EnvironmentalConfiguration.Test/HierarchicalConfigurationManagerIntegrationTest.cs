using EnvironmentalConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnvironmentalConfiguration.Test
{
    [TestClass]
    public class HierarchicalConfigurationManagerIntegrationTest
    {
        [TestMethod]
        [DeploymentItem("App.Config")]
        public void ShouldReadCorrectConfigurationFromFile()
        {
            ConfigFileBasedConfigurationReader configurationReader = new ConfigFileBasedConfigurationReader();
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);

            var settings = config.AppSettings;

            Assert.AreEqual(2, settings.Count);
            Assert.AreEqual("value1", settings["myKey1"]);
            Assert.AreEqual("value2Dev", settings["myKey2"]);
        }

        [TestMethod]
        [DeploymentItem("App.Config")]
        public void ShouldOverrideEnvFromCode()
        {
            ConfigFileBasedConfigurationReader configurationReader = new ConfigFileBasedConfigurationReader();
            IAppSettingsReader reader = new AppSettingsBasedConfigurationReader(configurationReader);
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager(reader);
            config.Environment = "test";

            var settings = config.AppSettings;

            Assert.AreEqual(2, settings.Count);
            Assert.AreEqual("value1", settings["myKey1"]);
            Assert.AreEqual("value2Test", settings["myKey2"]);
        }
    }
}
