﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ChameleonConfig.Reflection
{
    public static class PropertyInfoExtensions
    {
        public static Func<T, object> CreateGetter<T>(this PropertyInfo propertyInfo)
        {
            if (typeof(T) != propertyInfo.DeclaringType)
            {
                throw new ConfigException("PropertyInfo declaring type ({0} <-> target type mismatch");
            }

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "instance");
            var property = Expression.Property(instance, propertyInfo);
            var convert = Expression.TypeAs(property, typeof(object));
            return (Func<T, object>)Expression.Lambda(convert, instance).Compile();
        }

        public static Action<T, object> CreateSetter<T>(this PropertyInfo propertyInfo)
        {
            if (typeof(T) != propertyInfo.DeclaringType)
            {
                throw new ArgumentException();
            }

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "instance");
            var argument = Expression.Parameter(typeof(object), "argument");
            var setterCall = Expression.Call(instance, propertyInfo.GetSetMethod(), Expression.Convert(argument, propertyInfo.PropertyType));

            return (Action<T, object>)Expression.Lambda(setterCall, instance, argument).Compile();
        }
    }
}