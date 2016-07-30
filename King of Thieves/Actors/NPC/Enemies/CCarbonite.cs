using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies
{
    class CCarbonite : CBaseEnemy
    {
        private const string _SPRITE_NAMESPACE = "npc:carbonite";

        protected const string _IDLE = _SPRITE_NAMESPACE + ":idle";
        protected const string _WAKE = _SPRITE_NAMESPACE + ":wake";
        protected const string _ROLL_DOWN = _SPRITE_NAMESPACE + ":rollDown";
        protected const string _ROLL_UP = _SPRITE_NAMESPACE + ":rollUp";
        protected const string _ROLL_LEFT = _SPRITE_NAMESPACE + ":rollLeft";
        protected const string _ROLL_RIGHT = _SPRITE_NAMESPACE + ":rollRight";

        private Vector2 _moveToThis = Vector2.Zero;

        public CCarbonite() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/carbonite");

                Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 30, 1, "3:0", "3:0"));
                Graphics.CTextures.addTexture(_ROLL_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 30, 1, "0:5", "5:5", 10));
                Graphics.CTextures.addTexture(_ROLL_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 30, 1, "0:4", "5:4", 10));
                Graphics.CTextures.addTexture(_ROLL_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 30, 1, "0:4", "5:4", 10));
                Graphics.CTextures.addTexture(_ROLL_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 30, 1, "0:4", "5:4", 10));
            }

            _state = ACTOR_STATES.IDLE;
            _direction = DIRECTION.DOWN;
            _angle = 270;
            _lineOfSight = 10;
            _visionRange = 30;
            _hearingRadius = 30;
            swapImage(_IDLE);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case ACTOR_STATES.IDLE:
                    if (_checkIfPointInView(new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY)))
                    {
                        _state = ACTOR_STATES.ALERT;
                        startTimer0(60);
                    }
                    break;

                case ACTOR_STATES.CHASE:
                    moveInDirection(_moveToThis, 1);
                    break;

                case ACTOR_STATES.IDLE_STARE:
                    if (isPointInHearingRange(new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY)))
                    {
                        _state = ACTOR_STATES.ALERT;
                        startTimer0(60);
                    }
                    break;
            }
        }

        public override void timer0(object sender)
        {
            switch (_state)
            {
                case ACTOR_STATES.ALERT:
                    if (_checkIfPointInView(new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY)))
                    {
                        _state = ACTOR_STATES.ATTACK;
                    }
                    break;
            }
        }

        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case ACTOR_STATES.ATTACK:
                    _state = ACTOR_STATES.CHASE;
                    _moveToThis = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
                    break;
            }
        }

        public override void collide(object sender, CActor collider)
        {
            if (collider is Collision.CSolidTile)
            {
                solidCollide(collider);
                _state = ACTOR_STATES.IDLE_STARE;
            }
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Collision.CSolidTile));
        }
    }
}
