using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    class CTile
    {
        private Vector2 _tileBounds;
        public readonly Vector2 tileCoords;
        public string tileSet;
        private Actors.Collision.CHitBox _boundary = null;


        public CTile(Vector2 atlasCoords, Vector2 mapCoords, string tileSet)
        {
            _tileBounds = atlasCoords;
            tileCoords = mapCoords;
            this.tileSet = tileSet;

            //if tileSet is null, we're probably in game
            if (tileSet != null)
                _boundary = new Actors.Collision.CHitBox(null, mapCoords.X * Graphics.CTextures.textures[tileSet].FrameWidth, mapCoords.Y * Graphics.CTextures.textures[tileSet].FrameHeight, Graphics.CTextures.textures[tileSet].FrameWidth, Graphics.CTextures.textures[tileSet].FrameHeight);
        }

        public Vector2 atlasCoords
        {
            get
            {
                return _tileBounds;
            }
        }

        public Vector2 tileSize
        {
            get
            {
                Vector2 size = new Vector2(_boundary.halfWidth * 2, _boundary.halfHeight * 2);
                return size;
            }
        }

        public bool checkForClick(Vector2 mouseCoords)
        {
            return (_boundary.checkCollision(mouseCoords));
        }
    }
}
