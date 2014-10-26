using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using ChameleonConfig.Resources;

namespace ChameleonConfig.AppConfig
{
    public class AppSettingsConfigProvider : IConfigProvider
    {
        private readonly IErrorMessageProvider _errorMessageProvider;
        private static readonly IDictionary<string, string> _default;

        static AppSettingsConfigProvider()
        {
            _default = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                _default.Add(key, ConfigurationManager.AppSettings.Get(key));
            }
        }

        public AppSettingsConfigProvider() : this(CultureInfo.GetCultureInfoByIetfLanguageTag("en"))
        {
        }

        internal AppSettingsConfigProvider(CultureInfo culture)
        {
            _errorMessageProvider = new ErrorMessageProvider(culture.IetfLanguageTag);
        }

        public bool TryGetValue(Type type, string section, string setting, out object value)
        {
            value = null;

            if (type == null)
            {
                throw new ConfigException(_errorMessageProvider.ConfigProviderTypeNotProvided(section, setting));
            }

            if (string.IsNullOrWhiteSpace(section))
            {
                string defaultAppSetting;
                if (_default.TryGetValue(setting, out defaultAppSetting))
                {
                    value = ConvertSetting(type, section, setting, defaultAppSetting);
                    return true;
                }

                return false;
            }

            var sectionObject = ConfigurationManager.GetSection(section);
            var nvs = sectionObject as NameValueSection;

            if (nvs != null)
            {
                var element = nvs.Elements[setting];

                if (element != null)
                {
                    var configValue = element.Value;
                    value = ConvertSetting(type, section, setting, configValue);

                    return true;
                }
            }

            return false;
        }

        private object ConvertSetting(Type type, string section, string setting, string configValue)
        {
            if (type == typeof(string))
            {
                return configValue;
            }

            var converter = TypeDescriptor.GetConverter(type);

            if (!converter.IsValid(configValue))
            {
                throw new ConfigException(_errorMessageProvider.CannotConvertSetting(configValue, setting, section, type));
            }


            return converter.ConvertFrom(configValue);
        }
    }
}