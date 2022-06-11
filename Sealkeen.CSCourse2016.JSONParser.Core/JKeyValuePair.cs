using System;
using System.Collections.Generic;
using System.Text;

namespace Sealkeen.CSCourse2016.JSONParser.Core
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
                return new JString("Non-Valid Key.");
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
                return new JString("Non-Valid Value.");
            }
            set
            {
                if (Items.Count == 1)
                    Items.Add(value);
                else if (Items.Count >= 2)
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

        public JKeyValuePair(string key, string value, JItem parent = null) : this(parent)
        {
            Key = new JString(key);
            Value = new JString(value, this);
            
            Parent = parent;
        }

        public override JItem GetPairedValue()
        {
            if (Value != null)
                return Value;
            return
                base.GetPairedValue();
        }

        public override bool Contains(JSingleValue jItem)
        {
            if (Key.Equals(jItem) || Value.Equals(jItem))
                return true;
            return false;
        }

        public override bool ContainsKey(JSingleValue jItem)
        {
            if (this.Key.Equals(jItem))
            {
                return true;
            }
            return false;
        }

        public override bool ContainsValue(JSingleValue jItem)
        {
            if (this.Value.Equals(jItem))
            {
                return true;
            }
            return false;
        }

        public override bool ContainsKeyAndValue(JSingleValue key, JSingleValue value)
        {
            if (Key.Equals(key) && Value.Equals(value))
                return true;
            return false;
        }

        public override bool ContainsKeyAndValue(string key, string value)
        {
            if (Key.Equals(key) && Value.Equals(value))
                return true;
            return false;
        }

        public override bool ContainsKeyAndValueOf(JKeyValuePair of)
        {
            return ContainsKeyAndValue(of.Key.ToString(), of.Value.ToString());
        }

        public override bool ContainsIntegerValue()
        {
            if (Value != null)
            {
                if ((Value is JSingleValue) && Value.ContainsIntegerValue())
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
                if(int.TryParse(Value.AsUnquoted(), out result))
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
            return DateTime.Parse((Value as JSingleValue).AsUnquoted());
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

        public override bool AddPairs(List<JKeyValuePair> items)
        {
            return false;
        }
    }
}
