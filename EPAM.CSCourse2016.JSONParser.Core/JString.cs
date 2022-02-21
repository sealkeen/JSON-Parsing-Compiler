using System;

namespace EPAM.CSCourse2016.JSONParser.Library
{
    public class JString : JSingleValue
    {
        public JString(string value, JItem parent = null) : base (value, parent)
        {
            //Contents = value;
            if (Contents.Length > 1 && Contents[0] != '\"')
                Contents = '\"' + Contents;
            if (Contents.Length > 1 && Contents[Contents.Length - 1] != '\"')
                Contents += '\"';
        }

        public int ToInt32()
        {
            try
            {
                return Convert.ToInt32(Contents.Trim('\"'));
            } catch {
                return 0;
            }
        }
    }
}
