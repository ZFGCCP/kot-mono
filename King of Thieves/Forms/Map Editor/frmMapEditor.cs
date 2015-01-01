using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Forms.Map_Editor
{
    public partial class frmMapEditor : Form
    {
        public frmMapEditor()
        {
            InitializeComponent();
        }

        private void frmMapEditor_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Graphics.CTextureAtlas> textures in Graphics.CTextures.textures)
                if (textures.Value.isTileSet)
                    cmbTextures.Items.Add(textures.Key);

            cmbTextures.SelectedIndex = 0;
        }

        private void cmbTextures_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemoryStream mem = new MemoryStream();
            Texture2D tex = Graphics.CTextures.textures[cmbTextures.Text].sourceImage;

            try
            {
                tex.SaveAsPng(mem, tex.Width, tex.Height);
                pbxTileset.Image = Image.FromStream(mem, false, false);
                mem.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
    }
}
