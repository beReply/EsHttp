﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsHttpAchieve.Extensions
{
    public static class StringExtension
    {


        public static bool IsNullOrWhiteSpace(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            return false;
        }
    }
}
