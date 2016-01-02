using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Projectiles
{
    enum ARROW_TYPES
    {
        STANDARD = 0,
        FIRE,
        ICE
    }

    class CArrow : CProjectile
    {
        private static int _arrowCount = 0;
        private bool _isFire = false;
        private bool _isIce = false;

        public CArrow(DIRECTION direction, Vector2 velocity, Vector2 position, ARROW_TYPES arrowType = ARROW_TYPES.STANDARD) 
            : base(direction, velocity, position)
        {
            _imageIndex.Add(PROJ_DOWN, new Graphics.CSprite(Graphics.CTextures.EFFECT_ARROW));
            _imageIndex.Add(PROJ_RIGHT, new Graphics.CSprite(Graphics.CTextures.EFFECT_ARROW_RIGHT));
            _imageIndex.Add(PROJ_LEFT, new Graphics.CSprite(Graphics.CTextures.EFFECT_ARROW_RIGHT,true,false));
            _imageIndex.Add(PROJ_UP, new Graphics.CSprite(Graphics.CTextures.EFFECT_ARROW,false,true));

            _arrowCount += 1;
            _name = "arrow" + _arrowCount;
            
            _damage = 1;
            _transformArrowType(arrowType);

            startTimer1(60);
        }

        private void _userEventchangeType(object sender)
        {
            _transformArrowType((ARROW_TYPES)userParams[0]);
        }

        private void _transformArrowType(ARROW_TYPES arrowType)
        {
            switch (arrowType)
            {
                case ARROW_TYPES.STANDARD:
                    _transformToStandard();
                    break;

                case ARROW_TYPES.ICE:
                    _transformToIce();
                    break;

                case ARROW_TYPES.FIRE:
                    _transformToFire();
                    break;
            }
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Actors.NPC.Enemies.CBaseEnemy));
            _collidables.Add(typeof(Actors.NPC.Enemies.Poe.CLantern));
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);

            if (collider is NPC.Enemies.Rump.CRump)
            {
                if (((NPC.Enemies.Rump.CRump)collider).isReal)
                    dealDamange(1, collider);
                else
                {

                }
                _killMe = true;
            }
            else if (!(collider is Actors.NPC.Enemies.Poe.CPoe))
            {
                _killMe = true;
            }
        }

        private void _transformToFire()
        {
            _isFire = true;
            _isIce = false;
            _damage = 2;
        }

        private void _transformToIce()
        {
            _isFire = false;
            _isIce = true;
            _damage = 2;
        }

        private void _transformToStandard()
        {
            _isIce = false;
            _isFire = false;
            _damage = 1;
        }

        public override void destroy(object sender)
        {
            base.destroy(sender);
            _arrowCount -= 1;
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            _damage = 3;
        }

        protected override void shoot()
        {
            startTimer0(60);
            _followRoot = false;
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Items:arrowShoot"]);
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(PROJ_DOWN);
                    _hitBox = new Collision.CHitBox(this, 15, 20, 5, 5);
                    _velocity.Y = 4;
                    break;

                case DIRECTION.RIGHT:
                    swapImage(PROJ_RIGHT);
                    _hitBox = new Collision.CHitBox(this, 24, 12, 5, 5);
                    _velocity.X = 4;
                    break;

                case DIRECTION.LEFT:
                    swapImage(PROJ_LEFT);
                    _hitBox = new Collision.CHitBox(this, 6, 12, 5, 5);
                    _velocity.X = -4;
                    break;

                case DIRECTION.UP:
                    swapImage(PROJ_UP);
                    _hitBox = new Collision.CHitBox(this, 15, 10, 5, 5);
                    _velocity.Y = -4;
                    break;
            }
        }

        public void userEventShoot(object sender)
        {
            shoot();
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();

            _userEvents.Add(0, userEventShoot);
            _userEvents.Add(1, _userEventchangeType);
        }

        public bool isIce
        {
            get
            {
                return _isIce;
            }
        }

        public bool isFire
        {
            get
            {
                return _isFire;
            }
        }
        
    }
}
