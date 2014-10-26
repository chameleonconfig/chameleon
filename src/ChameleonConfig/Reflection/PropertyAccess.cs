using System;
using System.Reflection;

namespace ChameleonConfig.Reflection
{
    internal class PropertyAccess<T> : IPropertyAccess<T>
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly Action<T, object> _setter;

        public PropertyAccess(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
            _setter = propertyInfo.CreateSetter<T>();
        }

        public Action<T, object> Setter
        {
            get { return _setter; }
        }

        public string Name
        {
            get { return _propertyInfo.Name; }
        }

        public Type Type { get { return _propertyInfo.PropertyType; } }

        public bool TryGetAttribute<TAttribute>(out TAttribute result) where TAttribute : Attribute
        {
            return _propertyInfo.TryGetAttribute(out result);
        }
    }
}