//CREATED BY SILKIN IVAN FOR EPAM TRAINING COURSES ONLY
//THIS PRODUCT IS NOT COVERED BY ANY LICENSE AND GIVEN YOU
//"AS IS". DESPITE THAT, YOU ARE NOT ALLOWED TO USE THIS
//SIGNATURE IF YOU MODIFIED ANY PART OF THE PROGRAM WITHOUT
//MY PERSONAL AGREEMENT SO BE CAREFUL NOT TO SIGN YOUR
//MODIFICATION USING MY SIGNATURE, IT WOULD BE UNFAIR!
//                2016 EPAM TRAINING.

using System.Windows.Forms;
using System.Collections.Generic;
using Sealkeen.CSCourse2016.JSONParser.Core;
using System.Linq;
using System.Xml.Linq;

namespace Sealkeen.CSCourse2016.JSONParser.UI.WPF
{
    public partial class Tree : Form
    {
        //Root item for the structure
        JItem JRoot;
        public Tree(JItem root)
        {
            JRoot = root;
            InitializeComponent();
            treeView1.Nodes.Add(new JItemNode(JRoot));
        }
    }
    //Special class to create Treeview
    public class JItemNode : TreeNode
    {
        static int maxNodes = 750000;
        static int nodeCount = 0;
        static bool warningShown = false;
        static bool skip = false;
        public JItemNode(JItem jitem)
        {
            if (skip) return;
            nodeCount++;
            if ((nodeCount % maxNodes) == 0) {
                //if (!warningShown) {
                    warningShown = true;
                    var answer = MessageBox.Show($"{nodeCount} Nodes capacity reached. You may run out of RAM so further Parsing should disabled. Start loading tree right now?", "Node capacity reached.", MessageBoxButtons.YesNoCancel);

                    if (answer == DialogResult.Yes) {
                        skip = true;
                        return;
                    }
                //}
            }

            this.Text = AsTypeString(jitem);
                //jitem.Type == JItemType.KeyValue ?
                //$"{jitem.GetKey()} : { AsTypeString(jitem.GetPairedValue()) } " :
                //    (jitem.Type == JItemType.Object || jitem.Type == JItemType.Root || jitem.Type == JItemType.Collection) ?
                //        $"{jitem.Type}: {{ {jitem.Items.Count} }}" :
                //        (jitem.Type == JItemType.Array ?
                //            $"{jitem.Type}: [ {jitem.Items.Count} ]" :
                //                jitem.ToString()
                //    );

            List<JItem> nodes = new List<JItem>();
            jitem.ListAllNodes(ref nodes);
            nodes.ForEach(x => 
                { 
                    if (x.IsCollection()) 
                        this.Nodes.Add(new JItemNode(x));
                }
            );
        }

        static string AsTypeString(JItem item)
        {
             switch (item.Type) {
                case JItemType.Array: 
                    return $"[ {item.Items.Count} ]";
                case JItemType.Object:
                case JItemType.Root:
                    return $"{{ {item.Items.Count} }}";
                case JItemType.KeyValue:
                    return $" {item.GetKey()} : {AsTypeString(item.GetPairedValue())} ";
                //case JItemType.SingleValue:
                //    return " value ";
                //case JItemType.String:
                //    return "\"String\"";
                default: return item.ToString();
            };
        }
    }
}
