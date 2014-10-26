using System;
using System.Collections.Generic;
using System.Globalization;
using ChameleonConfig.Reflection;

namespace ChameleonConfig
{
    public class ConfigService : IConfigService
    {
        private readonly List<IConfigProvider> _providers;
        private readonly CultureInfo _cultureInfo;

        public ConfigService() : this(CultureInfo.GetCultureInfoByIetfLanguageTag("en"))
        {
        }

        public ConfigService(CultureInfo culture)
        {
            _cultureInfo = culture;
            _providers = new List<IConfigProvider>();
        }

        public void AddConfigProvider(IConfigProvider configProvider)
        {
            _providers.Add(configProvider);
        }

        public T Get<T>()
        {
            var type = typeof (T);

            var result = Activator.CreateInstance<T>();

            ConfigSectionAttribute configSectionAttribute;

            var sectionName = type.TryGetAttribute(out configSectionAttribute) ? configSectionAttribute.Name : type.Name;

            foreach (var property in ObjectAccess<T>.Accessors)
            {
                ConfigSettingAttribute configSettingAttribute;

                var settingName = property.TryGetAttribute(out configSettingAttribute)
                    ? configSectionAttribute.Name
                    : property.Name;

                foreach (var provider in _providers)
                {
                    object value;
                    if (provider.TryGetValue(property.Type, sectionName, settingName, out value))
                    {
                        property.Setter(result, value);
                    }
                }
            }

            return result;
        }
    }
}