using System;

namespace ChameleonConfig
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ConfigSettingAttribute : Attribute
    {
        private readonly string _name;

        public ConfigSettingAttribute()
        {
        }

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