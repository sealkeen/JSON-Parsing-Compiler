﻿using System.Collections.Generic;
using System.Text;

namespace Sealkeen.CSCourse2016.JSONParser.Core
{
    public class JSingleValue : JItem
    {
        public string Contents;
        public JSingleValue(string value, JItem parent = null) : 
            base(parent)
        {
            Contents = value;
        }

        public override string ToString()
        {
            return Contents;
        }

        public override string AsUnquoted()
        {
            return Contents.Trim('\"');
        }

        public override bool Contains(JSingleValue jItem)
        {
            return Contents.Contains(jItem.Contents);
        }

        public override bool ContainsValue(JSingleValue jItem)
        {
            if (this.Contents.Equals(jItem))
            {
                return true;
            }
            return false;
        }

        public override bool ContainsIntegerValue()
        {
            if (Contents != null)
            {
                int result = 0;
                if (int.TryParse(Contents, out result) == true)
                    return true;
            }
            return false;
        }

        public override bool ContainsDateTimeValue()
        {
            if (Contents != null)
            {
                System.DateTime result;
                if (System.DateTime.TryParse(AsUnquoted(), out result) == true)
                    return true;
            }
            return false;
        }

        public override int? GetIntegerValueOrReturnNull()
        {
            if (ContainsIntegerValue())
            {
                int result;
                if (int.TryParse(Contents, out result))
                    return result;
            }
            return null;
        }

        public override bool Equals(JItem obj)
        {
            if ((obj is JSingleValue) && (obj as JSingleValue).Contents == this.Contents) {
                return true;
            }
            else {
                return false;
            }
        }

        public override bool Equals(string obj)
        {
            if (obj == this.Contents)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void BuildString(ref StringBuilder builder)
        {
            builder.Append((this as JSingleValue).Contents);
        }

        public override void ListAllNodes(ref List<JItem> nodes)
        {
            if (Items == null)
            {
                Items = new List<JItem>();
            }
        }
        public override bool AddPairs(IEnumerable<JKeyValuePair> items)
        {
            return false;
        }

        public override void Add(params JItem[] jItem)
        {

        }

        public override JItemType Type
        {
            get { return JItemType.SingleValue; }
        }
    }
}
