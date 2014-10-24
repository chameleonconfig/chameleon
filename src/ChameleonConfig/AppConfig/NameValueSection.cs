using System.Configuration;

namespace ChameleonConfig.AppConfig
{
    internal class NameValueSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public NameValueConfigurationCollection Settings
        {
            get { return (NameValueConfigurationCollection)base[""]; }
        }
    }
}