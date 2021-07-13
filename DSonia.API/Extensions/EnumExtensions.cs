﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using System.ComponentModel;

namespace DSonia.API.Extensions
{
    static class EnumExtensions
    {
        public static string ToDescriptionString<T>(this T @enum) { 
            FieldInfo info = @enum.GetType().GetField(@enum.ToString());
            var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false); 

            return attributes?[0].Description ?? @enum.ToString();
        }
    }
}
