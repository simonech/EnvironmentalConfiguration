using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnvironmentalConfiguration.Test
{
    [TestClass]
    public class ConfigurationStaticInstanceTests
    {

        [TestMethod]
        public void ShouldWorkWithStaticMethodCall()
        {
            //resetting to make sure static instance is at the default value: just a testing hack
            Configuration.ConfigurationManager = new EnvironmentalConfigurationManager();
            //End testing hack

            var settings = Configuration.AppSettings;
            Assert.AreEqual(2, settings.Count);
            Assert.AreEqual("value1", settings["myKey1"]);
            Assert.AreEqual("value2Dev", settings["myKey2"]);
        }

        [TestMethod]
        public void ShouldWorkWithAssignedStaticInstance()
        {
            EnvironmentalConfigurationManager config = new EnvironmentalConfigurationManager();
            config.Environment = "test";
            Configuration.ConfigurationManager = config;

            var settings = Configuration.AppSettings;

            Assert.AreEqual(2, settings.Count);
            Assert.AreEqual("value1", settings["myKey1"]);
            Assert.AreEqual("value2Test", settings["myKey2"]);
        }
    }
}
