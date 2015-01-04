using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gears.Cloud;
using Microsoft.VisualBasic;
using System.Reflection;

namespace King_of_Thieves.Forms.Map_Edit
{
    public partial class FrmMap : Form
    {
        private Map.CMap _loadedMap;
        private List<string> nameSpaceList = new List<string>();
        private const string TOP_LEVEL = "King_of_Thieves.Actors.";

        public FrmMap()
        {
            InitializeComponent();
            
        }

        private void _newMap()
        {
            _loadedMap = null;
            _loadedMap = new Map.CMap();
        }

        private void FrmMap_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string,Graphics.CTextureAtlas> tileset in Graphics.CTextures.textures)
                if (tileset.Value.isTileSet)
                    cmbTilesets.Items.Add(tileset.Key);

            cmbTilesets.SelectedIndex = 0;
            cmbLayers.SelectedIndex = 0;

            populateNameSpaceList();
            populateActorList("all");

            _newMap();

           
        }

        private List<String> getNameSpacesByFilter(string filter)
        {
            List<String> results = new List<string>();

            foreach (string nameSpace in nameSpaceList)
                if (nameSpace.IndexOf(TOP_LEVEL + filter) == 0)
                    results.Add(nameSpace);

            return results;
        }

        private void populateNameSpaceList()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var all = assembly.GetTypes().Select(t => t.Namespace).Distinct();
            foreach (var x in all)
            {
                if (x == null || x == string.Empty)
                    continue;

                string stringVal = x.ToString();
                if (stringVal.IndexOf(TOP_LEVEL + "Items") == 0 ||
                    stringVal.IndexOf(TOP_LEVEL + "NPC") == 0 ||
                    stringVal.IndexOf(TOP_LEVEL + "Player") == 0 ||
                    stringVal.IndexOf(TOP_LEVEL + "World") == 0
                    ) nameSpaceList.Add(stringVal);
            }
        }

        private void populateActorList(string filter)
        {
            cmbActorList.Items.Clear();
            List<System.Type> actorList = new List<System.Type>();
            List<String> nameSpaceReference = filter == "all" ? nameSpaceList : getNameSpacesByFilter(filter);

            foreach (string nameSpace in nameSpaceReference)
                actorList.AddRange(Assembly.GetExecutingAssembly().GetTypes().ToList().Where(t => t.Namespace == nameSpace).ToList());

            foreach (System.Type type in actorList)
                cmbActorList.Items.Add(type.ToString().Substring(type.ToString().LastIndexOf('.') + 1));

            cmbActorList.Sorted = true;
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

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            string layerName = Microsoft.VisualBasic.Interaction.InputBox("Please Enter a layer name.", "Add New Layer", "New Layer" + cmbLayers.Items.Count);

            if (cmbLayers.Items.Contains(layerName))
            {
                MessageBox.Show("There is already a layer with that name.", "Add New Layer", MessageBoxButtons.OK);
                return;
            }

            cmbLayers.Items.Add(layerName);

            _loadedMap._layers.Add(new Map.CLayer());

        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            if (cmbLayers.Text == "root")
            {
                MessageBox.Show("Cannot delete the root layer.");
                return;
            }

            int index = cmbLayers.SelectedIndex;
            cmbLayers.Items.RemoveAt(index);
            _loadedMap._layers.RemoveAt(index);
        }
    }
}
