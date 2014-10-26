using System;

namespace ChameleonConfig.Reflection
{
    internal interface IPropertyAccess<T>
    {
        Action<T, object> Setter { get; }

        string Name { get; }

        Type Type { get; }

        bool TryGetAttribute<TAttribute>(out TAttribute result) where TAttribute : Attribute;

    }
}