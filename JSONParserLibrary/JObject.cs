using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParserLibrary
{
    public class JObject : JItem
    {
        public JObject(JItem parent = null) : base(JItemType.Object, "", parent)
        {
            
        }

        public override void ParseJItem(JItem currentJItem, char symbol, bool pending4Value)
        {
            if (pending4Value) //Check if our key:value pair expects value
            {
                JItem item = new JItem(JItemType.Object);
                item.Parent = currentJItem;
                //((JKeyValuePair)keyValueList[keyValueList.Count - 1]).Value = item;
                //buffer.Clear();
                    //keyValueList.Add(item);
                //itemList.Add(item);
                pending4Value = false;
            }
            else
            {
                JItem nItm = new JItem(JItemType.Object);
                currentJItem.Items.Add(nItm);
                nItm.Parent = currentJItem;
                //itemList.Add(nItm);
            }
        }
    }
}
