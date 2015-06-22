using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.weapons
{
    class CShockStick : CActor
    {
        private const string _PARENT_SPRITE_NAMESPACE = "npc:bokoblin";
        private const string _SPRITE_NAMESPACE = "weapons:shockStick";

        private const string _WALK_DOWN = _SPRITE_NAMESPACE + ":walkDown";
        private const string _WALK_UP = _SPRITE_NAMESPACE + ":walkUp";
        private const string _WALK_LEFT = _SPRITE_NAMESPACE + ":walkLeft";
        private const string _WALK_RIGHT = _SPRITE_NAMESPACE + ":walkRight";

        private const string _IDLE_DOWN = _SPRITE_NAMESPACE + ":idleDown";
        private const string _IDLE_UP = _SPRITE_NAMESPACE + ":idleUp";
        private const string _IDLE_LEFT = _SPRITE_NAMESPACE + ":idleLeft";
        private const string _IDLE_RIGHT = _SPRITE_NAMESPACE + ":idleRight";

        private const string _ATTACK_DOWN = _SPRITE_NAMESPACE + ":attackDown";
        private const string _ATTACK_UP = _SPRITE_NAMESPACE + ":attackUp";
        private const string _ATTACK_LEFT = _SPRITE_NAMESPACE + ":attackLeft";
        private const string _ATTACK_RIGHT = _SPRITE_NAMESPACE + ":attackRight";

        private static int _shockStickCount = 0;

        private Dictionary<string, string> _spriteMap = new Dictionary<string, string>();

        private Collision.CHitBox _sideBox = null;
        private Collision.CHitBox _downBox = null;
        private Vector2 _downOffset = new Vector2(12, 26);
        private Vector2 _leftOffset = new Vector2(2, 22);
        private Vector2 _rightOffset = new Vector2(26, 22);
        private Vector2 _upOffset = new Vector2(25, 5);

        public CShockStick()
            : base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_PARENT_SPRITE_NAMESPACE))
                Graphics.CTextures.addRawTexture(_PARENT_SPRITE_NAMESPACE, "sprites/npc/bokoblin");

            if (_shockStickCount <= 0)
            {
                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:0", "7:0", 20));
                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:4", "7:4", 20));
                Graphics.CTextures.addTexture(_WALK_RIGHT, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:2", "7:2", 20));

                Graphics.CTextures.addTexture(_IDLE_DOWN, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:0", "4:0", 0));
                Graphics.CTextures.addTexture(_IDLE_UP, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:4", "4:4", 0));
                Graphics.CTextures.addTexture(_IDLE_RIGHT, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:2", "4:2", 0));

                Graphics.CTextures.addTexture(_ATTACK_DOWN, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:1", "6:1", 0));
                Graphics.CTextures.addTexture(_ATTACK_UP, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:5", "6:5", 0));
                Graphics.CTextures.addTexture(_ATTACK_RIGHT, new Graphics.CTextureAtlas(_PARENT_SPRITE_NAMESPACE, 41, 41, 1, "4:3", "6:3", 0));
            }

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_RIGHT));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_RIGHT, true));

            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_IDLE_UP));
            _imageIndex.Add(_IDLE_RIGHT, new Graphics.CSprite(_IDLE_RIGHT));
            _imageIndex.Add(_IDLE_LEFT, new Graphics.CSprite(_IDLE_RIGHT, true));

            _imageIndex.Add(_ATTACK_DOWN, new Graphics.CSprite(_ATTACK_DOWN));
            _imageIndex.Add(_ATTACK_UP, new Graphics.CSprite(_ATTACK_UP));
            _imageIndex.Add(_ATTACK_RIGHT, new Graphics.CSprite(_ATTACK_RIGHT));
            _imageIndex.Add(_ATTACK_LEFT, new Graphics.CSprite(_ATTACK_RIGHT, true));

            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _IDLE_DOWN.Replace(_SPRITE_NAMESPACE, ""), _IDLE_DOWN);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _IDLE_UP.Replace(_SPRITE_NAMESPACE, ""), _IDLE_UP);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _IDLE_LEFT.Replace(_SPRITE_NAMESPACE, ""), _IDLE_LEFT);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _IDLE_RIGHT.Replace(_SPRITE_NAMESPACE, ""), _IDLE_RIGHT);

            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _WALK_DOWN.Replace(_SPRITE_NAMESPACE, ""), _WALK_DOWN);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _WALK_UP.Replace(_SPRITE_NAMESPACE, ""), _WALK_UP);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _WALK_LEFT.Replace(_SPRITE_NAMESPACE, ""), _WALK_LEFT);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _WALK_RIGHT.Replace(_SPRITE_NAMESPACE, ""), _WALK_RIGHT);

            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _ATTACK_DOWN.Replace(_SPRITE_NAMESPACE, ""), _ATTACK_DOWN);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _ATTACK_UP.Replace(_SPRITE_NAMESPACE, ""), _ATTACK_UP);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _ATTACK_LEFT.Replace(_SPRITE_NAMESPACE, ""), _ATTACK_LEFT);
            _spriteMap.Add(_PARENT_SPRITE_NAMESPACE + _ATTACK_RIGHT.Replace(_SPRITE_NAMESPACE, ""), _ATTACK_RIGHT);

            _sideBox = new Collision.CHitBox(this, 0, 0, 12, 7);
            _downBox = new Collision.CHitBox(this, 0, 0, 7, 12);

            _shockStickCount += 1;
            _state = ACTOR_STATES.IDLE;
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Player.CPlayer));
        }

        public override void collide(object sender, CActor collider)
        {
            if (collider is Player.CPlayer)
            {
                if (!INVINCIBLE_STATES.Contains(collider.state))
                {
                    collider.shock();
                    collider.dealDamange(2, collider);
                }
            }
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            _state = component.root.state;
            _direction = this.component.root.direction;
            swapImage(_spriteMap[component.root.currentImageIndex]);

            if (_state == ACTOR_STATES.ATTACK)
            {
                switch (_direction)
                {
                    case DIRECTION.LEFT:
                        _hitBox = _sideBox;
                        _hitBox.offset = _leftOffset;
                        break;

                    case DIRECTION.RIGHT:
                        _hitBox = _sideBox;
                        _hitBox.offset = _rightOffset;
                        break;

                    case DIRECTION.UP:
                        _hitBox = _downBox;
                        _hitBox.offset = _upOffset;
                        break;

                    case DIRECTION.DOWN:
                        _hitBox = _downBox;
                        _hitBox.offset = _downOffset;
                        break;
                }
            }
            else
                _hitBox = null;
        }
    }
}
