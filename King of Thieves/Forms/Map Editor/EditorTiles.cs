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
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Forms.Map_Editor
{
    public partial class EditorTiles : Form
    {
      
        bool changedTileSet = false;
        bool multiSelect = false;
        bool selectorChanged = false;
        Image currentTile = null;
        static Image cursor;
        int cellSpacing = 0;
        string mainTileSet = "";
        string previousTileSet = "";
        Microsoft.Xna.Framework.Rectangle[,] selectedTile = new Microsoft.Xna.Framework.Rectangle[1, 1];
        Texture2D sourceTileSet = null;
        Vector2 cellSize = Vector2.Zero;
        Vector2 cursorCoords = Vector2.Zero;
        Vector2[] multiSelections = new Vector2[2];
        Vector2 topLeft = Vector2.Zero;
        Vector2 bottomRight = Vector2.Zero;

        public EditorTiles()
        {
            InitializeComponent();
        }

        private void EditorTiles_Load(object sender, EventArgs e)
        {
            //load in tiles
            foreach (KeyValuePair<string, Graphics.CTextureAtlas> kvp in Graphics.CTextures.textures)
            {
                if (kvp.Value.isTileSet)
                    cmbTexture.Items.Add(kvp.Key);
            }

            cmbTexture.SelectedIndex = 0;
            
        }

        public string defaultTileSet
        {
            get
            {
                return mainTileSet;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectorChanged = true;
            //MemoryStream imageStream = new MemoryStream();
            sourceTileSet = Graphics.CTextures.textures[cmbTexture.Text].sourceImage;

            mainTileSet = cmbTexture.Text;
            MemoryStream imageStream = new MemoryStream();
            Texture2D source = CMasterControl.glblContent.Load<Texture2D>("cursorIcon");

            source.SaveAsPng(imageStream, source.Width, source.Height);

            cursor = Image.FromStream(imageStream);

            imageStream.Close();

            //set spacing
            cellSpacing = Graphics.CTextures.textures[cmbTexture.Text].CellSpacing;
            txtSpacing.Text = Graphics.CTextures.textures[cmbTexture.Text].CellSpacing.ToString();
            txtCellSize.Text = Graphics.CTextures.textures[cmbTexture.Text].FrameWidth + "," + Graphics.CTextures.textures[cmbTexture.Text].FrameHeight;
            cellSize = new Vector2(Graphics.CTextures.textures[cmbTexture.Text].FrameWidth, Graphics.CTextures.textures[cmbTexture.Text].FrameHeight);

            if (sourceTileSet.Width > 240)
            {
                hScrollBar1.Enabled = true;
                hScrollBar1.Maximum = sourceTileSet.Width - 240;
                hScrollBar1.Minimum = 0;
            }

            if (sourceTileSet.Height > 244)
            {
                vScrollBar1.Enabled = true;
                vScrollBar1.Maximum = sourceTileSet.Height - 244;
                vScrollBar1.Minimum = 0;
            }

            sourceTileSet.SaveAsPng(imageStream, sourceTileSet.Width, sourceTileSet.Height);

            currentTile = Image.FromStream(imageStream);

            imageStream.Close();
            imageStream = null;

            pictureBox1.Invalidate();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                cursorCoords.Y -= 1;
            else if (e.NewValue < e.OldValue)
                cursorCoords.Y += 1;

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(currentTile, new System.Drawing.Point(-1 * hScrollBar1.Value, -1 * vScrollBar1.Value));
            e.Graphics.DrawImage(cursor, new RectangleF(topLeft.X, topLeft.Y, cellSize.X  * (selectedTile.GetUpperBound(0) + 1), cellSize.Y * (selectedTile.GetUpperBound(1) + 1)));
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                cursorCoords.X -= 1;
            else if (e.NewValue < e.OldValue)
                cursorCoords.X += 1;

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Drawing.Point clientPoint = pictureBox1.PointToClient(MousePosition);
            cursorCoords = new Vector2((int)clientPoint.X, (int)clientPoint.Y);

            //snap the coordinates to the grid
            float snapX = (float)Math.Floor(cursorCoords.X / cellSize.X), snapY = (float)Math.Floor(cursorCoords.Y / cellSize.Y);

            cursorCoords.X = (snapX * cellSize.X) + (snapX * cellSpacing);
            cursorCoords.Y = (snapY * cellSize.Y) + (snapY * cellSpacing);
            topLeft = cursorCoords;

            selectedTile = new Microsoft.Xna.Framework.Rectangle[1, 1];
            selectedTile[0,0] = new Microsoft.Xna.Framework.Rectangle((int)cursorCoords.X + hScrollBar1.Value, (int)cursorCoords.Y + vScrollBar1.Value, (int)cellSize.X, (int)cellSize.Y);

            Input.CInput input = Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput;

            if (!multiSelect && input.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {
                multiSelect = true;

                multiSelections[0] = cursorCoords;
                btnSetMain.Text = "Cancel Multi Select";
                btnSetMain.BackColor = System.Drawing.Color.Red;
            }
            else if (multiSelect)
            {
                multiSelect = false;

                btnSetMain.Text = "Make Main Tileset;";
                btnSetMain.BackColor = System.Drawing.Color.White;

                multiSelections[1] = cursorCoords;

                //do some math here to determine which is the top left of the selection
                _determineTopLeft();
                Microsoft.Xna.Framework.Rectangle selectionRect = _buildSelectionRect();
                
                //build the selection
                int selectionWidth = (int)(selectionRect.Width / cellSize.X);
                int selectionHeight = (int)(selectionRect.Height / cellSize.Y);

                selectedTile = new Microsoft.Xna.Framework.Rectangle[selectionWidth, selectionHeight];

                for (int i = 0; i < selectionWidth; i++)
                    for (int j = 0; j < selectionHeight; j++)
                    {
                        selectedTile[i, j] = new Microsoft.Xna.Framework.Rectangle(selectionRect.X + i * (int)cellSize.X, selectionRect.Y + j * (int)cellSize.Y, (int)cellSize.X, (int)cellSize.Y);
                    }

            }

            pictureBox1.Invalidate();
        }

        private Microsoft.Xna.Framework.Rectangle _buildSelectionRect()
        {
            Microsoft.Xna.Framework.Rectangle output;

            output = new Microsoft.Xna.Framework.Rectangle((int)topLeft.X, (int)topLeft.Y, (int)(bottomRight.X - topLeft.X), (int)(bottomRight.Y - topLeft.Y));
            return output;
        }

        private void _determineTopLeft()
        {
            Vector2 smaller = multiSelections[0]; //start by assuming 0 is the smaller

            if (multiSelections[0].X < multiSelections[1].X && multiSelections[0].Y < multiSelections[1].Y) //0 is the smaller, leave it alone
            {
                topLeft = multiSelections[0];
                bottomRight = multiSelections[1];
            }
            else if (multiSelections[0].X < multiSelections[1].X)
            {
                topLeft = new Vector2(multiSelections[0].X, multiSelections[1].Y);
                bottomRight = new Vector2(multiSelections[1].X, multiSelections[0].Y);
            }
            else if (multiSelections[0].Y < multiSelections[1].Y)
            {
                topLeft = new Vector2(multiSelections[1].X, multiSelections[0].Y);
                bottomRight = new Vector2(multiSelections[0].X, multiSelections[1].Y);
            }
            else
            {
                topLeft = multiSelections[1];
                bottomRight = multiSelections[0];
            }

            bottomRight.X += cellSize.X;
            bottomRight.Y += cellSize.Y;

        }

        public Microsoft.Xna.Framework.Rectangle[,] tileRect
        {
            get
            {
                return selectedTile;
            }
        }

        public Texture2D sourceSet
        {
            get
            {
                return sourceTileSet;
            }
        }

        private void btnSetMain_Click(object sender, EventArgs e)
        {
            if (multiSelect)
            {
                btnSetMain.Text = "Make Main Tileset";
                btnSetMain.BackColor = System.Drawing.Color.White;
                multiSelect = false;
            }

            previousTileSet = mainTileSet;
            mainTileSet = cmbTexture.Text;
            changedTileSet = true;

            
        }

        public bool tileSetChanged
        {
            get
            {
                bool temp = changedTileSet;
                changedTileSet = false;
                return temp;
            }
        }

        public string previousDefaultTileSet
        {
            get
            {
                return previousTileSet;
            }
        }

        public bool selectorChange
        {
            get
            {
                bool temp = selectorChanged;
                selectorChanged = false;
                return temp;
            }

        }
    }
}
