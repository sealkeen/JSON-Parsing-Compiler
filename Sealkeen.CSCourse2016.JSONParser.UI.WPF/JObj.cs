//CREATED BY SILKIN IVAN FOR EPAM TRAINING COURSES ONLY
//THIS PRODUCT DOESN'T COVERED BY ANY LICENSE AND GIVEN YOU
//"AS IS". DESPITE THAT, YOU ARE NOT ALLOWED TO USE THIS
//SIGNATURE IF YOU MODIFIED ANY PART OF THE PROGRAM WITHOUT
//MY PERSONAL AGREEMENT SO BE CAREFUL NOT TO SIGN YOUR
//MODIFICATION USING MY SIGNATURE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

enum JSNParentType { Object, Array, None }

namespace SealkeenJSON
{
    //Class of Any Item in Json (Value, Key, Object, Array)
    public class JItem : IEnumerable
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

        public IEnumerator GetEnumerator()
        {
            foreach(JItem jI in Items)
            {
                yield return jI;
            }
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
        { get { return Items[0]; }
            set
            {
                    Items.Add(value);
            }
        }
        public JItem Value
        {
            get { try { return Items[1]; } catch { return new JItem { contents = "Exception"}; } }
            set
            {
                    Items.Add(value);
            }
        }
    }

    //Special class to create Treeview
    public class JItemNode : TreeNode
    {
        public JItemNode(JItem jitem)
        {
            this.Text = jitem.ToString();
            this.Jitem = jitem;
            this.Jitem.Items.ForEach(x => this.Nodes.Add(new JItemNode(x)));
        }

        public JItem Jitem
        {
            get;
            private set;
        }
    }
}
