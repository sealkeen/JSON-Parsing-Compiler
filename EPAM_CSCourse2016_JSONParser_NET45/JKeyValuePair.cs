using System.Text;

namespace EPAM.CSCourse2016.SilkinIvan.SealkeenJSON
{
    public class JKeyValuePair : JCollection
    {
        public override char Delimeter { get { return ':'; } }
        public JItem Key
        {
            get
            {
                if (Items.Count >= 1)
                    return Items[0];
                return new JSingleValue("Non-Valid Key.");
            }
            set
            {
                if (Items.Count == 0)
                    Items.Add(value);
                else
                    Items[0] = value;
            }
        }
        public JItem Value
        {
            get
            {
                if (Items.Count >= 2)
                    return Items[1];
                return new JSingleValue("Non-Valid Value.");
            }
            set
            {
                if (Items.Count == 1)
                    Items.Add(value);
                else if (Items.Count == 2)
                    Items[1] = value;
                //else throw new ArgumentException("Value cannot be set without a key.");
            }
        }
        public JKeyValuePair(JItem parent) : base(parent)
        {

        }
        public JKeyValuePair(JItem key, JItem value, JItem parent = null) : this(parent)
        {
            Key = key;
            Value = value;
            Parent = parent;
        }
        public override void BuildString(ref StringBuilder builder)
        {
            if( Key != null )
                Key.BuildString(ref builder);
            builder.Append(":");
            if( Value != null )
            Value.BuildString(ref builder);
        }
    }
}
