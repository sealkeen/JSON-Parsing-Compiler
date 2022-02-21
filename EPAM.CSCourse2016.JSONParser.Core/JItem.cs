using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EPAM.CSCourse2016.JSONParser.Library
{
    //Represents the structure of a JSON document
    public abstract class JItem
    {
        protected List<JItem> Items;
        public JItem Parent = null;
        bool built = false;
        public JItem(JItem parent = null)
        {
            Parent = parent;
        }

        public virtual List<JItem> Descendants()
        {
            return Items;
        }

        public List<JKeyValuePair> DescendantPairs()
        {
            List<JKeyValuePair> result = new List<JKeyValuePair>();
            
            foreach (var item in Items)
            {
                if (item.HasKeyOrValue())
                {
                    result.Add(item as JKeyValuePair);
                }
            }
            
            return result;
        }

        public virtual void Add(params JItem[] jItem)
        {

        }
        public bool SaveToFileAndOpenInNotepad(string filename, bool rewrite = false, string application = "notepad.exe")
        {
            bool result = ToFile(filename, rewrite);
            if (File.Exists(filename))
                System.Diagnostics.Process.Start(application, filename);
            else
                return false;
            return true;
        }
        public bool ToFile(string filename, bool rewrite = false)
        {
            StreamWriter sW;
            try
            {
                if (File.Exists(filename) && rewrite)
                {
                    sW = new StreamWriter(filename, !rewrite);
                }
                sW = new StreamWriter(filename, rewrite);
                sW.Write(ToString());
                sW.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public static JSingleValue Factory(JItemType itemType, string value)
        {
            switch (itemType)
            {
                case JItemType.SingleValue:
                    return new JSingleValue(value);
                case JItemType.String:
                    return new JString(value);
            }
            return null;
        }
        /// <summary>
        /// Finds a parent node that contains the value.
        /// </summary>
        /// <param name="jSingleValue">The parent property must not be null.</param>
        /// <returns></returns>
        public JItem FindContainerOrReturnParent(JSingleValue jSingleValue)
        {
            var container = this.Parent;
            while (container != null)
            {
                if (container.Contains(jSingleValue))
                    return container;
                container = container.Parent;
            }
            return this.Parent;
        }

        public JKeyValuePair FindPairByKey(JSingleValue key)
        {
            if (this.HasKeyOrValue() && this.ContainsKey(key))
                return this as JKeyValuePair;
            foreach (var i in this.Items)
            {
                FindPairByKey(key);
            }
            return new JKeyValuePair(key, new JString("null"));
        }

        public void ListAllPairs(ref List<JItem> nodes)
        {
            if (Items == null)
            {
                Items = new List<JItem>();
            }

            foreach (var jItem in Items)
            {
                jItem.ListAllPairs(ref nodes);
                if(jItem.HasKeyOrValue())
                    nodes.Add(jItem);
            }
        }

        public virtual void ListAllNodes(ref List<JItem> nodes)
        {
            if (Items == null)
            {
                Items = new List<JItem>();
            }
            foreach (var jItem in Items)
            {
                jItem.ListAllNodes(ref nodes);
                nodes.Add(jItem);
            }
        }

        public virtual bool Equals(JItem jitem)
        {
            return false;
        }
        public virtual bool Contains(JSingleValue jItem)
        {
            return false;
        }

        public virtual bool ContainsKeyAndValue(JSingleValue key, JSingleValue value)
        {
            return false;
        }

        public virtual bool ContainsKeyAndValueOf(JKeyValuePair of)
        {
            return false;
        }

        public virtual bool ContainsKeyAndValue(string key, string value)
        {
            return false;
        }

        public virtual bool ContainsKeyAndValueRecursive(JSingleValue key, JSingleValue value)
        {
            foreach (var jItem in Items)
            {
                if (ContainsKeyAndValue(key, value))
                    return true;

                ContainsKeyAndValueRecursive(key, value);
            }
            return false;
        }

        public virtual bool HasThesePairs(List<JKeyValuePair> attributes)
        {
            return false;
        }

        //StackOverflow
        public virtual JItem HasThesePairsRecursive(List<JKeyValuePair> sourcePairs)
        {
            var matchCount = 0;
            foreach (JItem targetPair in Items)
            {
                if (!targetPair.HasKeyOrValue())
                    continue;
                if (Matched(sourcePairs, targetPair) == 1)
                    matchCount++;
            }
            if (matchCount == sourcePairs.Count)
                return this;

            foreach (JItem jItem in Items)
            {
                if (!jItem.HasItems())
                    continue;
                HasThesePairsRecursive(sourcePairs);
            }

            return null;
        }

        private static int Matched(List<JKeyValuePair> sourcePairs, JItem targetPair)
        {
            int matchCount = 0;
            for (int a = 0; a < sourcePairs.Count; a++)
            {
                if (targetPair.ContainsKeyAndValueOf(sourcePairs[a]))
                    matchCount++;
            }
            return matchCount;
        }

        public virtual bool AddPairs(List<JKeyValuePair> items)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
            return true;
        }

        public virtual bool ContainsKey(JSingleValue jItem)
        {
            return false;
        }

        public virtual bool ContainsValue(JSingleValue jItem)
        {
            return false;
        }

        public virtual bool ContainsIntegerValue()
        {
            return false;
        }

        public virtual bool ContainsDateTimeValue()
        {
            return false;
        }

        public virtual int? GetIntegerValueOrReturnNull()
        {
            return null;
        }
        public virtual int? CompareIntsOrReturnNull(JSingleValue singleValue)
        {
            if (!(this is JSingleValue))
            {
                return null;
            } else {
                int? value = singleValue.GetIntegerValueOrReturnNull();
                if (value == null)
                    return null;
                var compared = value.Value.CompareTo( (this as JSingleValue).Contents);
                if (compared < 0)
                    return -1;
                else if (compared > 0)
                    return 1;
                return 0;
            }
        }
        public virtual bool HasItems()
        {
            return false;
        }
        /// <summary>
        /// Checks whether this JItem is a JKeyValuePair.
        /// </summary>
        /// <returns></returns>
        public virtual bool HasKeyOrValue()
        {
            return false;
        }

        public virtual string GetPairedValue()
        {
            return ("NaN");
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            BuildString(ref builder);
            return builder.ToString();
        }
        public virtual void BuildString(ref StringBuilder builder)
        {
            BuildString(ref builder);
        }
    }
}
