using System;
using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2016.SilkinIvan.SealkeenJSON
{
    public class JSingleValue : JItem
    {
        public string Contents;
        public JSingleValue(string value, JItem parent = null) : 
            base(parent)
        {
            Contents = value;
            //if (Contents[0] != '\"')
            //    Contents = '\"' + Contents;
            //if (Contents[Contents.Length - 1] != '\"')
            //    Contents += '\"';
            Parent = parent;
        }

        public override void BuildString(ref StringBuilder builder)
        {
            builder.Append((this as JSingleValue).Contents);
        }
    }
}
