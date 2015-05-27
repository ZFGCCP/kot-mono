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
        public const string SPRITE_NAMESPACE = "npc:armoredCrab";
        private const string _IDLE = SPRITE_NAMESPACE + ":idle";
        private const string _WALK = SPRITE_NAMESPACE + ":walk";
        private const string _IDLE_NOSHELL = SPRITE_NAMESPACE + ":idleNoShell";
        private const string _WALK_NOSHELL = SPRITE_NAMESPACE + ":walkNoShell";
        private const int _UPPER_RANGE = 70;
        private const int _LOWER_RANGE = 30;
        private bool _hasShell = true;

        public CArmoredCrab() :
            base()
        {
            if (_armoredCrabCount <= 0)
            {
                Graphics.CTextures.rawTextures.Add(SPRITE_NAMESPACE, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/armoredCrabNew"));

                Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 0, "0:0", "0:0", 0));
                Graphics.CTextures.addTexture(_WALK, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 0, "0:0", "3:0", 10));
                Graphics.CTextures.addTexture(_IDLE_NOSHELL, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 0, "0:3", "0:3", 0));
                Graphics.CTextures.addTexture(_WALK_NOSHELL, new Graphics.CTextureAtlas(SPRITE_NAMESPACE, 32, 32, 0, "0:3", "3:3", 10));
            }

            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            _imageIndex.Add(_WALK, new Graphics.CSprite(_WALK));
            _imageIndex.Add(_IDLE_NOSHELL, new Graphics.CSprite(_IDLE_NOSHELL));
            _imageIndex.Add(_WALK_NOSHELL, new Graphics.CSprite(_WALK_NOSHELL));

            _armoredCrabCount++;

            _hearingRadius = 50;
            _lineOfSight = 120;
            _visionRange = 60;

            //always looks down
            _direction = DIRECTION.DOWN;
            _angle = 270;
            _chooseNewPoint();
            swapImage(_WALK);
            _hitBox = new Collision.CHitBox(this, 7, 5, 20, 20);
        }

        public override void collide(object sender, CActor collider)
        {
            if (collider is Projectiles.CBomb)
            {
                if (collider.state == ACTOR_STATES.EXPLODE)
                {
                    if (_hasShell)
                    {
                        _hasShell = false;
                        swapImage(_WALK_NOSHELL);
                        _invulernable = true;
                        startTimer1(200);
                    }
                    else if (!_invulernable)
                    {
                        _killMe = true;
                    }
                }
            }

            if (!_hasShell && !_invulernable)
            {
                if (collider is Projectiles.CProjectile)
                    _killMe = true;
            }
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Projectiles.CBomb));
            _collidables.Add(typeof(Items.Swords.CSword));
            _collidables.Add(typeof(Projectiles.CArrow));
            _collidables.Add(typeof(Projectiles.CBoomerang));
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
            Graphics.CTextures.cleanUp(SPRITE_NAMESPACE);
            _armoredCrabCount = 0;
            base.cleanUp();
        }

        private void _chooseNewPoint()
        {
            int chooseLeftOrRight = (int)Math.Round(_randNum.NextDouble()); //0 == left, 1 == right


            if (chooseLeftOrRight == 0)
            {
                _moveToThis.X = _randNum.Next((int)_position.X - _UPPER_RANGE, (int)_position.X - _LOWER_RANGE);
                _moveToThis.Y = _position.Y;
            }
            else
            {
                _moveToThis.X = _randNum.Next((int)_position.X + _LOWER_RANGE, (int)_position.X + _UPPER_RANGE);
                _moveToThis.Y = _position.Y;
            }

            _moveToThis.X = (float)Math.Floor(_moveToThis.X);
            _moveToThis.Y = (float)Math.Floor(_moveToThis.Y);
            _state = ACTOR_STATES.MOVING;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            //check if the player is in hearing range
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            if (isPointInHearingRange(playerPos) || _checkIfPointInView(playerPos))
            {
                state = ACTOR_STATES.CHASE;
                moveToPoint((float)Math.Floor(Player.CPlayer.glblX), (float)Math.Floor(Player.CPlayer.glblY), 1.0f, false);
            }
            else
            {
                if (state == ACTOR_STATES.CHASE)
                {
                    state = ACTOR_STATES.MOVING;
                    _chooseNewPoint();
                }
            }

            if (state == ACTOR_STATES.MOVING)
            {
                moveToPoint2(_moveToThis.X, _moveToThis.Y, .5f, false);

                if ((_position.X >= _moveToThis.X - 2 && _position.X <= _moveToThis.X + 2) &&
                    (_position.Y >= _moveToThis.Y - 2 && _position.Y <= _moveToThis.Y + 2))
                {
                    state = ACTOR_STATES.IDLE;
                    swapImage(_hasShell ? _IDLE : _IDLE_NOSHELL);
                    startTimer0(120);
                }
            }

            
        }

        public override void timer0(object sender)
        {
            _chooseNewPoint();
            swapImage(_hasShell? _WALK : _WALK_NOSHELL);
            base.timer0(sender);
        }

        public override void timer1(object sender)
        {
            _invulernable = false;
            base.timer1(sender);
        }


    }
}
