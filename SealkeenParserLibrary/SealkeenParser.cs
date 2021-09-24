//CREATED BY SILKIN IVAN FOR EPAM TRAINING COURSES ONLY
//THIS PRODUCT IS NOT COVERED BY ANY LICENSE AND GIVEN YOU
//"AS IS". DESPITE THAT, YOU ARE NOT ALLOWED TO USE THIS
//SIGNATURE IF YOU MODIFIED ANY PART OF THE PROGRAM WITHOUT
//MY PERSONAL AGREEMENT SO BE CAREFUL NOT TO SIGN YOUR
//MODIFICATION USING MY SIGNATURE, IT WOULD BE UNFAIR!
//                2016 EPAM TRAINING.

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using EPAM.CSCourse2016.ParserPerfTester.Common;

namespace EPAM.CSCourse2016.SilkinIvan.SealkeenJSON
{
    //Represents methods to parse JSON
    public class SealkeenParserClass : IParser
    {
        //Token characters
        private static List<char> syntaxChars = new List<char>();
        //This list contains all of the items
        private static List<JItem> itemList = new List<JItem>();
        //This list contains Key : Value pair objects
        private static List<JItem> keyValueList = new List<JItem>();

        //Current item we are using to add items in
        private JItem currentItem = new JItem { jType = ItemType.Object};
        private JItem currentKV = new JKeyValuePair { jType = ItemType.KeyValue};

        //Buffer string for values and items
        private static StringBuilder buffer = new StringBuilder("");

        //Source string
        private static StringBuilder source;

        //Do not delete this variable despite the warning!
        bool runFromOutside = false;
        //While we are  inside quotes - we add our character to the Buffer (Without checking it for syntax token belonging)
        bool insideQuotes = false;
        //We expect some pentagon secrets to be inside the JSON value if true
        bool pending4Value = false;
        //Count of pentagon secrets
        private int indexOfTheChar = -1;

        //Parse inside of this class
        private void InnerParse()
        {
            try
            {
                for (int c = indexOfTheChar; c < source.Length; c++)
                {
                    StartParse();
                }
            }
            catch 
            {
                if (source.Length > (indexOfTheChar+1)) //if inccorrect symbol is inside of the string
                {
                    Debug.WriteLine($"Unexpected symbol under index: {indexOfTheChar}. Total Length : {source.Length}");
                    InnerParse();
                }
            }
            if (itemList.Count == 1 && keyValueList.Count == 1)
            {
                currentItem = itemList[itemList.Count-1];
                if (currentItem.items.Count == 0 && currentItem.items.Count == 0)
                {
                    currentItem.items.Clear(); 
                    currentItem.items.Add(new JItem { contents = buffer.Replace(" ", "").ToString(), jType = ItemType.SingleValue , parent = currentItem} );
                    buffer.Clear();
                }
            }
        }
         
        public string ToTestString(string str)
        {
            runFromOutside = true;
            source = new StringBuilder(str);
            InitializeStructure();
            InnerParse();
            FindRoot();
            return currentItem.ToString();
        }

        public JItem GetCurrentItem()
        {
            return currentItem;
        }

        //Find root object
        private void FindRoot()
        {
            while (currentItem.parent != null)
            {
                currentItem = currentItem.parent;
            }
        }

        //Initialize inner structure
        private void InitializeStructure()
        {

            JKeyValuePair newPair = new JKeyValuePair { parent = null };
            JItem Root = new JItem { jType = ItemType.Root };
            keyValueList.Clear();
            itemList.Clear();
            syntaxChars.Clear();

            keyValueList.Add(newPair);
            itemList.Add(Root);
            syntaxChars.Add(',');

            indexOfTheChar = -1;
            currentItem = Root;
        }


        /// <summary>
        /// The Main Big Parsing Method
        /// </summary>
        private void StartParse()
        {
            //The index inside the Source string  
            ++indexOfTheChar;

            //If the symbol is inside quotes, we append it to Buffer without creating objects
            if (insideQuotes)
            {
                if (syntaxChars[syntaxChars.Count-1] == '\"')
                {
                    if (source[indexOfTheChar] == '\"')
                    {
                        buffer.Append(source[indexOfTheChar]);
                        insideQuotes = false;
                        return;
                    }
                    buffer.Append(source[indexOfTheChar]);
                    return;
                }
            }
            else
            {
                switch (source[indexOfTheChar])
                {
                    case '{':
                        currentItem = itemList[itemList.Count-1];
                        if (pending4Value) //Check if our key:value pair expects value
                        {
                            JItem item = new JItem { jType = ItemType.Object };
                            item.parent = currentItem;
                            ((JKeyValuePair)keyValueList[keyValueList.Count-1]).Value = item;
                            buffer.Clear();
                            keyValueList.Add(item);
                            itemList.Add(item);
                            pending4Value = false;
                        }
                        else
                        {
                            JItem nItm = new JItem { jType = ItemType.Object };
                            currentItem.items.Add(nItm);
                            nItm.parent = currentItem;
                            itemList.Add(nItm);
                        }
                        break;
                    case '}':
                        if (pending4Value)  //Check if our key:value pair expects value
                        {
                            JItem item = new JItem { jType = ItemType.SingleValue };
                            item.parent = itemList[itemList.Count-1];
                            item.contents = buffer.ToString();
                            ((JKeyValuePair)keyValueList[keyValueList.Count-1]).Value = item;
                            buffer.Clear();
                            keyValueList.RemoveAt(keyValueList.Count-1);
                            pending4Value = false;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(buffer.ToString()))
                            {
                                itemList[itemList.Count-1].items.Add(new JItem { jType = ItemType.SingleValue, contents = buffer.ToString() });
                                buffer.Clear();
                            }
                        }
                        itemList.RemoveAt(itemList.Count-1);
                        break;
                    case '[':
                        currentItem = itemList[itemList.Count-1];
                        //Проверка на ожидание инициализации значения Value пары KeyValue 
                        if (pending4Value) //Check if our key:value pair expects value
                        {
                            JItem item = new JItem { jType = ItemType.Array };
                            item.parent = currentItem;
                            ((JKeyValuePair)keyValueList[keyValueList.Count-1]).Value = item;
                            buffer.Clear();
                            keyValueList.Add(item);
                            itemList.Add(item);
                            pending4Value = false;
                        }
                        else
                        {
                            JItem nItm = new JItem { jType = ItemType.Array };
                            currentItem.items.Add(nItm);
                            nItm.parent = currentItem;
                            itemList.Add(nItm);
                        }
                        break;
                    case ']':
                        if (pending4Value) //Check if our key:value pair expects value
                        {
                            JItem item = new JItem { jType = ItemType.SingleValue };
                            item.parent = itemList[itemList.Count-1];
                            item.contents = buffer.ToString();
                            ((JKeyValuePair)keyValueList[keyValueList.Count-1]).Value = item;
                            buffer.Clear();
                            keyValueList.RemoveAt(keyValueList.Count-1);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(buffer.ToString()))
                            {
                                itemList[itemList.Count-1].items.Add(new JItem { jType = ItemType.SingleValue, contents = buffer.ToString() });
                                buffer.Clear();
                            }
                        }
                        //currentItem = currentItem.Parent;
                        itemList.RemoveAt(itemList.Count-1);
                        pending4Value = false;
                        break;
                    case ':':
                        currentKV = keyValueList[keyValueList.Count-1];
                        currentItem = itemList[itemList.Count-1];
                        syntaxChars.Add(':');
                        if (!pending4Value)
                        {
                            JKeyValuePair pair = new JKeyValuePair { jType = ItemType.KeyValue};
                            pair.Key = new JItem { jType = ItemType.SingleValue };
                            pair.Key.contents = buffer.ToString();
                            pair.parent = currentItem;
                            currentItem.items.Add(pair);
                            keyValueList.Add(pair);
                        }
                        else
                        {
                            //throw new InvalidEnumArgumentException(); //не было более подходящего, лел
                        }
                        pending4Value = true;
                        buffer.Clear();
                        break;
                    case ',':
                        currentKV = keyValueList[keyValueList.Count-1];
                        if (pending4Value)
                        {
                            JItem jI = new JItem { jType = ItemType.SingleValue, contents = buffer.ToString(), parent = itemList[itemList.Count-1] };
                            ((JKeyValuePair)currentKV).Value = jI;
                            keyValueList.RemoveAt(keyValueList.Count-1);
                            pending4Value = false;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(buffer.ToString()))
                            {
                                itemList[itemList.Count-1].items.Add(new JItem { contents = buffer.ToString(), jType = ItemType.SingleValue });
                                buffer.Clear(); return;
                            }
                        }
                        syntaxChars.Add(',');
                        buffer.Clear();
                        break;
                    case '\"':
                        buffer.Append(source[indexOfTheChar]);
                        syntaxChars.Add('\"');
                        insideQuotes = !insideQuotes;
                        break;
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        break;
                    default:
                        buffer.Append(source[indexOfTheChar]);
                        break;
                }
            }
        }
    }

    //Represents the structure of a JSON document
    public class JItem 
    {
        public List<JItem> items = new List<JItem>();
        public JItem parent = null;
        public string contents;
        public ItemType jType = ItemType.Object;

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
                case ItemType.SingleValue:  //If our item is a Single Value
                    builder.Append(contents);
                    break;
                case ItemType.Object:       //If our item is an object { }
                    builder.Append("{");
                    for (int i = 0; i < items.Count; ++i)
                    {
                        items[i].BuildString(ref builder);
                        builder.Append(((items.Count - 1 == i) ? "" : ","));
                    }
                    builder.Append("}");
                    break;
                case ItemType.KeyValue:     //If our item is a "key":"value" pair key
                    var pair = (this as JKeyValuePair);
                    pair.Key.BuildString(ref builder);
                    builder.Append(":");
                    pair.Value.BuildString(ref builder);
                    break;
                case ItemType.Array:        //If our item is an array
                    builder.Append("[");
                    for (int i = 0; i < items.Count; ++i)
                    {
                        items[i].BuildString(ref builder);
                        builder.Append(((items.Count - 1 == i) ? "" : ","));
                    }
                    builder.Append("]");
                    break;
                case ItemType.Root:
                    for (int i = 0; i < items.Count; ++i)
                    {
                        items[i].BuildString(ref builder);
                        builder.Append(((items.Count - 1 == i) ? "" : ","));
                    }
                    break;
            }
        }
    }

    //Represents the structure of a key:value pair
    public class JKeyValuePair : JItem
    {
        public JItem Key
        {
            get { try { return items[0]; } catch { return new JItem { contents = "ExceptionKey" }; } }
            set
            {
                items.Add(value);
            }
        }
        public JItem Value
        {
            get { try { return items[1]; } catch { return new JItem { contents = "ExceptionValue" }; } }
            set
            {
                items.Add(value);
            }
        }
    }

    public enum ItemType : byte { SingleValue, Array, Object, KeyValue, Root }
}
