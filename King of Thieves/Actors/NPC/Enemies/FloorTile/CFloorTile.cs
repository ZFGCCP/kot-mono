using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.FloorTile
{
    class CFloorTile : CBaseEnemy
    {
        private Vector2 _attackPoint;
        private int _waitTime = 0;
        private const int _WIND_UP_TIME = 60;
        private static int _floorTileCount = 0;

        private const string _SPRITE_NAMESPACE = "npc:floorTile";
        private const string _IDLE = _SPRITE_NAMESPACE + ":idle";
        private const string _SPINNING = _SPRITE_NAMESPACE + ":spinning";
        private const string _BREAKING = _SPRITE_NAMESPACE + ":breaking";

        public CFloorTile() :
            base()
        {
            if (_floorTileCount == 0)
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/floorTile");

                Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE,16,16,0,"0:0","0:0"));
                Graphics.CTextures.addTexture(_SPINNING, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 16, 16, 0, "0:0", "0:1",10));
                Graphics.CTextures.addTexture(_BREAKING, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 16, 16, 0, "1:0", "1:1", 1));
            }

            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            _imageIndex.Add(_SPINNING, new Graphics.CSprite(_SPINNING));
            _imageIndex.Add(_BREAKING, new Graphics.CSprite(_BREAKING));

            _floorTileCount++;
            _state = ACTOR_STATES.IDLE;
            swapImage(_IDLE);
            _hitBox = new Collision.CHitBox(this, 0, 0, 16, 16);
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
            _hitBox.destroy();
            _hitBox = null;
            _state = ACTOR_STATES.EXPLODE;
            swapImage(_BREAKING);

            if (collider is Player.CPlayer)
            {
                if (!INVINCIBLE_STATES.Contains(collider.state))
                    collider.dealDamange(1, collider);
            }
        }

        public override void destroy(object sender)
        {
            base.destroy(sender);
            swapImage(_BREAKING);
        }

        public override void animationEnd(object sender)
        {
            base.animationEnd(sender);
            if (_state == ACTOR_STATES.EXPLODE)
                _killMe = true;
        }

        protected override void cleanUp()
        {
            _floorTileCount--;

            if (_floorTileCount == 0)
            {
                Graphics.CTextures.cleanUp(_SPRITE_NAMESPACE);
                Graphics.CTextures.rawTextures.Remove(_SPRITE_NAMESPACE);
            }
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _waitTime = Convert.ToInt32(additional[0]);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case ACTOR_STATES.ATTACK:
                    moveToPoint(_attackPoint.X, _attackPoint.Y, 2.0f);
                    break;

                default:
                    break;
            }
        }

        private void _userEventWakeUp(object sender)
        {
            startTimer0(_waitTime);
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _state = ACTOR_STATES.IDLE_STARE;
            swapImage(_SPINNING);
            startTimer1(_WIND_UP_TIME);
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            _state = ACTOR_STATES.ATTACK;
            _attackPoint.X = Player.CPlayer.glblX;
            _attackPoint.Y = Player.CPlayer.glblY;
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();
            _userEvents.Add(0, _userEventWakeUp);
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Player.CPlayer));
            _collidables.Add(typeof(Collision.CSolidTile));
            _collidables.Add(typeof(Items.Swords.CSword));
        }
    }
}
