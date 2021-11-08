﻿using System.Text;

namespace EPAM.CSCourse2016.SilkinIvan.JSONParser
{
    public class JRoot : JCollection
    {
        public override char Delimeter { get { return ','; } }
        public override string LeftBorder { get { return ""; } }
        public override string RightBorder { get { return ""; } }
        public JRoot() : base(null)
        { }
        public override void BuildString(ref StringBuilder builder)
        {
            for (int i = 0; i < Items.Count; ++i)
            {
                Items[i].BuildString(ref builder);
                builder.Append(((Items.Count - 1 == i) ? "" : ","));
            }
        }
    }
}
