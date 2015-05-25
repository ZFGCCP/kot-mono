using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Guards
{
    enum ALERT_LEVEL
    {
        NORMAL = 0,
        ELEVATED
    }

    class CCastleGuard : CBaseEnemy
    {
        private static ALERT_LEVEL _ALERT = ALERT_LEVEL.NORMAL;
        private const string _SPRITE_NAMESPACE = "npc:castleGuard";

        private const string _IDLE_DOWN = _SPRITE_NAMESPACE + ":idleDown";
        private const string _IDLE_LEFT = _SPRITE_NAMESPACE + ":idleLeft";
        private const string _IDLE_RIGHT = _SPRITE_NAMESPACE + ":idleRight";
        private const string _IDLE_UP = _SPRITE_NAMESPACE + ":idleUp";

        private const string _WALK_DOWN = _SPRITE_NAMESPACE + ":walkDown";
        private const string _WALK_LEFT = _SPRITE_NAMESPACE + ":walkLeft";
        private const string _WALK_RIGHT = _SPRITE_NAMESPACE + ":walkRight";
        private const string _WALK_UP = _SPRITE_NAMESPACE + ":walkUp";

        private const string _RUN_DOWN = _SPRITE_NAMESPACE + ":runDown";
        private const string _RUN_LEFT = _SPRITE_NAMESPACE + ":runLeft";
        private const string _RUN_RIGHT = _SPRITE_NAMESPACE + ":runRight";
        private const string _RUN_UP = _SPRITE_NAMESPACE + ":runUp";

        private static int _castleGuardCount = 0;

        private Vector2 _homePos = Vector2.Zero;

        public CCastleGuard() :
            base()
        {
            _angle = 270;
            _direction = DIRECTION.DOWN;

            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/castleGuard");

                Graphics.CTextures.addTexture(_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 38, 1, "1:0", "1:0"));
                Graphics.CTextures.addTexture(_IDLE_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 38, 1, "4:0", "4:0"));
                Graphics.CTextures.addTexture(_IDLE_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 38, 1, "7:0", "7:0"));

                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 38, 1, "0:0", "2:0",4));
                Graphics.CTextures.addTexture(_WALK_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 38, 1, "3:0", "5:0",4));
                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 38, 1, "6:0", "8:0", 4));
            }

            _imageIndex.Add(_MAP_ICON, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_LEFT));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_LEFT,true));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));

            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_LEFT, new Graphics.CSprite(_IDLE_LEFT));
            _imageIndex.Add(_IDLE_RIGHT, new Graphics.CSprite(_IDLE_LEFT, true));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_IDLE_UP));

            _castleGuardCount += 1;
            swapImage(_IDLE_DOWN);
            _state = ACTOR_STATES.IDLE;
            _angle = 270;
            _direction = DIRECTION.DOWN;
            _hearingRadius = 150;
            _lineOfSight = 120;
            _visionRange = 60;
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _homePos = position;
        }

        public override void destroy(object sender)
        {
            base.destroy(sender);
            _castleGuardCount -= 1;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            Vector2 playerPos = new Vector2(Player.CPlayer.glblX,Player.CPlayer.glblY);

            switch (_state)
            {
                case ACTOR_STATES.IDLE:
                    _searchForPlayer(playerPos);
                    break;

                case ACTOR_STATES.CHASE:

                    if (HUD.notoriety.CNotorietyIcon.notorietyLevel == HUD.notoriety.NOTORIETY_LEVEL.MEDIUM)
                        _direction = moveToPoint2(playerPos.X, playerPos.Y, .5f, false);
                    else
                        _direction = moveToPoint2(playerPos.X, playerPos.Y, 1, false);

                    if (!isPointInHearingRange(playerPos))
                    {
                        _state = ACTOR_STATES.IDLE_STARE;
                        startTimer0(180);
                    }

                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            _angle = 270;
                            swapImage(_WALK_DOWN);
                            break;

                        case DIRECTION.LEFT:
                            _angle = 180;
                            swapImage(_WALK_LEFT);
                            break;

                        case DIRECTION.RIGHT:
                            swapImage(_WALK_RIGHT);
                            _angle = 0;
                            break;

                        case DIRECTION.UP:
                            _angle = 90;
                            swapImage(_WALK_UP);
                            break;
                    }
                    break;

                case ACTOR_STATES.IDLE_STARE:
                    _searchForPlayer(playerPos);
                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            _angle = 270;
                            swapImage(_IDLE_DOWN);
                            break;

                        case DIRECTION.LEFT:
                            _angle = 180;
                            swapImage(_IDLE_LEFT);
                            break;

                        case DIRECTION.RIGHT:
                            swapImage(_IDLE_RIGHT);
                            _angle = 0;
                            break;

                        case DIRECTION.UP:
                            swapImage(_IDLE_UP);
                            _angle = 90;
                            break;
                    }
                    break;

                case ACTOR_STATES.GO_HOME:
                    _direction = moveToPoint2(_homePos.X, _homePos.Y, .5f, false);
                    _searchForPlayer(playerPos);

                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            _angle = 270;
                            swapImage(_WALK_DOWN);
                            break;

                        case DIRECTION.LEFT:
                            _angle = 180;
                            swapImage(_WALK_LEFT);
                            break;

                        case DIRECTION.RIGHT:
                            swapImage(_WALK_RIGHT);
                            _angle = 0;
                            break;

                        case DIRECTION.UP:
                            swapImage(_WALK_UP);
                            _angle = 90;
                            break;
                    }

                    if (_position == _homePos)
                    {
                        _state = ACTOR_STATES.IDLE;
                        _angle = 270;
                        swapImage(_IDLE_DOWN);
                    }

                    break;
            }

            
        }

        public override void timer0(object sender)
        {
            _state = ACTOR_STATES.GO_HOME;
        }

        private void _searchForPlayer(Vector2 playerPos)
        {
            if (isPointInHearingRange(playerPos))
            {
                if (_checkIfPointInView(playerPos))
                    _state = ACTOR_STATES.CHASE;
            }
        }


    }
}
