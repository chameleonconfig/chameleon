using System.Reflection;
using NUnit.Framework;

namespace ChameleonConfig.Tests.Unit
{
    [TestFixture]
    public class ConfigSettingAttributeTests
    {
        [Test]
        public void TestAttributeRetrieval()
        {
            // Setup
            var property = typeof (ExampleConfig).GetProperty("Property");
            ConfigSettingAttribute attribute;

            // Execute
            var result = property.TryGetAttribute(out attribute);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(attribute.Setting, Is.Null);
        }

        [Test]
        public void TestAttributeRetrievalNamed()
        {
            // Setup
            var property = typeof(ExampleConfig).GetProperty("NamedProperty");
            ConfigSettingAttribute attribute;

            // Execute
            var result = property.TryGetAttribute(out attribute);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(attribute.Setting, Is.EqualTo("Named"));
        }

        [Test]
        public void TestAttributeRetrievalPropertyInfoNull()
        {
            // Setup
            PropertyInfo property = null;
            ConfigSettingAttribute attribute;

            // Execute
            var result = property.TryGetAttribute(out attribute);

            // Assert
            Assert.That(result, Is.False);
        }

        private class ExampleConfig
        {
            [ConfigSetting]
            public string Property { get; set; }

            [ConfigSetting("Named")]
            public string NamedProperty { get; set; }
        }
    }
}