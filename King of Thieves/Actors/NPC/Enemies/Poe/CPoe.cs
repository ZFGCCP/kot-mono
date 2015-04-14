using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Poe
{
    class CPoe : CBaseEnemy
    {
        private const string _SPRITE_NAMESPACE = "npc:poe";
        private const string _MOVING = _SPRITE_NAMESPACE + ":moving";
        private static int _poeCount = 0;

        public CPoe() :
            base()
        {
            if (_poeCount <= 0)
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/poe");

                Graphics.CTextures.addTexture(_MOVING, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 40, 50, 1, "0:0", "8:0", 5));
            }

            _imageIndex.Add(_MOVING, new Graphics.CSprite(_MOVING));
            _state = ACTOR_STATES.MOVING;
            swapImage(_MOVING);
            _poeCount += 1;
            _hitBox = new Collision.CHitBox(this, 10, 10, 22, 35);

            _velocity = new Vector2(.25f, .25f);
            startTimer0(120);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            switch (_state)
            {
                case ACTOR_STATES.MOVING:
                    moveInDirection(_velocity);
                    break;

                default:
                    break;
            }
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _selectVelocity();
            startTimer0(120);
        }

        public override void destroy(object sender)
        {
            base.destroy(sender);
            _poeCount--;

            if (_poeCount == 0)
                cleanUp();
        }

        protected override void cleanUp()
        {
            Graphics.CTextures.cleanUp(_SPRITE_NAMESPACE);
        }

        private void _selectVelocity()
        {
            int xSign = this._randNum.Next(2) > 0 ? 1 : -1;
            int ySign = this._randNum.Next(2) > 0 ? 1 : -1;

            _velocity.X *= xSign;
            _velocity.Y *= ySign;
        }
    }
}
