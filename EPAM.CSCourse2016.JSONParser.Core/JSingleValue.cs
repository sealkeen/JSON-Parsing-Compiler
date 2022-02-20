using System.Text;

namespace EPAM.CSCourse2016.JSONParser.Library
{
    public class JSingleValue : JItem
    {
        public string Contents;
        public JSingleValue(string value, JItem parent = null) : 
            base(parent)
        {
            Contents = value;
        }

        public string GetValueQuotesRemoved()
        { 
            return Contents.Trim('\"');
        }

        public override bool Contains(JSingleValue jItem)
        {
            return Contents.Contains(jItem.Contents);
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
                if (System.DateTime.TryParse(GetValueQuotesRemoved(), out result) == true)
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
        public override void BuildString(ref StringBuilder builder)
        {
            builder.Append((this as JSingleValue).Contents);
        }
    }
}
