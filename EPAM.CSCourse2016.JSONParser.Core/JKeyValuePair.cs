using System;
using System.Text;

namespace EPAM.CSCourse2016.JSONParser.Library
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
            }
        }
        public JKeyValuePair(JItem parent) : base(parent)
        {

        }
        public JKeyValuePair(JItem key, JItem value, JItem parent = null) : this(parent)
        {
            Key = key;
            if (value is JKeyValuePair)
            {
                Value = new JObject(this, value);
            }
            else
            {
                Value = value;
            }
            Parent = parent;
        }

        public override bool Contains(JSingleValue jItem)
        {
            if (Key.Equals(jItem) || Value.Equals(jItem))
                return true;
            return false;
        }

        public override bool ContainsIntegerValue()
        {
            if (Value != null)
            {
                if ((Value is JSingleValue) && (Value as JSingleValue).ContainsIntegerValue())
                    return true;
            }
            return false;
        }

        public override bool ContainsDateTimeValue()
        {
            if (Value != null)
            {
                if ((Value is JSingleValue) && (Value as JSingleValue).ContainsDateTimeValue())
                    return true;
            }
            return false;
        }

        public override int? GetIntegerValueOrReturnNull()
        {
            if (ContainsIntegerValue()) {
                int result;
                if(int.TryParse((Value as JSingleValue).Contents, out result))
                    return result;
            }
            return null;
        }
        public int GetIntegerValue()
        {
            return int.Parse((Value as JSingleValue).Contents);
        }
        public DateTime GetDateTimeValue()
        {
            return DateTime.Parse((Value as JSingleValue).GetValueQuotesRemoved());
        }

        public override bool HasKeyOrValue()
        {
            return true;
        }
        public override void BuildString(ref StringBuilder builder)
        {
            if ( Key != null )
                Key.BuildString(ref builder);
            builder.Append(":");
            if ( Value != null )
                Value.BuildString(ref builder);
        }

    }
}
