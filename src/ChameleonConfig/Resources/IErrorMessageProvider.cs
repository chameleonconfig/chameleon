using System;

namespace ChameleonConfig.Resources
{
    public interface IErrorMessageProvider
    {
        string ConfigProviderTypeNotProvided(string section, string setting);

        string CannotConvertSetting(string configValue, string setting, string section, Type type);
    }
}