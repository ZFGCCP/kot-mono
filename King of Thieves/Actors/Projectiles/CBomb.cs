using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Projectiles
{
    class CBomb : CProjectile
    {
        private static int _bombCount = 0;
        private static string _TICK = "bombTick";
        private static string _FAST_TICK = "bombFastTick";
        private static string _EXPLOSION = "explosion";

        public CBomb(DIRECTION direction, Vector2 velocity, Vector2 position, int damage) :
            base(direction, velocity, position)
        {
            _bombCount += 1;
            _damage = damage;
            
            _imageIndex.Add(_TICK, new Graphics.CSprite(Graphics.CTextures.EFFECT_BOMB));
            _imageIndex.Add(_FAST_TICK, new Graphics.CSprite(Graphics.CTextures.EFFECT_BOMB_FAST_TICK));
            _imageIndex.Add(_EXPLOSION, new Graphics.CSprite(Graphics.CTextures.EFFECT_EXPLOSION));
            _name = "bomb" + _bombCount;

            _hitBox = new Collision.CHitBox(this, 0, 0, 10, 10);
            swapImage(_TICK);
            shoot();
            startTimer0(120);
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            swapImage(_FAST_TICK);
            startTimer1(60);
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            _blowUp();
            _hitBox.destroy();
            _hitBox = null;
            _hitBox = new Collision.CHitBox(this, 0, 0, 30, 30);
            _velocity = Vector2.Zero;
        }

        public override void animationEnd(object sender)
        {
            if (_state == ACTOR_STATES.EXPLODE)
                _killMe = true;
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);

            if (_state == ACTOR_STATES.EXPLODE)
            {

            }
            else
            {
                if (collider is Actors.Collision.CSolidTile || collider is Actors.NPC.Enemies.CBaseEnemy)
                    _blowUp();
            }
        }

        private void _blowUp()
        {
            _state = ACTOR_STATES.EXPLODE;
            swapImage(_EXPLOSION);
        }





    }
}
