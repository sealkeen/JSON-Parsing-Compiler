using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.CSCourse2016.ParserPerfTester.Common;

namespace SealkeenParser
{
    public class SealkeenParserClass : IParser
    {
        //Token characters
        public static Stack<char> syntaxChars = new Stack<char>();
        //This stack contains all of the items
        public static Stack<JItem> itemStack = new Stack<JItem>();
        //This stack contains Key : Value pair objects
        public static Stack<JItem> keyValueStack = new Stack<JItem>();

        //Current item we are using to add items in
        private JItem currentItem = new JItem { root = true, isObject = true, isArray = false, Parent = null };
        private JItem currentKV = new JKeyValuePair { };

        public JItem GetCurrentItem
        {
            get { return currentItem; }
        }

        //one of the buffers of a top model
        static string buffer = "";

        //исходная строка
        static string source = "";

        //it's too dangerouse here, run forest
        bool runFromOutside = false;
        //the pentagon secret should have to be inside of the quotes
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
                for (int c = 0; c < source.Length; c++)
                {
                    StartParse();
                }
            }
            catch (Exception ex)
            {
                if(source.Length != indexOfTheChar)
                    Debug.WriteLine($"Unxpected symbol under index {indexOfTheChar}");
            }
        }
         
        public string ToTestString(string str)
        {
            runFromOutside = true;
            source = str;
            InitializeStructure();
            //Perform parsing within the form
            InnerParse();
            FindRoot();

            return currentItem.ToString();
        }

        //Find the meaning of life
        private void FindRoot()
        {
            while (currentItem.Parent != null)
            {
                currentItem = currentItem.Parent;
            }
        }

        //The starting point of fun
        private void InitializeStructure()
        {
            JKeyValuePair newPair = new JKeyValuePair { Parent = null };
            JItem Root = new JItem { root = true, isObject = true, isArray = false, Parent = null };
            keyValueStack.Push(newPair);
            itemStack.Push(Root);

            indexOfTheChar = -1;
            syntaxChars.Clear();
            syntaxChars.Push(',');
            currentItem = new JItem();
            InnerParse();
        }/// <summary>
         /// The Main Big Parsing Method
         /// </summary>
        private void StartParse()
        {
            //index of top secret pentagon data
            ++indexOfTheChar;
            try
            {
                if (syntaxChars?.Peek() == '\"' && insideQuotes)
                {
                    if (source[indexOfTheChar] == '\"')
                    {
                        buffer += source[indexOfTheChar];
                        insideQuotes = false;
                        return;
                    }
                    buffer += source[indexOfTheChar];
                    return;
                }
            }
            catch { /*MessageBox.Show("Дирижабль");*/ }

            switch (source[indexOfTheChar])
            {
                case '{':
                    currentItem = itemStack.Peek();
                    currentKV = keyValueStack.Peek();
                    //Проверка на ожидание инициализации значения Value пары KeyValue 
                    if (pending4Value)
                    {
                        JItem item = new JItem { isObject = true };
                        item.Parent = currentItem;
                        ((JKeyValuePair)currentKV).Value = item;
                        buffer = "";
                        keyValueStack.Push(item);
                        itemStack.Push(item);
                    }
                    else
                    {
                        JItem nItm = new JItem { isObject = true };
                        currentItem.Items.Add(nItm);
                        nItm.Parent = currentItem;
                        itemStack.Push(nItm);
                    }

                    pending4Value = false;
                    break;
                case '}':
                    currentKV = keyValueStack.Peek();
                    currentItem = itemStack.Peek();
                    if (pending4Value)
                    {
                        JItem item = new JItem();
                        item.Parent = currentItem;
                        item.contents = buffer;
                        ((JKeyValuePair)currentKV).Value = item;
                        buffer = "";
                        keyValueStack.Pop();
                    }
                    currentItem = itemStack.Pop();
                    pending4Value = false;
                    break;
                case '[':
                    currentItem = itemStack.Peek();
                    currentKV = keyValueStack.Peek();
                    //Проверка на ожидание инициализации значения Value пары KeyValue 
                    if (pending4Value)
                    {
                        JItem item = new JItem { isObject = false, isArray = true };
                        item.Parent = currentItem;
                        ((JKeyValuePair)currentKV).Value = item;
                        buffer = "";
                        keyValueStack.Push(item);
                        itemStack.Push(item);
                    }
                    else
                    {
                        JItem nItm = new JItem { isObject = false, isArray = true };
                        currentItem.Items.Add(nItm);
                        nItm.Parent = currentItem;
                        itemStack.Push(nItm);
                    }

                    pending4Value = false;
                    break;
                case ']':
                    currentKV = keyValueStack.Peek();
                    currentItem = itemStack.Peek();
                    if (pending4Value)
                    {
                        JItem item = new JItem { singleValue = true };
                        item.Parent = currentItem;
                        item.contents = buffer;
                        ((JKeyValuePair)currentKV).Value = item;
                        buffer = "";
                        keyValueStack.Pop();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(buffer))
                        {
                            currentItem.Items.Add(new JItem { contents = buffer, singleValue = true });
                            buffer = "";
                        }
                    }
                    //currentItem = currentItem.Parent;
                    currentItem = itemStack.Pop();
                    pending4Value = false;
                    break;
                case ':':
                    currentKV = keyValueStack.Peek();
                    currentItem = itemStack.Peek();
                    syntaxChars.Push(':');
                    if (!pending4Value)
                    {
                        JKeyValuePair pair = new JKeyValuePair();
                        pair.Key = new JItem { singleValue = true };
                        pair.Key.contents = buffer;
                        pair.Parent = currentItem;
                        currentItem.Items.Add(pair);
                        keyValueStack.Push(pair);
                    }
                    else
                    {
                        //throw new InvalidEnumArgumentException(); //не было более подходящего, лел
                    }
                    pending4Value = true;
                    buffer = "";
                    break;
                case ',':
                    currentKV = keyValueStack.Peek();
                    try { currentItem = itemStack.Peek(); }
                    catch { }
                    if (pending4Value)
                    {
                        JItem jI = new JItem { singleValue = true, contents = buffer, Parent = currentItem };
                        ((JKeyValuePair)currentKV).Value = jI;
                        keyValueStack.Pop();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(buffer))
                        {
                            currentItem.Items.Add(new JItem { contents = buffer, singleValue = true });
                            buffer = ""; return;
                        }
                    }
                    pending4Value = false;
                    syntaxChars.Push(',');
                    buffer = "";

                    break;
                case '\"':
                    buffer += source[indexOfTheChar];
                    syntaxChars.Push('\"');
                    insideQuotes = !insideQuotes;
                    break;
                case ' ':
                case '\t':
                case '\r':
                case '\n':
                    break;

                default:
                    buffer += source[indexOfTheChar];
                    break;
            }
        }
    }

    public class JItem 
    {
        public List<JItem> Items = new List<JItem>();
        public JItem LastChild { get; set; } = null;
        public JItem Parent { get; set; } = null;
        public string contents;
        public bool isArray;
        public bool root = false;
        public bool singleValue = false;
        public bool isObject = false;
        //public JSNParentType ParentType { get; set; } = JSNParentType.None;

        public override string ToString()
        {
            string str = "";
            if (singleValue)
                str += contents;
            else
            {
                if (isArray)
                {
                    str += '[';
                    try
                    {
                        for (int i = 0; i < Items.Count; ++i)
                        {
                            if (Items[i].singleValue)
                            {
                                str += $"{Items[i].contents}";
                            }
                            else
                            {
                                if (Items[i].isObject)
                                {
                                    str += "{";
                                    str += (Items[i].ToString());
                                    //str += ((Items.Count - 1 == i) ? "" : ",");
                                    str += "}";
                                }
                                else
                                    if (Items[i].isArray)
                                {
                                    str += "[";
                                    str += (Items[i].ToString());
                                    str += "]";
                                }
                            }
                            str += ((Items.Count - 1 == i) ? "" : ",");
                        }
                    }
                    catch { }
                    str += ']';
                }
                else
                {
                    if (Items.Count == 0)
                    {
                        str += contents;
                    }
                    else
                    {
                        if (this is JKeyValuePair)
                        {
                            if ((this as JKeyValuePair).Value.isObject)
                                str += $"{(this as JKeyValuePair).Key.ToString()}:{{{(this as JKeyValuePair).Value.ToString()}}}";
                            else
                            {
                                if ((this as JKeyValuePair).Value.isArray)
                                    str += $"{(this as JKeyValuePair).Key.ToString()}:{(this as JKeyValuePair).Value.ToString()}";
                                else
                                {
                                    str += $"{(this as JKeyValuePair).Key.ToString()}:{(this as JKeyValuePair).Value.ToString()}";
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < Items.Count; ++i)
                            {
                                if ((Items[i].isObject) && !Items[i].isArray)
                                    str += "{" + Items[i].ToString() + "}";
                                else
                                    str += Items[i].ToString();

                                str += ((Items.Count - 1 == i) ? "" : ",");
                            }
                        }
                    }
                }
            }
            return str;
        }

        public void ToStructure()
        {
            string str = "";

        }
    }

    //Class of Key/Value Pair for JSON
    public class JKeyValuePair : JItem
    {
        public JItem Key
        {
            get { return Items[0]; }
            set
            {
                Items.Add(value);
            }
        }
        public JItem Value
        {
            get { try { return Items[1]; } catch { return new JItem { contents = "Exception" }; } }
            set
            {
                Items.Add(value);
            }
        }
    }
}
