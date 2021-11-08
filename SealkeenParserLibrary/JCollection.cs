using System.Collections.Generic;
using System.Text;

namespace EPAM.CSCourse2016.SilkinIvan.SealkeenJSON
{
    public class JCollection : JItem
    {
        public virtual char Delimeter { get { return ','; } }
        public virtual string LeftBorder { get { return "{"; } }
        public virtual string RightBorder { get { return "}"; } }
        public JCollection(JItem parent) : base(parent)
        {
            Items = new List<JItem>();
        }
        public JCollection(JItem parent, params JItem[] jItems) : base(parent)
        {
            Items = new List<JItem>();
            Add(jItems);
        }
        public override bool HasItems()
        {
            if (Items == null)
                return false;
            if (Items.Count == 0)
                return false;
            else
                return true;
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
        public virtual void Add(params JItem[] jItem)
        {
            foreach (var item in jItem)
            {
                item.Parent = this;
                Items.Add(item);
            }
        }
    }
}
