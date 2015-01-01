using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    class CTexAtlasControl
    {
        public Dictionary<string, CTextureAtlas> atlasBank = new Dictionary<string, CTextureAtlas>();
        private RenderTarget2D _tileMapGen = null;
        private SpriteBatch _tileBatch = null;

        public CTexAtlasControl()
        {
            _tileBatch = new SpriteBatch(CGraphics.GPU);
        }

        ~CTexAtlasControl()
        {
            _tileBatch.Dispose();
            _tileBatch = null;
        }

        public Texture2D generateLayerImage(Map.CLayer layerToRender, Map.CTile[] tileStrip)
        {
            _tileMapGen = new RenderTarget2D(CGraphics.GPU, layerToRender.width, layerToRender.height);

            CGraphics.GPU.SetRenderTarget(_tileMapGen);
            _tileBatch.Begin();
          
            foreach (Map.CTile tile in tileStrip)
                _tileBatch.Draw(atlasBank[tile.tileSet].sourceImage, tile.tileCoords, atlasBank[tile.tileSet].getTile((int)tile.atlasCoords.X, (int)tile.atlasCoords.Y), Color.White);

            _tileBatch.End();
            CGraphics.GPU.SetRenderTarget(null);

            return (_tileMapGen);
        }


    }
}
