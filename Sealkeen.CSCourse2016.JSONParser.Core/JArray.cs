using System.Text;

namespace Sealkeen.CSCourse2016.JSONParser.Core
{
    public class JArray : JCollection
    {
        public override char Delimeter { get { return ','; } }
        public override string LeftBorder { get { return "["; } }
        public override string RightBorder { get { return "]"; } }
        public JArray(JItem parent) : base(parent)
        {

        }

        public override void BuildString(ref StringBuilder builder)
        {
            builder.Append(LeftBorder);
            for (int i = 0; i < Items.Count; ++i)
            {
                Items[i].BuildString(ref builder);
                builder.Append(((Items.Count - 1 == i) ? "" : ","));
            }
            builder.Append(RightBorder);
        }
    }
}
