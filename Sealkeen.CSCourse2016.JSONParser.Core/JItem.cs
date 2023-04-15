using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sealkeen.CSCourse2016.JSONParser.Core
{
    /// <summary>
    /// Represents the structure of a JSON document.
    /// </summary>
    public abstract class JItem
    {
        public JItem(JItem parent = null)
        {
            Parent = parent;
        }

        public static bool AddIdentityForSerialization { get; set; } = false;
        public JItem Parent = null;
        public List<JItem> Items;

        /// <returns>Item's nodes list</returns>
        public virtual List<JItem> Descendants()
        {
            if (Items != null)
                return Items;
            else
                return new List<JItem>();
        }

        /// <returns>Determines whether the item can contain items.</returns>
        public virtual bool IsCollection()
        {
            return false;
        }

        /// <returns>null if node has no items.</returns>
        public JItem FirstNode()
        {
            if(Items != null && Items.Count > 0)
                return Items[0];
            return null;
        }

        /// <returns>List of all descendant JKeyValuePair nodes.</returns>
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

        /// <returns>Get all of the descendant pairs recursively.</returns>
        public List<JKeyValuePair> DescendantPairsRecursive()
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

        /// <returns>null if the item contains no array recursively</returns>
        public JArray FindFirstArray()
        {
            if (this is JArray)
                return this as JArray;
            foreach (var i in this.Items)
            {
                i.FindFirstArray();
            }
            return null;
        }

        /// <param name="key">Key of the pair to find.</param>
        /// <returns>null if the key was not found.</returns>
        public JKeyValuePair DescendantPair(string key)
        {
            JString str = new JString(key);
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    if (item.HasKeyOrValue())
                    {
                        if (item.ContainsKey(str))
                            return item as JKeyValuePair;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Overriden. Adding a pair to collection if it is a JCollection;
        /// </summary>
        /// <param name="jItem">Items to add.</param>
        public virtual void Add(params JItem[] jItem)
        {
            //TODO: Add body
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
            StreamWriter sW  = null;
            try
            {
                sW = new StreamWriter(filename, rewrite);
                sW.Write(ToString());
                sW.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                if (sW != null)
                    sW.Dispose();
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

        /// <summary>
        /// Recursively finds the pair idented by key.
        /// </summary>
        /// <param name="key">JKeyValuePair identation.</param>
        /// <returns></returns>
        public JKeyValuePair FindPairByKey(JSingleValue key)
        {
            if (this.HasKeyOrValue() && this.ContainsKey(key))
                return this as JKeyValuePair;
            else
            {
                if (Items != null)
                {
                    JKeyValuePair pair = null;
                    foreach (var i in this.Items)
                    {
                        pair = i.FindPairByKey(key);
                        if (pair != null)
                            return pair;
                    }
                }
            }
            return null;
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

        public virtual bool Equals(string jitem)
        {
            return false;
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
                {
                    if (sourcePairs.Count == 1)
                        return targetPair;
                    matchCount++;
                }
            }
            if (matchCount == sourcePairs.Count)
                return this;

            foreach (JItem jItem in Items)
            {
                if (!jItem.HasItems())
                    continue;
                var item = jItem.HasThesePairsRecursive(sourcePairs);
                if (item != null)
                    return item;
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

        public virtual bool AddPairs(IEnumerable<JKeyValuePair> items)
        {
            foreach (var item in items)
            {
                item.Parent = this;
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

        public virtual string AsUnquoted()
        {
            return null;
        }

        public virtual JItem GetPairedValue()
        {
            return new JString("NaN");
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

        public virtual long? ToUInt64()
        {
            return null;
        }

        public JArray ToArray()
        {
            JArray result = new JArray(this.Parent);
            result.Items = this.Items;
            return result;
        }
    }
}
