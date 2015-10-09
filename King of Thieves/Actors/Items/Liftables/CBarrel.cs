using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.Liftables
{
    class CBarrel : CLiftable
    {
        private static int _barrelCount = 0;

        public CBarrel() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.rawTextures.Add(_SPRITE_NAMESPACE, CMasterControl.glblContent.Load<Texture2D>("sprites/items/liftables"));
                Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "0:0"));
            }

            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            _imageIndex.Add(_MAP_ICON, new Graphics.CSprite(_IDLE));

            _barrelCount++;

            swapImage(_IDLE);
            _state = ACTOR_STATES.IDLE;
            _drawDepth = 8;
            _hitBox = new Collision.CHitBox(this, 8, 5, 16, 24);
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            /*if (additional != null > 0 && additional[0] == "T")
            {
                _position.Y -= 10;
                startTimer0(30);
                
            }*/
        }

        public override void timer0(object sender)
        {
            _dropOverPlayer();
        }

        private void _dropOverPlayer()
        {
            _state = ACTOR_STATES.CARRY;
            _position.X += 10;
        }
    }
}
