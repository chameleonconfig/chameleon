using System;
using System.Reflection;

namespace ChameleonConfig
{
    public static class AttributeExtensions
    {
        public static bool TryGetAttribute<T>(this Type type, out T attribute)
            where T : Attribute
        {
            if (type == null)
            {
                attribute = default(T);
                return false;
            }

            var attributes = type.GetCustomAttributes(typeof(T), false);

            if (attributes.Length == 0)
            {
                attribute = default(T);
                return false;
            }

            attribute = (T) attributes[0];
            return true;
        }

        public static bool TryGetAttribute<T>(this PropertyInfo type, out T attribute)
            where T : Attribute
        {
            if (type == null)
            {
                attribute = default(T);
                return false;
            }

            var attributes = type.GetCustomAttributes(typeof(T), false);

            if (attributes.Length == 0)
            {
                attribute = default(T);
                return false;
            }

            attribute = (T)attributes[0];
            return true;
        }
    }
}