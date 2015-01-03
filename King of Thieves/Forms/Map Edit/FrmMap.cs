using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gears.Cloud;

namespace King_of_Thieves.Forms.Map_Edit
{
    public partial class FrmMap : Form
    {
        public FrmMap()
        {
            InitializeComponent();
        }

        private void FrmMap_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string,Graphics.CTextureAtlas> tileset in Graphics.CTextures.textures)
                if (tileset.Value.isTileSet)
                    cmbTilesets.Items.Add(tileset.Key);

            cmbTilesets.SelectedIndex = 0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmbTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {
            txvTextures.changeSprite(cmbTilesets.Text, Graphics.CTextures.textures[cmbTilesets.Text]);

            //check the texture width and height
            if (txvTextures.checkWidth())
            {
                hsbTexture.Minimum = 0;
                hsbTexture.Maximum = txvTextures.atlasWidth - txvTextures.Width;
            }

            if (txvTextures.checkHeight())
            {
                vsbTexture.Minimum = 0;
                vsbTexture.Maximum = txvTextures.atlasHeight - txvTextures.Height;
            }

            hsbTexture.Value = 0;
        }

        private void hsbTexture_Scroll(object sender, ScrollEventArgs e)
        {
            txvTextures.scrollHorizontal(-hsbTexture.Value);
        }

        private void vsbTexture_Scroll(object sender, ScrollEventArgs e)
        {
            txvTextures.scrollVertical(-vsbTexture.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txvTextures_Click(object sender, EventArgs e)
        {
            txvTextures.selectTile(hsbTexture.Value, vsbTexture.Value);
        }

        private void txvTextures_Load(object sender, EventArgs e)
        {
            
        }
    }
}
