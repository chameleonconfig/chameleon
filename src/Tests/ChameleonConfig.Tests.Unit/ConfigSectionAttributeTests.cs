using System;
using NUnit.Framework;

namespace ChameleonConfig.Tests.Unit
{
    [TestFixture]
    public class ConfigSectionAttributeTests
    {
        [Test]
        public void TestAttributeRetrieval()
        {
            // Setup
            var type = typeof (ExampleConfig);
            ConfigSectionAttribute attribute;

            // Execute
            var result = type.TryGetAttribute(out attribute);

            // Assert
            Assert.IsTrue(result);
            Assert.That(attribute.Name, Is.EqualTo("Example"));
        }

        [Test]
        public void TestAttributeRetrievalTypeNull()
        {
            // Setup
            Type type = null;
            ConfigSectionAttribute attribute;

            // Execute
            var result = type.TryGetAttribute(out attribute);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TestAttributeRetrievalNotInherited()
        {
            // Setup
            var type = typeof(ChildConfig);
            ConfigSectionAttribute attribute;

            // Execute
            var result = type.TryGetAttribute(out attribute);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TestAttributeRetrievalNotPresent()
        {
            // Setup
            var type = typeof(NoAttributeConfig);
            ConfigSectionAttribute attribute;

            // Execute
            var result = type.TryGetAttribute(out attribute);

            // Assert
            Assert.IsFalse(result);
        }

        [ConfigSection("Example")]
        private class ExampleConfig
        {
        }

        private class NoAttributeConfig
        {
        }

        private class ChildConfig : ExampleConfig
        {
        }
    }
}
