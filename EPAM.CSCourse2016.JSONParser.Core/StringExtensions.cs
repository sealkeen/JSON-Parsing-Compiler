using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2016.JSONParser.Library
{
    public static class StringExtensions
    {
        public static JString ToJString(this string source)
        {
            JString jString = new JString(source);
            return jString;
        }

        public static JSingleValue ToSingleValue(this string source)
        {
            JSingleValue jSingleValue = new JSingleValue(source.Trim('\"'));
            return jSingleValue;
        }
    }
}
