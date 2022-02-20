﻿using System.Collections.Generic;
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

        public JItem FindPairByKey(JSingleValue key)
        {
            if (this.Contains(key))
                return this.Parent;
            foreach (var i in this.Items)
            {
                FindPairByKey(key);
            }
            return null;
        }

        public void ListAllNodes(ref List<JItem> nodes)
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

        public virtual bool ContainsKey(JSingleValue jItem)
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
