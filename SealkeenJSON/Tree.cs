using System.Windows.Forms;

using SealkeenParser;

namespace SealkeenJSON
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
            this.Jitem.Items.ForEach(x => this.Nodes.Add(new JItemNode(x)));
        }

        public JItem Jitem
        {
            get;
            private set;
        }
    }
}
