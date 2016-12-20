namespace SealkeenJSON
{
    partial class frmTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest));
            this.txtMain = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnTestCasting = new System.Windows.Forms.Button();
            this.txtTest = new System.Windows.Forms.TextBox();
            this.btnWhiteSpace = new System.Windows.Forms.Button();
            this.parserMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnShowTree = new System.Windows.Forms.Button();
            this.btnDoShit = new System.Windows.Forms.Button();
            this.parserMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMain
            // 
            this.txtMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMain.Location = new System.Drawing.Point(12, 40);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(624, 204);
            this.txtMain.TabIndex = 1;
            this.txtMain.Text = "Place your JSON here and press build";
            this.txtMain.TextChanged += new System.EventHandler(this.txtMain_TextChanged);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(642, 431);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(85, 23);
            this.btnTest.TabIndex = 11;
            this.btnTest.Text = "test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuild.Location = new System.Drawing.Point(642, 40);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(85, 23);
            this.btnBuild.TabIndex = 5;
            this.btnBuild.Text = "build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnTestCasting
            // 
            this.btnTestCasting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestCasting.Enabled = false;
            this.btnTestCasting.Location = new System.Drawing.Point(642, 402);
            this.btnTestCasting.Name = "btnTestCasting";
            this.btnTestCasting.Size = new System.Drawing.Size(85, 23);
            this.btnTestCasting.TabIndex = 6;
            this.btnTestCasting.Text = "cast";
            this.btnTestCasting.UseVisualStyleBackColor = true;
            this.btnTestCasting.Click += new System.EventHandler(this.btnTestCasting_Click);
            // 
            // txtTest
            // 
            this.txtTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTest.Location = new System.Drawing.Point(12, 250);
            this.txtTest.Multiline = true;
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(624, 204);
            this.txtTest.TabIndex = 7;
            this.txtTest.Text = "See OutPut Here (Converted From JObj)";
            // 
            // btnWhiteSpace
            // 
            this.btnWhiteSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWhiteSpace.Location = new System.Drawing.Point(642, 69);
            this.btnWhiteSpace.Name = "btnWhiteSpace";
            this.btnWhiteSpace.Size = new System.Drawing.Size(85, 23);
            this.btnWhiteSpace.TabIndex = 8;
            this.btnWhiteSpace.Text = "Whitespaces";
            this.btnWhiteSpace.UseVisualStyleBackColor = true;
            this.btnWhiteSpace.Click += new System.EventHandler(this.btnWhiteSpace_Click);
            // 
            // parserMenu
            // 
            this.parserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.parserMenu.Location = new System.Drawing.Point(0, 0);
            this.parserMenu.Name = "parserMenu";
            this.parserMenu.Size = new System.Drawing.Size(739, 24);
            this.parserMenu.TabIndex = 12;
            this.parserMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.aboutToolStripMenuItem.Text = "How to use guideline";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnShowTree
            // 
            this.btnShowTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowTree.Location = new System.Drawing.Point(642, 98);
            this.btnShowTree.Name = "btnShowTree";
            this.btnShowTree.Size = new System.Drawing.Size(85, 23);
            this.btnShowTree.TabIndex = 13;
            this.btnShowTree.Text = "Show tree";
            this.btnShowTree.UseVisualStyleBackColor = true;
            this.btnShowTree.Click += new System.EventHandler(this.showTree_Click);
            // 
            // btnDoShit
            // 
            this.btnDoShit.Location = new System.Drawing.Point(642, 127);
            this.btnDoShit.Name = "btnDoShit";
            this.btnDoShit.Size = new System.Drawing.Size(85, 23);
            this.btnDoShit.TabIndex = 14;
            this.btnDoShit.Text = "Do shit";
            this.btnDoShit.UseVisualStyleBackColor = true;
            this.btnDoShit.Click += new System.EventHandler(this.btnDoShit_Click);
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 466);
            this.Controls.Add(this.btnDoShit);
            this.Controls.Add(this.btnShowTree);
            this.Controls.Add(this.btnWhiteSpace);
            this.Controls.Add(this.txtTest);
            this.Controls.Add(this.btnTestCasting);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtMain);
            this.Controls.Add(this.parserMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.parserMenu;
            this.MinimumSize = new System.Drawing.Size(755, 505);
            this.Name = "frmTest";
            this.Text = "Sealkeen mega JSON Parser 2.0";
            this.Load += new System.EventHandler(this.frmTest_Load);
            this.parserMenu.ResumeLayout(false);
            this.parserMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnTestCasting;
        private System.Windows.Forms.TextBox txtTest;
        private System.Windows.Forms.Button btnWhiteSpace;
        private System.Windows.Forms.MenuStrip parserMenu;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Button btnShowTree;
        private System.Windows.Forms.Button btnDoShit;
    }
}