using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Poe
{
    class CLantern : CActor
    {
        private const string _IDLE = "idle";
        private int _health = 2;

        public CLantern() :
            base()
        {
            _imageIndex.Add(_IDLE, new Graphics.CSprite("tileset:smallLantern"));
            swapImage(_IDLE);
            _velocity.Y = 1.0f;
            _state = ACTOR_STATES.IDLE;
            _hitBox = new Collision.CHitBox(this, 3, 3, 10, 10);
        }

        private void _fall()
        {
            moveInDirection(_velocity);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            if (_state == ACTOR_STATES.EXPLODE)
                _fall();
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);

            if (collider is Actors.Projectiles.CBomb && collider.state != ACTOR_STATES.EXPLODE)
                return;

            if (_state != ACTOR_STATES.KNOCKBACK || _state != ACTOR_STATES.EXPLODE)
            {
                _health--;
                _state = ACTOR_STATES.KNOCKBACK;
                startTimer1(120);
            }

            if (_health <= 0)
            {
                _state = ACTOR_STATES.EXPLODE;
                startTimer0(30);
                noCollide = true;
                _followRoot = false;
            }
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            _state = ACTOR_STATES.IDLE;
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            Graphics.CEffects.createEffect(Graphics.CTextures.EFFECT_FIRE_BALL_SMALL, _position, 9);
            _killMe = true;
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Actors.Items.Swords.CSword));
            _collidables.Add(typeof(Actors.Projectiles.CProjectile));
        }
    }
}
