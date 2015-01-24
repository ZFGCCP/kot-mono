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
        private static Vector2 _returnVelocity = new Vector2(1, 1);

        public CBoomerang(Vector2 velocity, Vector2 position, int damage)
            : base(DIRECTION.DOWN, velocity, position)
        {
            _damage = damage;
            _imageIndex.Add(_SPIN, new Graphics.CSprite(Graphics.CTextures.EFFECT_BOOMERANG));
            swapImage(_SPIN);
            _followRoot = false;
            _state = ACTOR_STATES.MOVING;
            _name = "boomerang";
            startTimer0(60);
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
                    _killMe = true;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (_state == ACTOR_STATES.FOLLOW_PLAYER)
            {
                Vector2 position = (Vector2)Map.CMapManager.propertyGetter("player", Map.EActorProperties.POSITION);
                moveToPoint(position.X, position.Y, _returnVelocity.Length());
            }


        }


    }
}
