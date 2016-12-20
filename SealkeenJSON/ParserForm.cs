//CREATED BY SILKIN IVAN FOR EPAM TRAINING COURSES ONLY
//THIS PRODUCT DOESN'T COVERED BY ANY LICENSE AND GIVEN YOU
//"AS IS". DESPITE THAT, YOU ARE NOT ALLOWED TO USE THIS
//SIGNATURE IF YOU MODIFIED ANY PART OF THE PROGRAM WITHOUT
//MY PERSONAL AGREEMENT SO BE CAREFUL NOT TO SIGN YOUR
//MODIFICATION USING MY SIGNATURE.

using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SealkeenParser;

namespace SealkeenJSON
{
  

    public partial class frmTest : Form
    {
        SealkeenParserClass sPC = new SealkeenParserClass();

        JItem currentItem = null;
        //source string

        public frmTest()
        {
            InitializeComponent();
        }


        //Rather testing method
        private bool CheckEmptiness(ref string inBorders)
        {
            if(string.IsNullOrEmpty(inBorders))
            {
                return true;
            }
            return false;
        }

        //Testing method and field
        public string RemoveWhiteSpaces(string str)
        {
            string target;
            target = str.Replace(" ", "");
            target = target.Replace("\n", "");
            target = target.Replace("\t", "");
            target = target.Replace("\r", "");
            return target;
        }
        
        /// <summary>
        /// Event Handlers
        /// </summary>

        private void btnTest_Click(object sender, EventArgs e)
        {

        }

        private void btnTestCasting_Click(object sender, EventArgs e)
        {
        }

        private void btnWhiteSpace_Click(object sender, EventArgs e)
        {
            txtMain.Text = RemoveWhiteSpaces(txtMain.Text);
            txtTest.Text = RemoveWhiteSpaces(txtTest.Text);
            MessageBox.Show(string.Compare(txtMain.Text, txtTest.Text) == 0? "Results Match": "Different Results");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void txtStart_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEnd_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnContinue_Click(object sender, EventArgs e)
        {

        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            
            txtTest.Text = "";
            txtTest.Text += sPC.ToTestString(txtMain.Text);
            currentItem = sPC.GetCurrentItem;
        }

        private void txtMain_TextChanged(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHelp help = new FrmHelp();
            help.Show();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog() { Multiselect = false };
            openFileDialog.ShowDialog();

            StreamReader sR =
                File.Exists(openFileDialog.FileName) ?
                new StreamReader(openFileDialog.OpenFile(), Encoding.Default) :
                null;
            try
            {
                if (sR != null)
                    txtMain.Text = sR.ReadToEnd();
            }
            catch(Exception ex)

            { MessageBox.Show(ex.Message); }
        }

        private void showTree_Click(object sender, EventArgs e)
        {
            try
            {
                if (sPC.GetCurrentItem != null && sPC.GetCurrentItem.Items.Count != 0)
                {
                    Tree tree = new Tree((sPC.GetCurrentItem.Items[0]).Items[0]);
                    tree.Show();
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnDoShit_Click(object sender, EventArgs e)
        {

        }
    }
}
