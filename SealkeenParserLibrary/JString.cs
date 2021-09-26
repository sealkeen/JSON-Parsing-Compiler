using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.CSCourse2016.SilkinIvan.SealkeenJSON
{
    public class JString : JSingleValue
    {
        public JString(string value, JItem parent = null) : base (value, parent)
        {
            Contents = value;
            if (Contents[0] != '\"')
                Contents = '\"' + Contents;
            if (Contents[Contents.Length - 1] != '\"')
                Contents += '\"';
        }
    }
}
