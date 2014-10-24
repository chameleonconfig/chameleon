using System;
using System.Reflection;

namespace ChameleonConfig
{
    public static class AttributeExtensions
    {
        public static bool TryGetAttribute(this Type type, out ConfigSectionAttribute attribute)
        {
            if (type == null)
            {
                attribute = null;
                return false;
            }

            var attributes = type.GetCustomAttributes(typeof(ConfigSectionAttribute), false);

            if (attributes.Length == 0)
            {
                attribute = null;
                return false;
            }

            attribute = (ConfigSectionAttribute) attributes[0];
            return true;
        }

        public static bool TryGetAttribute(this PropertyInfo type, out ConfigSettingAttribute attribute)
        {
            if (type == null)
            {
                attribute = null;
                return false;
            }

            var attributes = type.GetCustomAttributes(typeof(ConfigSettingAttribute), false);

            if (attributes.Length == 0)
            {
                attribute = null;
                return false;
            }

            attribute = (ConfigSettingAttribute)attributes[0];
            return true;
        }
    }
}