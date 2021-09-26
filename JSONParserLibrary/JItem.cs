using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JSONParserLibrary
{
    //Represents the structure of a JSON document
    public class JItem
    {
        public List<JItem> Items = new List<JItem>();
        public JItem Parent = null;
        public string Contents;
        public JItemType jType = JItemType.Object;
        public virtual void ParseJItem(JItem currentJItem, char symbol, bool pending4Value)
        {
            
        }

        public JItem(JItemType itemType, string contents = "", JItem parent = null)
        {
            jType = itemType;
            Contents = contents;
            Parent = parent;
        }
        public bool ToFile(string filename)
        {
            if (!File.Exists(filename))
            {
                StreamWriter sW = new StreamWriter(filename);
                sW.Write(ToString());
                sW.Close();
                return true;
            }
            return false;
        }

        public virtual bool Add(JItem jItem)
        {
            Items.Add(jItem);
            return true;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            BuildString(ref builder);
            return builder.ToString();
        }

        public void BuildString(ref StringBuilder builder)
        {
            switch (jType)
            {
                case JItemType.SingleValue:  //If our item is a Single Value
                    builder.Append(Contents);
                    break;
                case JItemType.Object:       //If our item is an object { }
                    builder.Append("{");
                    for (int i = 0; i < Items.Count; ++i)
                    {
                        Items[i].BuildString(ref builder);
                        builder.Append(((Items.Count - 1 == i) ? "" : ","));
                    }
                    builder.Append("}");
                    break;
                case JItemType.KeyValue:     //If our item is a "key":"value" pair key
                    var pair = (this as JKeyValuePair);
                    pair.Key.BuildString(ref builder);
                    builder.Append(":");
                    pair.Value.BuildString(ref builder);
                    break;
                case JItemType.Array:        //If our item is an array
                    builder.Append("[");
                    for (int i = 0; i < Items.Count; ++i)
                    {
                        Items[i].BuildString(ref builder);
                        builder.Append(((Items.Count - 1 == i) ? "" : ","));
                    }
                    builder.Append("]");
                    break;
                case JItemType.Root:
                    for (int i = 0; i < Items.Count; ++i)
                    {
                        Items[i].BuildString(ref builder);
                        builder.Append(((Items.Count - 1 == i) ? "" : ","));
                    }
                    break;
            }
        }
    }
}
