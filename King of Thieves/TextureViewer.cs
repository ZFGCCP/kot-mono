#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WinFormsGraphicsDevice
{
    public partial class TextureViewer : GraphicsDeviceControl
    {
        BasicEffect effect;
        Stopwatch timer;
        private SpriteBatch spriteBatch = null;
        private King_of_Thieves.Graphics.CTextureAtlas _textureAtlas = null;
        private King_of_Thieves.Graphics.CSprite _currentSprite = null;
        private King_of_Thieves.Graphics.CCamera _camera = new King_of_Thieves.Graphics.CCamera();
        private Texture2D _selector = null;

        public void scrollHorizontal(int amount)
        {
            Vector3 translation = Vector3.Zero;
            translation.X = amount;
            _camera.jump(translation);
        }

        public int atlasWidth
        {
            get
            {
                return _textureAtlas.frameWidth * _textureAtlas.tileXCount;
            }
        }

        public int atlasHeight
        {
            get
            {
                return _textureAtlas.frameHeight * _textureAtlas.tileYCount;
            }
        }

        public void changeSprite(string atlasName, King_of_Thieves.Graphics.CTextureAtlas newTexture)
        {
            _textureAtlas = newTexture;
            _currentSprite = new King_of_Thieves.Graphics.CSprite(atlasName, newTexture);

            _selector = new Texture2D(GraphicsDevice, 1,1);
            _selector.SetData<Color>(new Color[] { Color.Red });
        }

        public bool checkHeight()
        {
            return atlasHeight > this.Height;
        }

        public bool checkWidth()
        {
            return atlasWidth > this.Width;
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            // Create our effect.
            effect = new BasicEffect(GraphicsDevice);

            effect.VertexColorEnabled = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };

            
        }


        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (_currentSprite != null)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, _camera.transformation);
                _currentSprite.drawAsTileset(0, 0, spriteBatch);
                Rectangle rect = new Rectangle();
                rect.X = 0;
                rect.Y = 0;
                rect.Width = _textureAtlas.frameWidth;
                rect.Height = _textureAtlas.FrameHeight;
                spriteBatch.Draw(_selector, rect, Color.White);
                spriteBatch.End();
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _camera.update();
        }
    }
}
