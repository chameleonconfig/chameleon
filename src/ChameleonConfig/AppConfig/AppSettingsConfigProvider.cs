using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

        public AppSettingsConfigProvider(IErrorMessageProvider errorMessageProvider)
        {
            _errorMessageProvider = errorMessageProvider;
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
                foreach (var key in nvs.Settings.AllKeys)
                {
                    if (string.Equals(key, setting, StringComparison.OrdinalIgnoreCase))
                    {
                        var configValue = nvs.Settings[key].Value;
                        value = ConvertSetting(type, section, setting, configValue);

                        return true;
                    }
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