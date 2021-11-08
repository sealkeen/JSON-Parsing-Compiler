using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParserLibrary
{
    //Represents the structure of a key:value pair
    public class JKeyValuePair : JItem
    {
        public override void ParseJItem(JItem jItem, char symbol, bool pending4Value)
        {

        }
        public void ParseKey(JKeyValuePair jKeyValuePair, char symbol)
        {

        }
        public void ParseValue(JKeyValuePair jKeyValuePair, char symbol)
        {
            
        }

        public JKeyValuePair() : base(JItemType.KeyValue, "")
        {
            
        }
        public JKeyValuePair(JItem parent, string contents) : this()
        {
            Contents = contents; 
            Parent = parent;
        }
        public JItem Key
        {
            get 
            {
                if (Items.Count >= 1)
                    return Items[0]; 
                return new JItem(JItemType.SingleValue, "Non-Valid Key.");
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
            get { 
                if(Items.Count >= 2) 
                    return Items[1];  
                return new JItem(JItemType.SingleValue, "Non-Valid Value.");
            }
            set
            {
                if (Items.Count == 1)
                    Items.Add(value);
                else if (Items.Count == 2)
                    Items[1] = value;
                else throw new ArgumentException("Value cannot be set with a key.");
            }
        }
    }
}
