using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.usr.local
{
    class EditorMode : Gears.Navigation.MenuReadyGameState
    {
        /*private Forms.Map_Editor.EditorComponents _componentEditor = new Forms.Map_Editor.EditorComponents();
        private Forms.Map_Editor.EditorTiles _tileEditor = new Forms.Map_Editor.EditorTiles();
        private Actors.CComponent _controlManager = new Actors.CComponent(0);
        private Rectangle[,] _selectedTiles = new Rectangle[2,2];
        private Graphics.CSprite _currentTileSet = null;

        Map.CLayer[] layers = new Map.CLayer[1];*/
        private Forms.Map_Editor.frmMapEditor editorForm = new Forms.Map_Editor.frmMapEditor();

        public EditorMode()
        {
            editorForm.ShowDialog();
            /*_componentEditor.Visible = true;
            _tileEditor.Visible = true;
            


            _controlManager.root = new Actors.Controllers.CEditorInputController();
            _controlManager.actors.Add("btnNew", new Actors.Controllers.CEditorNew());
            _controlManager.actors.Add("btnOpen", new Actors.Controllers.CEditorOpen());
            _controlManager.actors.Add("btnSave", new Actors.Controllers.CEditorSave());

            

            CMasterControl.commNet.Add((int)_controlManager.address, new List<Actors.CActorPacket>());

            layers = new Map.CLayer[1];
            Graphics.CSprite startingTexture = new Graphics.CSprite(_tileEditor.defaultTileSet, Graphics.CTextures.textures[_tileEditor.defaultTileSet]);
            layers[0] = new Map.CLayer(ref startingTexture);*/
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            /*float cursorX = (float)Math.Floor(((double)((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseX / Graphics.CTextures.textures[_tileEditor.defaultTileSet].FrameWidth)) * 
                                                        Graphics.CTextures.textures[_tileEditor.defaultTileSet].FrameWidth);

            float cursorY = (float)Math.Floor(((double)((Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput).mouseY / Graphics.CTextures.textures[_tileEditor.defaultTileSet].FrameHeight)) * 
                                                        Graphics.CTextures.textures[_tileEditor.defaultTileSet].FrameHeight);

            
            //draw the tiles
            layers[0].drawLayer(true);

            Vector2 cursorTile = new Vector2(cursorX, cursorY);
            //draw the selected tiles
            Vector2 sampleTileSize = new Vector2(_selectedTiles[0, 0].Width, _selectedTiles[0, 0].Height);
            for (int i = 0; i < _selectedTiles.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < _selectedTiles.GetUpperBound(1) + 1; j++)
                {
                    spriteBatch.Draw(_tileEditor.sourceSet, cursorTile + new Vector2(i * sampleTileSize.X, j * sampleTileSize.Y), _selectedTiles[i, j], Color.White);
                }

            _controlManager.Draw(spriteBatch);*/

            
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            /*_controlManager.Update(gameTime);
            Input.CInput input = Gears.Cloud.Master.GetInputManager().GetCurrentInputHandler() as Input.CInput;

            if (_tileEditor.selectorChange)
                _currentTileSet = new Graphics.CSprite(_tileEditor.Controls["cmbTexture"].Text, Graphics.CTextures.textures[_tileEditor.Controls["cmbTexture"].Text]);

            if (((Actors.Controllers.CEditorInputController)_controlManager.root)._shutDown)
            {
                _componentEditor.Visible = false;
                _tileEditor.Visible = false;
                Gears.Cloud.Master.Pop();
            }

            _selectedTiles = _tileEditor.tileRect;

            if (((Actors.Controllers.CEditorNew)_controlManager.actors["btnNew"]).createNew)
            {
                layers = null;
                layers = new Map.CLayer[1];
                Graphics.CSprite startingTexture = new Graphics.CSprite(_tileEditor.defaultTileSet, Graphics.CTextures.textures[_tileEditor.defaultTileSet]);
                layers[0] = new Map.CLayer(ref startingTexture);

            }

            if (_tileEditor.tileSetChanged)
            {
                //apply the current tileSet to all tiles that use it
                foreach (Map.CLayer layer in layers)
                {
                    layer.updateTileSet(_currentTileSet);
                }
            }

            if (((Actors.Controllers.CEditorInputController)_controlManager.root).dropTile)
            {
                
                Vector2 mouseCoords = new Vector2(input.mouseX, input.mouseY);
                string tileSet = "";

                if (mouseCoords.Y <= 32)
                    return;

                //if (_tileEditor.Controls["cmbTexture"].Text == _tileEditor.defaultTileSet)
                    //tileSet = null;
                //else
                    tileSet = _tileEditor.Controls["cmbTexture"].Text;

                Map.CTile[,] temp = new Map.CTile[_selectedTiles.GetUpperBound(0) + 1, _selectedTiles.GetUpperBound(1) + 1];

                for (int i = 0; i < _selectedTiles.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < _selectedTiles.GetUpperBound(1) + 1; j++)
                    {
                        if (_selectedTiles[i, j].Width == 0 || _selectedTiles[i, j].Height == 0)
                            continue;

                        Vector2 atlasCoords = new Vector2(_selectedTiles[i, j].X / _selectedTiles[i, j].Width, _selectedTiles[i, j].Y / _selectedTiles[i, j].Height);

                        float snapX = (float)Math.Floor(mouseCoords.X / _selectedTiles[i, j].Width), snapY = (float)Math.Floor(mouseCoords.Y / _selectedTiles[i, j].Height);

                        mouseCoords.X = snapX;
                        mouseCoords.Y = snapY;

                        temp[i, j] = new Map.CTile(atlasCoords, mouseCoords, tileSet);
                        layers[0].addTile(temp[i, j]);
                    }
                }



                if (!layers[0].otherImages.ContainsKey(_currentTileSet.atlasName))
                    layers[0].otherImages.Add(_currentTileSet.atlasName, _currentTileSet);

                
            }

            if (input.mouseRightClick)
            {
                Vector2 mouseCoords = new Vector2(input.mouseX, input.mouseY);
                int index = 0;

                if ((index = layers[0].indexOfTile(mouseCoords)) > -1)
                    layers[0].removeTile(index);

            }

            if (((Actors.Controllers.CEditorSave)_controlManager.actors["btnSave"]).saveFile)
            {
                Map.CMap saveMe = new Map.CMap(layers);

                saveMe.writeMap(((Actors.Controllers.CEditorSave)_controlManager.actors["btnSave"]).fileName);
            }

            if (((Actors.Controllers.CEditorOpen)_controlManager.actors["btnOpen"]).openFile)
            {
                Map.CMap openMe = new Map.CMap(((Actors.Controllers.CEditorOpen)_controlManager.actors["btnOpen"]).fileName);

                layers = null;
                layers = openMe._layers;
            }*/

            
            
        }
    }
}
