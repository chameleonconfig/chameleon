using System;
using System.Globalization;
using System.Resources;

namespace ChameleonConfig.Resources
{
    public class ErrorMessageProvider : IErrorMessageProvider
    {
        private readonly ResourceManager _resourceManager;
        private readonly CultureInfo _culture;

        public ErrorMessageProvider(string language)
        {
            _resourceManager = new ResourceManager("ChameleonConfig.Resources.Errors", typeof(ErrorMessageProvider).Assembly);
            _culture = CultureInfo.CreateSpecificCulture(language);
        }

        public string ConfigProviderTypeNotProvided(string section, string setting)
        {
            var resourceString = _resourceManager.GetString("ConfigProviderTypeNotProvided", _culture);

            if (resourceString == null)
            {
                return null;
            }

            return string.Format(resourceString, section, setting);
        }

        public string CannotConvertSetting(string configValue, string setting, string section, Type type)
        {
            var resourceString = _resourceManager.GetString("CannotConvertSetting", _culture);

            if (resourceString == null)
            {
                return null;
            }

            return string.Format(resourceString, configValue, setting, section, type);
        }
    }
}