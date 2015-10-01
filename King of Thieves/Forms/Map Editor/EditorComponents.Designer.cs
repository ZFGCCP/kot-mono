namespace King_of_Thieves.Forms.Map_Editor
{
    partial class EditorComponents
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
            this.btnPlayTest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstActorList = new System.Windows.Forms.ListBox();
            this.cmbActorCategory = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlayTest
            // 
            this.btnPlayTest.Location = new System.Drawing.Point(2, 3);
            this.btnPlayTest.Name = "btnPlayTest";
            this.btnPlayTest.Size = new System.Drawing.Size(278, 23);
            this.btnPlayTest.TabIndex = 0;
            this.btnPlayTest.Text = "Play Test";
            this.btnPlayTest.UseVisualStyleBackColor = true;
            this.btnPlayTest.Click += new System.EventHandler(this.btnPlayTest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstActorList);
            this.groupBox1.Controls.Add(this.cmbActorCategory);
            this.groupBox1.Location = new System.Drawing.Point(2, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 250);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actors";
            // 
            // lstActorList
            // 
            this.lstActorList.FormattingEnabled = true;
            this.lstActorList.Location = new System.Drawing.Point(7, 47);
            this.lstActorList.Name = "lstActorList";
            this.lstActorList.Size = new System.Drawing.Size(263, 134);
            this.lstActorList.TabIndex = 5;
            // 
            // cmbActorCategory
            // 
            this.cmbActorCategory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbActorCategory.FormattingEnabled = true;
            this.cmbActorCategory.Items.AddRange(new object[] {
            "All",
            "Items",
            "NPC",
            "Player",
            "World"});
            this.cmbActorCategory.Location = new System.Drawing.Point(6, 19);
            this.cmbActorCategory.Name = "cmbActorCategory";
            this.cmbActorCategory.Size = new System.Drawing.Size(264, 21);
            this.cmbActorCategory.TabIndex = 4;
            this.cmbActorCategory.SelectedIndexChanged += new System.EventHandler(this.cmbActorCategory_SelectedIndexChanged);
            // 
            // EditorComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 286);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPlayTest);
            this.Name = "EditorComponents";
            this.Text = "EditorComponents";
            this.Load += new System.EventHandler(this.EditorComponents_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlayTest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbActorCategory;
        private System.Windows.Forms.ListBox lstActorList;
    }
}