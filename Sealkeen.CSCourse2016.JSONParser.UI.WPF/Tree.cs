//CREATED BY SILKIN IVAN FOR EPAM TRAINING COURSES ONLY
//THIS PRODUCT IS NOT COVERED BY ANY LICENSE AND GIVEN YOU
//"AS IS". DESPITE THAT, YOU ARE NOT ALLOWED TO USE THIS
//SIGNATURE IF YOU MODIFIED ANY PART OF THE PROGRAM WITHOUT
//MY PERSONAL AGREEMENT SO BE CAREFUL NOT TO SIGN YOUR
//MODIFICATION USING MY SIGNATURE, IT WOULD BE UNFAIR!
//                2016 EPAM TRAINING.

using System.Windows.Forms;
using System.Collections.Generic;
using EPAM.CSCourse2016.JSONParser.Library;
using System.Linq;

namespace EPAM.CSCourse2016.JSONParser.SealkeenJSON
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
        public JItemNode(JItem jitem)
        {
            this.Text = jitem.ToString();
            this.Jitem = jitem;
            List<JItem> nodes = new List<JItem>();
            jitem.ListAllNodes(ref nodes);
            nodes.ForEach(x => this.Nodes.Add(new JItemNode(x)));
        }

        public JItem Jitem
        {
            get;
            private set;
        }
    }
}
