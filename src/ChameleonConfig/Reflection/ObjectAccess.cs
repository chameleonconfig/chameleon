using System.Collections.Generic;
using System.Reflection;

namespace ChameleonConfig.Reflection
{
    internal class ObjectAccess<T>
    {
        private static readonly IList<IPropertyAccess<T>> _properties;

        static ObjectAccess()
        {
            var type = typeof (T);
            _properties = new List<IPropertyAccess<T>>();

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var access = new PropertyAccess<T>(property);
                _properties.Add(access);
            }
        }

        public static IEnumerable<IPropertyAccess<T>> Accessors
        {
            get
            {
                return _properties;
            }
        }
    }
}