using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gears.Cloud;

namespace King_of_Thieves.Actors.HUD
{
    class CScreenFader : CHUDElement
    {
        private Texture2D _screenCover = null;
        private Vector4 _colorVec = Vector4.Zero;
        private Color _color = Color.Black;
        private bool _isActive = false;
        public CScreenFader()
        {
            _fixedPosition = Vector2.Zero;
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            Texture2D texture = new Texture2D(Graphics.CGraphics.GPU, 1, 1, false, SurfaceFormat.Color);
            Color[] c = new Color[1];
            c[0] = Color.White;
            texture.SetData<Color>(c);

            _colorVec = new Vector4((float)Convert.ToDouble(additional[0]),
                                 (float)Convert.ToDouble(additional[1]),
                                 (float)Convert.ToDouble(additional[2]),0);

            _color = new Color(_colorVec);

            _imageIndex.Add("debug:redBox", new Graphics.CSprite("debug:redBox"));

            
        }

        public void beginFade(Vector3 color)
        {
            _isActive = true;
            _colorVec = new Vector4(color, 0);

            _color = new Color(_colorVec);
            startTimer0(3);
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);


            _colorVec = new Vector4(_colorVec.X,
                                    _colorVec.Y,
                                    _colorVec.Z, _colorVec.W + .1f);

            _color = new Color(_colorVec);

            if (_colorVec.W <= 1.0f)
                startTimer0(6);
            else
            {
                _isActive = false;
                Master.Push(new usr.local.GameOver(CMasterControl.gameOverMenu()));
            }
        }

        public override void drawMe(bool useOverlay = false, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch = null)
        {
            if (_isActive)
            {
                Graphics.CGraphics.spriteBatch.Draw(Graphics.CTextures.rawTextures["debug:redBox"], new Rectangle((int)(position.X), (int)(position.Y),
                                            240, 160), null, _color);

                base.drawMe();
            }
        }
    }
}
