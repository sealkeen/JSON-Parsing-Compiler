using System;
using System.Collections.Generic;
using System.Text;

namespace Sealkeen.CSCourse2016.JSONParser.Core
{
    public class JItemFactory
    {
        public static JItem JSingleValue(string value, JItem parent = null)
        {
            if (value.StartsWith("\""))
            {
                return new JString(value, parent);
            } else {
                return new JSingleValue(value, parent);
            }
        }
    }
}
