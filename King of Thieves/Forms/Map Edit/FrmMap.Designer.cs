namespace King_of_Thieves.Forms.Map_Edit
{
    partial class FrmMap
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapHScroll = new System.Windows.Forms.HScrollBar();
            this.mapVScroll = new System.Windows.Forms.VScrollBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hsbTexture = new System.Windows.Forms.HScrollBar();
            this.vsbTexture = new System.Windows.Forms.VScrollBar();
            this.txvTextures = new WinFormsGraphicsDevice.TextureViewer();
            this.cmbTilesets = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnNewComponent = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbActorList = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.componentContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbLayers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.btnDeleteLayer = new System.Windows.Forms.Button();
            this.btnInsertAfter = new System.Windows.Forms.Button();
            this.mpvMapView = new WinFormsGraphicsDevice.SpinningTriangleControl();
            this.btnNewAnimated = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.componentContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(812, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mapHScroll
            // 
            this.mapHScroll.LargeChange = 16;
            this.mapHScroll.Location = new System.Drawing.Point(292, 526);
            this.mapHScroll.Maximum = 3000;
            this.mapHScroll.Name = "mapHScroll";
            this.mapHScroll.Size = new System.Drawing.Size(500, 17);
            this.mapHScroll.SmallChange = 16;
            this.mapHScroll.TabIndex = 2;
            this.mapHScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // mapVScroll
            // 
            this.mapVScroll.LargeChange = 16;
            this.mapVScroll.Location = new System.Drawing.Point(795, 43);
            this.mapVScroll.Maximum = 3000;
            this.mapVScroll.Name = "mapVScroll";
            this.mapVScroll.Size = new System.Drawing.Size(17, 480);
            this.mapVScroll.SmallChange = 16;
            this.mapVScroll.TabIndex = 3;
            this.mapVScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.mapVScroll_Scroll);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(274, 450);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnNewAnimated);
            this.tabPage1.Controls.Add(this.hsbTexture);
            this.tabPage1.Controls.Add(this.vsbTexture);
            this.tabPage1.Controls.Add(this.txvTextures);
            this.tabPage1.Controls.Add(this.cmbTilesets);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(266, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tiles";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // hsbTexture
            // 
            this.hsbTexture.LargeChange = 1;
            this.hsbTexture.Location = new System.Drawing.Point(5, 262);
            this.hsbTexture.Name = "hsbTexture";
            this.hsbTexture.Size = new System.Drawing.Size(238, 17);
            this.hsbTexture.TabIndex = 5;
            this.hsbTexture.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbTexture_Scroll);
            // 
            // vsbTexture
            // 
            this.vsbTexture.Location = new System.Drawing.Point(246, 3);
            this.vsbTexture.Name = "vsbTexture";
            this.vsbTexture.Size = new System.Drawing.Size(17, 256);
            this.vsbTexture.TabIndex = 5;
            this.vsbTexture.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbTexture_Scroll);
            // 
            // txvTextures
            // 
            this.txvTextures.BackColor = System.Drawing.Color.Black;
            this.txvTextures.Location = new System.Drawing.Point(3, 3);
            this.txvTextures.Name = "txvTextures";
            this.txvTextures.Size = new System.Drawing.Size(240, 256);
            this.txvTextures.TabIndex = 2;
            this.txvTextures.VSync = false;
            this.txvTextures.Load += new System.EventHandler(this.txvTextures_Load);
            this.txvTextures.Click += new System.EventHandler(this.txvTextures_Click);
            // 
            // cmbTilesets
            // 
            this.cmbTilesets.FormattingEnabled = true;
            this.cmbTilesets.Location = new System.Drawing.Point(3, 282);
            this.cmbTilesets.Name = "cmbTilesets";
            this.cmbTilesets.Size = new System.Drawing.Size(240, 21);
            this.cmbTilesets.TabIndex = 1;
            this.cmbTilesets.SelectedIndexChanged += new System.EventHandler(this.cmbTilesets_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnNewComponent);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cmbActorList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(266, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Components";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnNewComponent
            // 
            this.btnNewComponent.Location = new System.Drawing.Point(11, 54);
            this.btnNewComponent.Name = "btnNewComponent";
            this.btnNewComponent.Size = new System.Drawing.Size(249, 23);
            this.btnNewComponent.TabIndex = 2;
            this.btnNewComponent.Text = "Create New Component";
            this.btnNewComponent.UseVisualStyleBackColor = true;
            this.btnNewComponent.Click += new System.EventHandler(this.btnNewComponent_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Actors";
            // 
            // cmbActorList
            // 
            this.cmbActorList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActorList.FormattingEnabled = true;
            this.cmbActorList.Location = new System.Drawing.Point(51, 20);
            this.cmbActorList.Name = "cmbActorList";
            this.cmbActorList.Size = new System.Drawing.Size(209, 21);
            this.cmbActorList.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(266, 424);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Hitboxes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(261, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Only the hitboxes of the current layer will be displayed.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "followed by the bottom right position.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Drop a hitbox by clicking the top left position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "You are now in hitbox mode.";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // componentContextMenu
            // 
            this.componentContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.componentContextMenu.Name = "componentContextMenu";
            this.componentContextMenu.Size = new System.Drawing.Size(128, 70);
            this.componentContextMenu.Text = "Component";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // cmbLayers
            // 
            this.cmbLayers.FormattingEnabled = true;
            this.cmbLayers.Items.AddRange(new object[] {
            "root"});
            this.cmbLayers.Location = new System.Drawing.Point(45, 494);
            this.cmbLayers.Name = "cmbLayers";
            this.cmbLayers.Size = new System.Drawing.Size(124, 21);
            this.cmbLayers.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 496);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Layer";
            // 
            // btnAddLayer
            // 
            this.btnAddLayer.Location = new System.Drawing.Point(175, 494);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(37, 23);
            this.btnAddLayer.TabIndex = 7;
            this.btnAddLayer.Text = "+";
            this.btnAddLayer.UseVisualStyleBackColor = true;
            this.btnAddLayer.Click += new System.EventHandler(this.btnAddLayer_Click);
            // 
            // btnDeleteLayer
            // 
            this.btnDeleteLayer.Location = new System.Drawing.Point(218, 494);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new System.Drawing.Size(37, 23);
            this.btnDeleteLayer.TabIndex = 8;
            this.btnDeleteLayer.Text = "-";
            this.btnDeleteLayer.UseVisualStyleBackColor = true;
            this.btnDeleteLayer.Click += new System.EventHandler(this.btnDeleteLayer_Click);
            // 
            // btnInsertAfter
            // 
            this.btnInsertAfter.Location = new System.Drawing.Point(45, 519);
            this.btnInsertAfter.Name = "btnInsertAfter";
            this.btnInsertAfter.Size = new System.Drawing.Size(75, 23);
            this.btnInsertAfter.TabIndex = 9;
            this.btnInsertAfter.Text = "Insert After";
            this.btnInsertAfter.UseVisualStyleBackColor = true;
            this.btnInsertAfter.Click += new System.EventHandler(this.btnInsertAfter_Click);
            // 
            // mpvMapView
            // 
            this.mpvMapView.BackColor = System.Drawing.Color.Black;
            this.mpvMapView.Location = new System.Drawing.Point(280, 43);
            this.mpvMapView.Name = "mpvMapView";
            this.mpvMapView.Size = new System.Drawing.Size(512, 480);
            this.mpvMapView.TabIndex = 1;
            this.mpvMapView.VSync = false;
            this.mpvMapView.Load += new System.EventHandler(this.mpvMapView_Load);
            this.mpvMapView.Click += new System.EventHandler(this.mpvMapView_Click);
            // 
            // btnNewAnimated
            // 
            this.btnNewAnimated.Location = new System.Drawing.Point(8, 322);
            this.btnNewAnimated.Name = "btnNewAnimated";
            this.btnNewAnimated.Size = new System.Drawing.Size(243, 23);
            this.btnNewAnimated.TabIndex = 6;
            this.btnNewAnimated.Text = "Create New Animated Tile";
            this.btnNewAnimated.UseVisualStyleBackColor = true;
            this.btnNewAnimated.Click += new System.EventHandler(this.btnNewAnimated_Click);
            // 
            // FrmMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 548);
            this.Controls.Add(this.btnInsertAfter);
            this.Controls.Add(this.btnDeleteLayer);
            this.Controls.Add(this.btnAddLayer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLayers);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mapVScroll);
            this.Controls.Add(this.mapHScroll);
            this.Controls.Add(this.mpvMapView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMap";
            this.Text = "Map Editor";
            this.Load += new System.EventHandler(this.FrmMap_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.componentContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private WinFormsGraphicsDevice.SpinningTriangleControl mpvMapView;
        private System.Windows.Forms.HScrollBar mapHScroll;
        private System.Windows.Forms.VScrollBar mapVScroll;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cmbTilesets;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip componentContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private WinFormsGraphicsDevice.TextureViewer txvTextures;
        private System.Windows.Forms.HScrollBar hsbTexture;
        private System.Windows.Forms.VScrollBar vsbTexture;
        private System.Windows.Forms.ComboBox cmbLayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddLayer;
        private System.Windows.Forms.Button btnDeleteLayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbActorList;
        private System.Windows.Forms.Button btnNewComponent;
        private System.Windows.Forms.Button btnInsertAfter;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNewAnimated;
    }
}