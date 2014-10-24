using System;

namespace ChameleonConfig
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
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
}