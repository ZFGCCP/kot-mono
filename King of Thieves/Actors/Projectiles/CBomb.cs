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
            _followRoot = false;

            _hitBox = new Collision.CHitBox(this, 13, 11, 10, 10);
            swapImage(_TICK);
            shoot();
            startTimer0(120);
        }

        public override void timer0(object sender)
        {
            swapImage(_FAST_TICK);
            startTimer1(60);
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            _blowUp();
        }

        public override void animationEnd(object sender)
        {
            if (_state == ACTOR_STATES.EXPLODE)
                _killMe = true;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
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

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Collision.CSolidTile));
            _collidables.Add(typeof(NPC.Enemies.CBaseEnemy));
        }

        private void _blowUp()
        {
            _state = ACTOR_STATES.EXPLODE;
            swapImage(_EXPLOSION,false);
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Items:explosionSmall"]);
            _position.X -= 13;
            _position.Y -= 13;
            _hitBox.destroy();
            _hitBox = null;
            _hitBox = new Collision.CHitBox(this, 16, 20, 30, 30);
            _velocity = Vector2.Zero;
        }

        protected override void shoot()
        {
            swapImage(_TICK);
        }





    }
}
