using System;

namespace ChameleonConfig
{
    public class ConfigException : Exception
    {
        public ConfigException(string message) : base(message)
        {
        }
    }
}