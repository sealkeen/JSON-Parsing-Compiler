using System.Text;

namespace EPAM.CSCourse2016.SilkinIvan.SealkeenJSON
{
    public class JObject : JCollection
    {
        public JObject(JItem parent, params JItem[] jItems) : base(parent, jItems)
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
