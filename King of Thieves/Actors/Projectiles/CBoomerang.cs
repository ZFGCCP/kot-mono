using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.Projectiles
{
    class CBoomerang : CProjectile
    {
        private static string _SPIN = "spin";
        private static int _VELO = 2;

        public CBoomerang(Vector2 velocity, Vector2 position, DIRECTION direction, int damage)
            : base(DIRECTION.DOWN, velocity, position)
        {
            _damage = damage;
            _imageIndex.Add(_SPIN, new Graphics.CSprite(Graphics.CTextures.EFFECT_BOOMERANG));
            swapImage(_SPIN);
            _followRoot = false;
            _state = ACTOR_STATES.MOVING;
            _name = "boomerang";
            startTimer0(60);
            _hitBox = new Collision.CHitBox(this, 9, 9, 10, 10);

            //direction calculation
            _calculateDirection(velocity, direction);
            CMasterControl.audioPlayer.soundBank["Items:boomerang"].sfxInstance.IsLooped = true;
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Items:boomerang"]);
        }

        private void _calculateDirection(Vector2 velocity, DIRECTION direction)
        {
            if (velocity.X != 0)
                velocity.X /= Math.Abs(velocity.X);

            if (velocity.Y != 0)
                velocity.Y /= Math.Abs(velocity.Y);

            velocity *= _VELO;
            _velocity = velocity;
        }

        public override void timer0(object sender)
        {
            _state = ACTOR_STATES.FOLLOW_PLAYER;
            _velocity = Vector2.Zero;
        }

        public override void collide(object sender, CActor collider)
        {
            if (_state == ACTOR_STATES.FOLLOW_PLAYER)
                if (collider is Player.CPlayer)
                {
                    _killMe = true;
                    CMasterControl.audioPlayer.stopSfx(CMasterControl.audioPlayer.soundBank["Items:boomerang"]);
                }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (_state == ACTOR_STATES.FOLLOW_PLAYER)
            {
                Vector2 position = (Vector2)Map.CMapManager.propertyGetter("player", Map.EActorProperties.POSITION);
                moveToPoint(position.X, position.Y, _VELO);
            }


        }


    }
}
