using System;

namespace Sealkeen.CSCourse2016.JSONParser.Core
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

        public override bool Equals(string obj)
        {
            if (obj.Trim('\"') == this.Contents.Trim('\"'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool ContainsIntegerValue()
        {
            if (Contents.Trim('\"') != null)
            {
                int result = 0;
                if (int.TryParse(Contents.Trim('\"'), out result) == true)
                    return true;
            }
            return false;
        }

        public override int? GetIntegerValueOrReturnNull()
        {
            if (Contents.Trim('\"') != null)
            {
                int result = 0;
                if (int.TryParse(Contents.Trim('\"'), out result) == true)
                    return result;
            }
            return null;
        }

        public override JItemType Type
        {
            get { return JItemType.String; }
        }
    }
}
