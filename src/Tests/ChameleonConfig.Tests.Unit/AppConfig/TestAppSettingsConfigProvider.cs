using ChameleonConfig.AppConfig;
using NUnit.Framework;

namespace ChameleonConfig.Tests.Unit.AppConfig
{
    [TestFixture]
    public class TestAppSettingsConfigProvider
    {
        [Test]
        public void TestDefaultAppSettingsFound()
        {
            // Setup
            var provider = new AppSettingsConfigProvider();

            // Execute
            object result;
            var found = provider.TryGetValue(typeof(int), null, "Test", out result);

            // Assert
            Assert.IsTrue(found);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void TestDefaultAppSettingsFoundWrongType()
        {
            // Setup
            var provider = new AppSettingsConfigProvider();
            object result;

            // Execute and Assert
            Assert.Throws<ConfigException>(() => provider.TryGetValue(typeof(int), null, "Invalid", out result));
        }

        [Test]
        public void TestDefaultAppSettingsNotFound()
        {
            // Setup
            var provider = new AppSettingsConfigProvider();

            // Execute
            object result;
            var found = provider.TryGetValue(typeof(int), null, "Test1", out result);

            // Assert
            Assert.IsFalse(found);
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void TestSectionHandlingWithService()
        {
            // Setup
            var provider = new AppSettingsConfigProvider();
            var service = new ConfigService();
            service.AddConfigProvider(provider);

            // Execute
            var config = service.Get<SomeSettingsConfig>();

            // Assert
            Assert.That(config, Is.Not.Null);
            Assert.That(config.PropertyOne, Is.EqualTo(1));
        }

        [ConfigSection("someSettings")]
        private class SomeSettingsConfig
        {
            public int PropertyOne { get; set; }
        }
    }
}