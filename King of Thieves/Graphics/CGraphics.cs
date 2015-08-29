using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    static class CGraphics
    {
        private static GraphicsDeviceManager _graphicsInfo;
        public static  SpriteBatch spriteBatch;
        public static RenderTarget2D _rtar2D = null;
        public static RenderTarget2D fullScreenRenderTarget = null;
        public static GraphicsDevice GPU
        {
            get
            {
                return _graphicsInfo.GraphicsDevice;
            }
        }

        public static void acquireGraphics(ref GraphicsDeviceManager manager)
        {
            _graphicsInfo = manager;
        }

        public static void changeResolution(int width, int height)
        {
            Gears.Cloud.ViewportHandler.SetScreen(width, height);
            _graphicsInfo.PreferredBackBufferWidth = width;
            _graphicsInfo.PreferredBackBufferHeight = height;
            fullScreenRenderTarget = new RenderTarget2D(GPU, width, height, true, SurfaceFormat.Color, DepthFormat.Depth24);
            _graphicsInfo.ApplyChanges();
        }

        public static void spawnRenderTarget(ref RenderTarget2D offScreenBuffer)
        {
            _rtar2D = offScreenBuffer;
        }

        public static Texture2D Flip(Texture2D source, bool vertical, bool horizontal)
        {
            Texture2D flipped = new Texture2D(source.GraphicsDevice, source.Width, source.Height);
            Color[] data = new Color[source.Width * source.Height];
            Color[] flippedData = new Color[data.Length];

            source.GetData<Color>(data);

            for (int x = 0; x < source.Width; x++)
                for (int y = 0; y < source.Height; y++)
                {
                    int idx = (horizontal ? source.Width - 1 - x : x) + ((vertical ? source.Height - 1 - y : y) * source.Width);
                    flippedData[x + y * source.Width] = data[idx];
                }

            flipped.SetData<Color>(flippedData);

            return flipped;
        }  
    }
}
