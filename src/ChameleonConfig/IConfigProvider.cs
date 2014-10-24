﻿using System;
using System.Security.Policy;

namespace ChameleonConfig
{
    public interface IConfigProvider
    {
        bool TryGetValue(Type type, string section, string setting, out object value);
    }
}
