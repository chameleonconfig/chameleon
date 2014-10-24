using System;

namespace ChameleonConfig
{
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