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
using Microsoft.Xna.Framework;
using King_of_Thieves.Actors;

namespace King_of_Thieves.Forms.Map_Edit
{
    public partial class FrmMap : Form
    {
		#if WINDOWS
        private enum EDITOR_MODE
        {
            TILE = 0,
            COMPONENT,
            HITBOX
        }

        private Map.CMap _loadedMap;
        private List<string> nameSpaceList = new List<string>();
        private const string TOP_LEVEL = "King_of_Thieves.Actors.";
        private Dictionary<string, Graphics.CSprite> _atlasCache = new Dictionary<string,Graphics.CSprite>();
        private Dictionary<string, string> _actorFullyQualifiedNames = new Dictionary<string, string>();
        private EDITOR_MODE _editorMode = EDITOR_MODE.TILE;
        private FrmNewComponent _newComponent = null;
        King_of_Thieves.Map.CTile tile = null;
        private Vector2 _hitboxTopLeft = new Vector2(-1, -1);
        private int _hitBoxCounter = 0;
        private bool _animationSelectMode = false;
        private Vector2 _animStart = new Vector2(-1, -1);
        private Vector2 _animEnd = new Vector2(-1, -1);
        

        public FrmMap()
        {
            _newComponent = new FrmNewComponent(this);
            InitializeComponent();
        }

		private static string showInputDialog(String title, String prompt, String defaultMessage)
		{
			System.Drawing.Size size = new System.Drawing.Size(200, 100);
			Form inputBox = new Form();

            inputBox.Text = title;
			inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			inputBox.ClientSize = size;
			inputBox.Text = title;

			System.Windows.Forms.TextBox textBox = new TextBox();
			textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
			textBox.Location = new System.Drawing.Point(5, 20);
            textBox.Text = defaultMessage;
			inputBox.Controls.Add(textBox);

            Label label = new Label();
            label.Text = prompt;
            label.Size = new System.Drawing.Size(size.Width - 10, 15);
            label.Location = new System.Drawing.Point(5, 5);
            inputBox.Controls.Add(label);

			Button okButton = new Button();
			okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okButton.Name = "okButton";
			okButton.Size = new System.Drawing.Size(75, 23);
			okButton.Text = "&OK";
			okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 60);
			inputBox.Controls.Add(okButton);

			Button cancelButton = new Button();
			cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelButton.Name = "cancelButton";
			cancelButton.Size = new System.Drawing.Size(75, 23);
			cancelButton.Text = "&Cancel";
			cancelButton.Location = new System.Drawing.Point(size.Width - 80, 60);
			inputBox.Controls.Add(cancelButton);

			inputBox.AcceptButton = okButton;
			inputBox.CancelButton = cancelButton; 

			DialogResult result = inputBox.ShowDialog();
            if (result == DialogResult.OK)
                return textBox.Text;
            else
                return string.Empty;
		}

        private void _newMap()
        {
            _loadedMap = null;
            _loadedMap = new Map.CMap(_atlasCache);
            mpvMapView.map = _loadedMap;
        }

        private void FrmMap_Load(object sender, EventArgs e)
        {
            Graphics.CSprite.initFrameRateMapping();

            foreach (KeyValuePair<string,Graphics.CTextureAtlas> tileset in Graphics.CTextures.textures)
                if (tileset.Value.isTileSet)
                {
                    cmbTilesets.Items.Add(tileset.Key);
                    King_of_Thieves.Graphics.CSprite sprite = txvTextures.changeSprite(tileset.Key, tileset.Value);
                    _atlasCache.Add(tileset.Key, sprite);
                }



            populateNameSpaceList();
            populateActorList("all");

            cmbTilesets.SelectedIndex = 0;
            cmbLayers.SelectedIndex = 0;
            cmbActorList.SelectedIndex = 0;

            _newMap();
            tile = txvTextures.selectTile(hsbTexture.Value, vsbTexture.Value);
           
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
                    stringVal.IndexOf(TOP_LEVEL + "World") == 0 ||
                    stringVal.IndexOf(TOP_LEVEL + "Collision") == 0
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
            {
                string shortName = type.ToString().Substring(type.ToString().LastIndexOf('.') + 1);
                cmbActorList.Items.Add(shortName);
                _actorFullyQualifiedNames.Add(shortName, type.ToString());
            }

            cmbActorList.Sorted = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmbTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {
            King_of_Thieves.Graphics.CSprite sprite = txvTextures.changeSprite(cmbTilesets.Text, Graphics.CTextures.textures[cmbTilesets.Text]);
            mpvMapView.changeCurrentTileSet(sprite);
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
            tile = txvTextures.selectTile(hsbTexture.Value, vsbTexture.Value);

            if (_animationSelectMode)
            {
                if (_animStart.X == -1)
                    _animStart = tile.atlasCoords;
                else
                {
                    _animEnd = tile.atlasCoords;
                    tile = txvTextures.selectAnimatedTile(_animStart, _animEnd);
                    _animationSelectMode = false;
                    btnNewAnimated.BackColor = System.Drawing.Color.Transparent;
                    _animStart = new Vector2(-1, -1);
                    _animEnd = new Vector2(-1, -1);
                }
            }

            mpvMapView.changeSelectedTile(tile);
        }

        private void mpvMapView_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            Vector2 position = mpvMapView.getMouseSnapCoords();
            switch (_editorMode)
            {
                case EDITOR_MODE.TILE:
                    if (args.Button == System.Windows.Forms.MouseButtons.Left)
                        mpvMapView.dropTile(txvTextures.currentTile, _loadedMap.getLayer(cmbLayers.SelectedIndex),mapHScroll.Value,mapVScroll.Value);
                    else if (args.Button == System.Windows.Forms.MouseButtons.Right)
                        mpvMapView.removeTile(_loadedMap.getLayer(cmbLayers.SelectedIndex), position,mapHScroll.Value, mapVScroll.Value);
                    break;

                case EDITOR_MODE.COMPONENT:
                    string actorName = "";
                    string[] parameters = null;
                    _newComponent = new FrmNewComponent(this);
                    if (_newComponent.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        actorName = _newComponent.actorName;
                        parameters = _newComponent.parameters;

                        try
                        {
                            position.X += mapHScroll.Value;
                            position.Y += mapVScroll.Value;
                            mpvMapView.dropActor(_actorFullyQualifiedNames[cmbActorList.Text], actorName, position, cmbLayers.SelectedIndex, parameters);
                        }
                        catch (KotException.KotBadArgumentException ex)
                        {
                            MessageBox.Show("Invalid parameter used for actor.");
                        }
                    }
                    
                    break;

                case EDITOR_MODE.HITBOX:

                    dropHitbox(position, args);

                    break;
            }
        }

        private void dropHitbox(Vector2 position, MouseEventArgs args)
        {
            
            if (_hitboxTopLeft.X == -1 && _hitboxTopLeft.Y == -1)
            {
                if (args.Button == MouseButtons.Right)
                    mpvMapView.deleteHitbox(position, mapHScroll.Value, mapVScroll.Value);
                else
                {
                    position.X += mapHScroll.Value;
                    position.Y += mapVScroll.Value;
                    _hitboxTopLeft = position;
                }
            }
            else
            {
                position.X += mapHScroll.Value;
                position.Y += mapVScroll.Value;
                if (args.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    _hitboxTopLeft = new Vector2(-1, -1);
                    return;
                }
                string hbParams = "";
                King_of_Thieves.Actors.Collision.CSolidTile box = new Actors.Collision.CSolidTile((int)_hitboxTopLeft.X, (int)_hitboxTopLeft.Y, (int)(position.X - _hitboxTopLeft.X), (int)(position.Y - _hitboxTopLeft.Y));
                _hitBoxCounter += 1;


                hbParams = (int)(position.X - _hitboxTopLeft.X) + ":" + (int)(position.Y - _hitboxTopLeft.Y);
                string[] hbParamsArr = hbParams.Split(':');
                mpvMapView.dropActor("King_of_Thieves.Actors.Collision.CSolidTile", "hitbox" + _hitBoxCounter, _hitboxTopLeft, cmbLayers.SelectedIndex, hbParamsArr);
                _hitboxTopLeft = new Vector2(-1, -1);

            }
        }


        private void txvTextures_Load(object sender, EventArgs e)
        {
            
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            Map.CLayer layer = _createNewLayer();

            if (layer != null)
            {
                _loadedMap._layers.Add(layer);
                cmbLayers.Items.Add(layer.NAME);
            }
        }

        private Map.CLayer _createNewLayer()
        {
            string layerName = string.Empty;

            if ((layerName = showInputDialog("Add New Layer", "Please Enter a layer name.", "New Layer" + cmbLayers.Items.Count)) == string.Empty)
                return null;

            if (cmbLayers.Items.Contains(layerName))
            {
                MessageBox.Show("There is already a layer with that name.", "Add New Layer", MessageBoxButtons.OK);
                return null;
            }

            Map.CLayer layer = new Map.CLayer(_atlasCache);
            layer.NAME = layerName;

            return layer;
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this layer?  All tiles, components and hitboxes will be removed.", "Remove Layer", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (cmbLayers.Text == "root")
                {
                    MessageBox.Show("Cannot delete the root layer.");
                    return;
                }

                int index = cmbLayers.SelectedIndex;
                cmbLayers.Items.RemoveAt(index);
                _loadedMap._layers.RemoveAt(index);
                cmbLayers.SelectedIndex = index - 1;
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Tiles")
                _editorMode = EDITOR_MODE.TILE;
            else if (tabControl1.SelectedTab.Text == "Components")
                _editorMode = EDITOR_MODE.COMPONENT;
            else if (tabControl1.SelectedTab.Text == "Hitboxes")
                _editorMode = EDITOR_MODE.HITBOX;
        }

        private void btnNewComponent_Click(object sender, EventArgs e)
        {
            if (_newComponent.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _editorMode = EDITOR_MODE.COMPONENT;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                if (saveFileDialog1.FileName.Trim() != string.Empty)
                {
                    string fileName = saveFileDialog1.FileName;
                    _loadedMap.writeMap(fileName);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                if (openFileDialog1.FileName.Trim() != string.Empty)
                {
                    string fileName = openFileDialog1.FileName;
                    _loadedMap = new Map.CMap(fileName,_atlasCache);
                    _hitBoxCounter = _loadedMap.hitBoxCounter;
                    mpvMapView.map = _loadedMap;
                    cmbLayers.Items.Clear();

                    //load layers
                    foreach(Map.CLayer layer in _loadedMap._layers)
                        cmbLayers.Items.Add(layer.NAME);

                    cmbLayers.SelectedIndex = 0;
                }
            }
        }

        private void mpvMapView_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            mpvMapView.scrollHorizontal(-mapHScroll.Value);
        }

        private void mapVScroll_Scroll(object sender, ScrollEventArgs e)
        {
            mpvMapView.scrollVertical(-mapVScroll.Value);
        }

        private void btnInsertAfter_Click(object sender, EventArgs e)
        {
            Map.CLayer layer = _createNewLayer();
            int insertIndex = cmbLayers.SelectedIndex + 1;

            if (layer != null)
            {
                _loadedMap._layers.Insert(insertIndex, layer);
                cmbLayers.Items.Insert(insertIndex, layer.NAME);
            }
        }

        private void btnNewAnimated_Click(object sender, EventArgs e)
        {
            _animationSelectMode = !_animationSelectMode;

            if (_animationSelectMode)
                btnNewAnimated.BackColor = System.Drawing.Color.Red;
            else
                btnNewAnimated.BackColor = System.Drawing.Color.Transparent;
        }

        private void cmbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Actors.Collision.CSolidTile.currentSelectedLayer = cmbLayers.SelectedIndex;
        }

        private void mpvMapView_MouseMove(object sender, MouseEventArgs e)
        {
            Vector2 mouseCoords = mpvMapView.getMouseSnapCoords();
            mouseCoords.X += mapHScroll.Value;
            mouseCoords.Y += mapVScroll.Value;
            string coordLabel = string.Format("{0,-8}{1,-5}{2,-8}{3,-5}", "Mouse X:", ((int)mouseCoords.X).ToString(), "Mouse Y:", ((int)mouseCoords.Y).ToString());
            label7.Text = coordLabel;

        }
#endif
    }
}
