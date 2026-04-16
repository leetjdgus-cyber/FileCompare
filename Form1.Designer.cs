namespace FileCompare
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel3 = new Panel();
            btnLeftDir = new Button();
            txtLeftDir = new TextBox();
            btnCopyFromLeft = new Button();
            lblAppName = new Label();
            panel2 = new Panel();
            panel7 = new Panel();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            panel5 = new Panel();
            btnRightDir = new Button();
            txtRightDir = new TextBox();
            panel4 = new Panel();
            lvwRightDir = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            panel6 = new Panel();
            btnCopyFromRight = new Button();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveBorder;
            panel3.Controls.Add(btnLeftDir);
            panel3.Controls.Add(txtLeftDir);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 97);
            panel3.Name = "panel3";
            panel3.Size = new Size(473, 83);
            panel3.TabIndex = 2;
            panel3.Paint += panel3_Paint;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLeftDir.Location = new Point(383, 32);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(75, 29);
            btnLeftDir.TabIndex = 1;
            btnLeftDir.Text = "폴더 선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            btnLeftDir.Click += btnLeftDir_Click;
            // 
            // txtLeftDir
            // 
            txtLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtLeftDir.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtLeftDir.Location = new Point(10, 31);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.Size = new Size(367, 29);
            txtLeftDir.TabIndex = 0;
            // 
            // btnCopyFromLeft
            // 
            btnCopyFromLeft.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopyFromLeft.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromLeft.Location = new Point(381, 37);
            btnCopyFromLeft.Name = "btnCopyFromLeft";
            btnCopyFromLeft.Size = new Size(77, 37);
            btnCopyFromLeft.TabIndex = 0;
            btnCopyFromLeft.Text = ">>>";
            btnCopyFromLeft.UseVisualStyleBackColor = true;
            btnCopyFromLeft.Click += btnCopyFromLeft_Click;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("맑은 고딕", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAppName.ForeColor = Color.Red;
            lblAppName.Location = new Point(3, 24);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(253, 50);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "File Compare";
            // 
            // panel2
            // 
            panel2.Controls.Add(panel7);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(473, 556);
            panel2.TabIndex = 2;
            // 
            // panel7
            // 
            panel7.BackColor = SystemColors.Desktop;
            panel7.Controls.Add(listView1);
            panel7.Dock = DockStyle.Bottom;
            panel7.Location = new Point(0, 180);
            panel7.Name = "panel7";
            panel7.Size = new Size(473, 376);
            panel7.TabIndex = 0;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(10, 6);
            listView1.Name = "listView1";
            listView1.Size = new Size(448, 355);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "이름";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "크기";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "수정일";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(5, 5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel3);
            splitContainer1.Panel1.Controls.Add(panel1);
            splitContainer1.Panel1.Controls.Add(panel2);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel5);
            splitContainer1.Panel2.Controls.Add(panel4);
            splitContainer1.Panel2.Controls.Add(panel6);
            splitContainer1.Size = new Size(962, 556);
            splitContainer1.SplitterDistance = 473;
            splitContainer1.TabIndex = 0;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(btnCopyFromLeft);
            panel1.Controls.Add(lblAppName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(473, 97);
            panel1.TabIndex = 3;
            // 
            // panel5
            // 
            panel5.Controls.Add(btnRightDir);
            panel5.Controls.Add(txtRightDir);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 97);
            panel5.Name = "panel5";
            panel5.Size = new Size(485, 83);
            panel5.TabIndex = 4;
            // 
            // btnRightDir
            // 
            btnRightDir.Location = new Point(391, 31);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(75, 29);
            btnRightDir.TabIndex = 2;
            btnRightDir.Text = "폴더 선택";
            btnRightDir.UseVisualStyleBackColor = true;
            btnRightDir.Click += btnRightDir_Click;
            // 
            // txtRightDir
            // 
            txtRightDir.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtRightDir.Location = new Point(18, 31);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.Size = new Size(367, 29);
            txtRightDir.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ControlDarkDark;
            panel4.Controls.Add(lvwRightDir);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 180);
            panel4.Name = "panel4";
            panel4.Size = new Size(485, 376);
            panel4.TabIndex = 3;
            // 
            // lvwRightDir
            // 
            lvwRightDir.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader6 });
            lvwRightDir.FullRowSelect = true;
            lvwRightDir.GridLines = true;
            lvwRightDir.Location = new Point(18, 6);
            lvwRightDir.Name = "lvwRightDir";
            lvwRightDir.Size = new Size(448, 355);
            lvwRightDir.TabIndex = 1;
            lvwRightDir.UseCompatibleStateImageBehavior = false;
            lvwRightDir.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "이름";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "크기";
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "수정일";
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.AppWorkspace;
            panel6.Controls.Add(btnCopyFromRight);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(485, 97);
            panel6.TabIndex = 3;
            // 
            // btnCopyFromRight
            // 
            btnCopyFromRight.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromRight.Location = new Point(18, 37);
            btnCopyFromRight.Name = "btnCopyFromRight";
            btnCopyFromRight.Size = new Size(77, 37);
            btnCopyFromRight.TabIndex = 1;
            btnCopyFromRight.Text = "<<<";
            btnCopyFromRight.UseVisualStyleBackColor = true;
            btnCopyFromRight.Click += btnCopyFromRight_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 566);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Padding = new Padding(5);
            Text = "File Compare v1.0";
            Load += Form1_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel7.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
        private SplitContainer splitContainer1;
        private Panel panel6;
        private Panel panel1;
        private Button btnCopyFromLeft;
        private Label lblAppName;
        private Button btnLeftDir;
        private TextBox txtLeftDir;
        private Button btnCopyFromRight;
        private ListView lvwRightDir;
        private Panel panel7;
        private ListView listView1;
        private Panel panel5;
        private Button btnRightDir;
        private TextBox txtRightDir;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
    }
}
