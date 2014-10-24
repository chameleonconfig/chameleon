using System;
using System.Security.Policy;

namespace ChameleonConfig
{
    public interface IConfigProvider
    {
        bool TryGetValue(Type type, string section, string setting, out object value);
    }

    public interface IConfigService
    {
        T Get<T>();
    }

    public class ConfigSectionAttribute : Attribute
    {
        private readonly string _name;

        public ConfigSectionAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }

    public class ConfigSettingAttribute : Attribute
    {
        private readonly string _name;

        public ConfigSettingAttribute(string name)
        {
            _name = name;
        }

        public string Setting
        {
            get { return _name; }
        }
    }
}
