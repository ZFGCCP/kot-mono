using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.NPC.Enemies.ArmoredCrab
{
    class CArmoredCrab : CBaseEnemy
    {
        Vector2 _moveToThis = Vector2.Zero;
        private static int _armoredCrabCount = 0;
        private readonly static string _SPRITE_NAMESPACE = "npc:armoredCrab";

        public CArmoredCrab() :
            base()
        {
            if (_armoredCrabCount <= 0)
                Graphics.CTextures.rawTextures.Add(_SPRITE_NAMESPACE, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/armoredCrab"));

            _armoredCrabCount++;

            _hearingRadius = 3;
            _lineOfSight = 10;
            _visionRange = 90;

            //always looks down
            _direction = DIRECTION.DOWN;
            _angle = 270;
        }

        public override void destroy(object sender)
        {
            _armoredCrabCount--;

            if (_armoredCrabCount <= 0)
            {
                cleanUp();
                _armoredCrabCount = 0;
            }

            base.destroy(sender);
        }

        protected override void cleanUp()
        {
            Graphics.CTextures.cleanUp(_SPRITE_NAMESPACE);
            _armoredCrabCount = 0;
            base.cleanUp();
        }

        private void _chooseNewPoint()
        {
            _moveToThis = getRandomPointInSightRange();
            _state = ACTOR_STATES.MOVING;
        }

        public override void update(GameTime gameTime)
        {
            //check if the player is in hearing range
            if (isPointInHearingRange(new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY)))
                state = ACTOR_STATES.CHASE;

            if (state == ACTOR_STATES.MOVING)
            {
                moveToPoint(_moveToThis.X, _moveToThis.Y, 5);

                if ((_position.X <= _moveToThis.X - 2 && _position.X >= _moveToThis.X + 2) &&
                    (_position.Y <= _moveToThis.Y - 2 && _position.Y >= _moveToThis.Y + 2))
                {
                    state = ACTOR_STATES.IDLE;
                    startTimer0(120);
                }
            }

            base.update(gameTime);
        }

        public override void timer0(object sender)
        {
            _chooseNewPoint();
            base.timer0(sender);
        }


    }
}
