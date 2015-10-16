using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using King_of_Thieves.Input;
using Gears.Cloud;

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

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }

        public override void lift()
        {
            base.lift();
            Vector2 pos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            pos.Y -= 10;
            _state = ACTOR_STATES.LIFT;
            jumpToPoint(pos.X, pos.Y);
            startTimer0(30);
        }

        private void _dropOverPlayer()
        {
            _position.Y += 10;
            _state = ACTOR_STATES.CARRY;
            component.root.hidden = true;
        }

        public override void timer0(object sender)
        {
            _dropOverPlayer();
        }

        public override void keyRelease(object sender)
        {
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;

            if (_state == ACTOR_STATES.CARRY && input.keysReleased.Contains(input.getKey(CInput.KEY_ACTION)))
            {
                toss();
                _followRoot = false;
            }
        }
    }
}
