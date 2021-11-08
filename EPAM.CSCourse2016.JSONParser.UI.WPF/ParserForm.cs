//CREATED BY SILKIN IVAN FOR EPAM TRAINING COURSES ONLY
//THIS PRODUCT IS NOT COVERED BY ANY LICENSE AND GIVEN YOU
//"AS IS". DESPITE THAT, YOU ARE NOT ALLOWED TO USE THIS
//SIGNATURE IF YOU MODIFIED ANY PART OF THE PROGRAM WITHOUT
//MY PERSONAL AGREEMENT SO BE CAREFUL NOT TO SIGN YOUR
//MODIFICATION USING MY SIGNATURE, IT WOULD BE UNFAIR!
//                2016 EPAM TRAINING.

using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace EPAM.CSCourse2016.JSONParser.SealkeenJSON
{
    public partial class frmTest : Form
    {

        public frmTest()
        {
            InitializeComponent();
        }

        JSONParser sPC = new JSONParser();
        JItem currentItem = null;

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

        //Remove white spaces from both text boxes
        private void btnWhiteSpace_Click(object sender, EventArgs e)
        {
            txtMain.Text = RemoveWhiteSpaces(txtMain.Text);
            txtTest.Text = RemoveWhiteSpaces(txtTest.Text);
            MessageBox.Show(string.Compare(txtMain.Text, txtTest.Text) == 0? "Results Match": "Different Results");
        }

        //Build JSON structure 
        private void btnBuild_Click(object sender, EventArgs e)
        {
            txtTest.Text = "";
            var str = txtMain.Text;
            Stopwatch sW = new Stopwatch();
            sW.Start();
            var target = sPC.ToTestString(str);
            sW.Stop();
            txtTest.AppendText(target);
            currentItem = sPC.GetCurrentItem();
            if (allocated) {
                Console.WriteLine(sW.ElapsedTicks); }
        }

        //Opening File Dialog
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

        //Showing visual tree of the document
        private void showTree_Click(object sender, EventArgs e)
        {
            try
            {
                if (sPC.GetCurrentItem() != null && sPC.GetCurrentItem().HasItems())
                {
                    ThreadStart tS;
                    tS = new ThreadStart(delegate { Application.Run(new Tree((sPC.GetCurrentItem()))); });
                    Thread t = new Thread(tS);
                    t.Start();
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        //Testing control component
        private void checkSpeed_CheckedChanged(object sender, EventArgs e)
        {
            bool checkd = (sender as CheckBox).Checked;
                btnAlloc.Enabled = checkd;
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCredits help = new frmCredits();
            help.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHelp help = new FrmHelp();
            help.Show();
        }
        private void frmTest_Load(object sender, EventArgs e)
        {

        }

        private void txtMain_TextChanged(object sender, EventArgs e)
        {

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

        //WinApi for testing purposes

        //Testing WinApi Console allocation // start
        bool allocated = false;
        private void btnAlloc_Click(object sender, EventArgs e)
        {
            ThreadStart tS = new ThreadStart(testingAlloc);
            Thread t = new Thread(tS);
            t.Start();
        }

        private void testingAlloc()
        {
            if (AllocConsole())
                allocated = true;
            else
                MessageBox.Show("Not allocated");
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Ввод данных");
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMain.Text = "";
            txtTest.Text = "";
        }

        //Testing WinApi Console allocation // end
    }
}
