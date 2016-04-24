using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    class CLayer : IDisposable
    {
        private ComponentManager _components;
        public string NAME;
        private int _width, _height; //PIXELS!!!
        private Graphics.CSprite _image;
        public Dictionary<string, Graphics.CSprite> otherImages = new Dictionary<string, Graphics.CSprite>();
        private double _mapVersion;
        private Graphics.CDrawList _drawlist = new Graphics.CDrawList();
        private int _layerIndex;
        private int _hitboxAddress = Actors.CReservedAddresses.HITBOX_NOT_PRESENT;
        private List<CTile> _tilesOnScreen = new List<CTile>(204);
        private Vector2 _imageVector = Vector2.Zero;

        private List<CTile> _tiles = new List<CTile>(); //raw tile data

        public CLayer(ref Graphics.CSprite image)
        {
            _image = image;

            if (_image != null)
                _imageVector = new Vector2(Graphics.CTextures.textures[_image.atlasName].FrameWidth, Graphics.CTextures.textures[_image.atlasName].FrameHeight);
        }

        public CLayer(Dictionary<string, Graphics.CSprite> atlasCache)
        {
            otherImages = atlasCache;
            _components = new ComponentManager(new ComponentFactory[] { new ComponentFactory(new List<Actors.CComponent>().ToArray()) });
        }

        public CLayer()
        {
        }

        
        public CLayer(string name, Actors.CComponent[] components, CTile[] tiles, ref Graphics.CSprite image, int index, double version = 1, int hitBoxAddress = Actors.CReservedAddresses.HITBOX_NOT_PRESENT)
        {
            _width = 0; _height = 0;
            NAME = name;
            _tiles.AddRange(tiles);
            _image = image;
            _components = new ComponentManager(new ComponentFactory[]{ new ComponentFactory(components) } );
            _mapVersion = version;
            _layerIndex = index;
            _hitboxAddress = hitBoxAddress;

            if(_image != null)
                _imageVector = new Vector2(Graphics.CTextures.textures[_image.atlasName].FrameWidth, Graphics.CTextures.textures[_image.atlasName].FrameHeight);
        }

        ~CLayer()
        {
             _image = null;
        }

        public void tileCoordConverter()
        {
            for(int i = 0; i < _tiles.Count; i++)
            {
                CTile tile = _tiles[i];
                tile.tileCoords.X *= 16;
                tile.tileCoords.Y *= 16;
            }
        }

        public void swapDrawDepth(int newDepth, Actors.CActor sprite)
        {
            _drawlist.changeSpriteDepth(sprite, sprite.drawDepth, newDepth);
        }

        public int hitboxAddress
        {
            get
            {
                return _hitboxAddress;
            }
            set
            {
                _hitboxAddress = value;
            }
        }

        public void removeFromDrawList(Actors.CActor actor)
        {
            _drawlist.removeSpriteFromDrawList(actor);
        }

        public void addToDrawList(Actors.CActor actor)
        {
            _drawlist.addSpriteToList(actor.drawDepth, actor);
        }

        public void addToDrawList(List<Actors.CActor> actors)
        {
            _drawlist.addSpriteToList(actors.ToArray());
        }

        public Dictionary<int, List<string>> getActorHeaderInfo()
        {
            if (_components == null)
                return null;

            return _components.actorHeaderMap();
        }

        public CTile getTileInfo(int index)
        {
            CTile temp = _tiles[index] as CAnimatedTile;

            if (temp == null)
                return new CTile(_tiles[index].atlasCoords, _tiles[index].tileCoords, _tiles[index].tileSet);
            else
            {
                CAnimatedTile anim = (CAnimatedTile)temp;
                return new CAnimatedTile(anim.startingPosition, anim.atlasCoordsEnd, anim.tileCoords, anim.tileSet, anim.speed);
            }
        }

        public int numberOfTiles
        {
            get
            {
                return _tiles.Count();
            }
        }

        public void addComponent(Actors.CComponent component)
        {
            _components.addComponent(component);
        }

        public void removeComponent(Actors.CComponent component)
        {
            _components.removeComponent(component);
        }

        public void addDrop(Actors.Items.Drops.CDroppable drop)
        {
            //_components.
        }

        public void initComponents(Actors.CComponent component)
        {
            if (_components == null)
            {
                Actors.CComponent[] temp = new Actors.CComponent[] { component };
                _components = new ComponentManager(new ComponentFactory[] { new ComponentFactory(temp) });
            }
            else
                throw new InvalidOperationException("Cannot reinitialize a layer's components once they have been initialized.");
        }
        public int componentCount
        {
            get
            {
                return _components.componentCount;
            }
        }
        //returns the first occurance
        public int indexOfTile(Vector2 coords)
        {
            int outPut = -1;

            for (int i = 0; i < _tiles.Count; i++)
            {
                if (_tiles[i].checkForClick(coords))
                    return i;
            }

            return outPut;
        }

        //returns the last occurance
        public int indexOfTileReverse(Vector2 coords)
        {
            int outPut = -1;

            for (int i = _tiles.Count - 1; i >=0; i--)
            {
                if (_tiles[i].checkForClick(coords))
                    return i;
            }

            return outPut;
        }

        public void updateTileSet(Graphics.CSprite newSet)
        {
            foreach (CTile tile in _tiles)
            {
                if (string.IsNullOrEmpty(tile.tileSet))
                    tile.tileSet = _image.atlasName;
            }

            _image = newSet;
        }

        public void setName(string name)
        {
            NAME = name;
        }

        public void addTile(CTile tile)
        {
            _tiles.Add(tile);
        }

        public void removeTile(int index)
        {
            if (index == -1)
                return;

            _tiles.RemoveAt(index);
        }

        public void updateLayer(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //components
            _components.Update(gameTime);
            //update tiles
            for (int i = 0; i < _tiles.Count; i++)
            {
                CTile tile = _tiles[i];
                Vector2 dimensions = Vector2.Zero;

                //get tileset info
                if (string.IsNullOrEmpty(tile.tileSet))
                    dimensions = _imageVector;
                else
                    dimensions = tile.dimensions;

                tile.shouldDraw = CMasterControl.buttonController.checkCullBoundary(tile.tileCoords, dimensions);

                if (tile.shouldDraw)
                    tile.update();
            }
        }

        public void drawLayer(SpriteBatch spriteBatch = null)
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                CTile tile = _tiles[i];
                if (spriteBatch == null && !tile.shouldDraw)
                    continue;

                otherImages[tile.tileSet].draw((int)(tile.tileCoords.X), (int)(tile.tileCoords.Y), (int)(tile.atlasCoords.X), (int)(tile.atlasCoords.Y), 1, 1, true, spriteBatch);
            }

            if (_components != null)
                _drawlist.drawAll(_layerIndex,spriteBatch);

            _tilesOnScreen.Clear();
        }

        public int width
        {
            get
            {
                return _width;
            }
        }

        public int height
        {
            get
            {
                return _height;
            }
        }

        public void addImage(Graphics.CSprite image)
        {
            _image = image;
        }

        public void Dispose()
        {
            foreach (CTile tile in _tiles)
                tile.Dispose();

            _components.disposeFactories();

            otherImages.Clear();

            _tiles.Clear();
            _tiles = null;
        }
    }
}
