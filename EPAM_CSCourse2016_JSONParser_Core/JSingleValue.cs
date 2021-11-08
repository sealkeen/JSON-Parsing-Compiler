using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParserLibrary
{
    public class JSingleValue : JItem
    {
        public JSingleValue(string value, JItem parent = null) : 
            base(JItemType.SingleValue)
        {
            Contents = value;
            if (Contents[0] != '\"')
                Contents = '\"' + Contents;
            if (Contents[Contents.Length - 1] != '\"')
                Contents += '\"';
            Parent = parent;
        }
    }
}
