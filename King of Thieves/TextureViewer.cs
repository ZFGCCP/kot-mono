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
        private Rectangle selectorRect = new Rectangle();
        private King_of_Thieves.Map.CTile _currentTile = null;

        public King_of_Thieves.Map.CTile selectTile(int offSetX = 0, int offSetY = 0)
        {
            System.Drawing.Point mousePos = PointToClient(MousePosition);

            int snapX = (int)System.Math.Floor((mousePos.X + offSetX) / 16.0);
            int snapY = (int)System.Math.Floor((mousePos.Y + offSetY) / 16.0);

            selectorRect.X = snapX * 16;
            selectorRect.Y = snapY * 16;

            King_of_Thieves.Map.CTile tile = new King_of_Thieves.Map.CTile(new Vector2(snapX, snapY), Vector2.Zero, _currentSprite.atlasName);
            _currentTile = tile;
            return tile;
        }

        public void scrollVertical(int amount)
        {
            Vector3 translation = Vector3.Zero;
            translation.Y = amount;
            translation.X = _camera.position.X;
            _camera.jump(translation);
        }

        public void scrollHorizontal(int amount)
        {
            Vector3 translation = Vector3.Zero;
            translation.X = amount;
            translation.Y = _camera.position.Y;
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

        public King_of_Thieves.Map.CTile currentTile
        {
            get
            {
                return _currentTile;
            }
        }

        public King_of_Thieves.Graphics.CSprite changeSprite(string atlasName, King_of_Thieves.Graphics.CTextureAtlas newTexture)
        {
            _textureAtlas = newTexture;
            _currentSprite = new King_of_Thieves.Graphics.CSprite(atlasName, newTexture);

            _selector = new Texture2D(GraphicsDevice, 1,1);
            _selector.SetData<Color>(new Color[] { Color.Red });

            return _currentSprite;
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

            selectorRect.X = 0;
            selectorRect.Y = 0;
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
                selectorRect.Width = _textureAtlas.frameWidth;
                selectorRect.Height = _textureAtlas.FrameHeight;
                spriteBatch.Draw(_selector, selectorRect, Color.White);
                spriteBatch.End();
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _camera.update();
        }

        public King_of_Thieves.Graphics.CSprite currentSprite
        {
            get
            {
                return new King_of_Thieves.Graphics.CSprite(_currentSprite);
            }
        }
    }
}
