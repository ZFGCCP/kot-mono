namespace King_of_Thieves.Forms.Map_Editor
{
    partial class EditorTiles
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbTexture = new System.Windows.Forms.ComboBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.txtSpacing = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCellSize = new System.Windows.Forms.TextBox();
            this.btnSetMain = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 244);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // cmbTexture
            // 
            this.cmbTexture.FormattingEnabled = true;
            this.cmbTexture.Location = new System.Drawing.Point(0, 0);
            this.cmbTexture.Name = "cmbTexture";
            this.cmbTexture.Size = new System.Drawing.Size(240, 21);
            this.cmbTexture.TabIndex = 2;
            this.cmbTexture.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.LargeChange = 1;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 266);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(240, 17);
            this.hScrollBar1.TabIndex = 3;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(241, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 265);
            this.vScrollBar1.TabIndex = 4;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // txtSpacing
            // 
            this.txtSpacing.Location = new System.Drawing.Point(8, 328);
            this.txtSpacing.Name = "txtSpacing";
            this.txtSpacing.ReadOnly = true;
            this.txtSpacing.Size = new System.Drawing.Size(30, 20);
            this.txtSpacing.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "CellSpacing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cell Size";
            // 
            // txtCellSize
            // 
            this.txtCellSize.Location = new System.Drawing.Point(76, 328);
            this.txtCellSize.Name = "txtCellSize";
            this.txtCellSize.ReadOnly = true;
            this.txtCellSize.Size = new System.Drawing.Size(44, 20);
            this.txtCellSize.TabIndex = 7;
            // 
            // btnSetMain
            // 
            this.btnSetMain.Location = new System.Drawing.Point(7, 286);
            this.btnSetMain.Name = "btnSetMain";
            this.btnSetMain.Size = new System.Drawing.Size(240, 23);
            this.btnSetMain.TabIndex = 9;
            this.btnSetMain.Text = "Make Main Tileset";
            this.btnSetMain.UseVisualStyleBackColor = true;
            this.btnSetMain.Click += new System.EventHandler(this.btnSetMain_Click);
            // 
            // EditorTiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 407);
            this.ControlBox = false;
            this.Controls.Add(this.btnSetMain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCellSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSpacing);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.cmbTexture);
            this.Controls.Add(this.pictureBox1);
            this.Name = "EditorTiles";
            this.Text = "EditorControl";
            this.Load += new System.EventHandler(this.EditorTiles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbTexture;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.TextBox txtSpacing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCellSize;
        private System.Windows.Forms.Button btnSetMain;
    }
}