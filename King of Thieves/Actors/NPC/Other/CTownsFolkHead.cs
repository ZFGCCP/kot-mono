using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Other
{
    class CTownsFolkHead : CActor
    {
        private string _SPRITE_NAMESPACE = "";
        private const string _SHEET01 = "sprites/npc/friendly/townsfolk01";

        private string _WALK_UP = "walkHeadUp";
        private string _WALK_DOWN = "walkHeadDown";
        private string _WALK_LEFT = "walkHeadLeft";
        private string _WALK_RIGHT = "walkHeadRight";

        private string _IDLE_UP = "idleHeadUp";
        private string _IDLE_DOWN = "idleHeadDown";
        private string _IDLE_LEFT = "idleHeadLeft";
        private string _IDLE_RIGHT = "idleHeadRight";

        private Dictionary<string, string> _spriteMap = new Dictionary<string, string>();

        public CTownsFolkHead() :
            base()
        {
            _drawDepth = 10;
            _direction = DIRECTION.DOWN;
            _angle = 270;
            _state = ACTOR_STATES.IDLE;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            _SPRITE_NAMESPACE = additional[0];

            _WALK_UP = _SPRITE_NAMESPACE + _WALK_UP;
            _WALK_DOWN = _SPRITE_NAMESPACE + _WALK_DOWN;
            _WALK_LEFT = _SPRITE_NAMESPACE + _WALK_LEFT;
            _WALK_RIGHT = _SPRITE_NAMESPACE + _WALK_RIGHT;

            _IDLE_UP = _SPRITE_NAMESPACE + _IDLE_UP;
            _IDLE_DOWN = _SPRITE_NAMESPACE + _IDLE_DOWN;
            _IDLE_LEFT = _SPRITE_NAMESPACE + _IDLE_LEFT;
            _IDLE_RIGHT = _SPRITE_NAMESPACE + _IDLE_RIGHT;

            Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:5", "2:5", 4));
            Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:3", "3:3", 4));
            Graphics.CTextures.addTexture(_WALK_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:4", "2:4", 4));

            Graphics.CTextures.addTexture(_IDLE_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:5", "0:5", 0));
            Graphics.CTextures.addTexture(_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "1:3", "1:3", 0));
            Graphics.CTextures.addTexture(_IDLE_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:4", "0:4", 0));

            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_IDLE_UP));
            _imageIndex.Add(_IDLE_LEFT, new Graphics.CSprite(_IDLE_RIGHT, true));
            _imageIndex.Add(_IDLE_RIGHT, new Graphics.CSprite(_IDLE_RIGHT));

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_RIGHT, true));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_RIGHT));
            swapImage(_WALK_DOWN);

            _spriteMap.Add(_IDLE_DOWN.Replace("Head", ""), _IDLE_DOWN);
            _spriteMap.Add(_IDLE_UP.Replace("Head", ""), _IDLE_UP);
            _spriteMap.Add(_IDLE_LEFT.Replace("Head", ""), _IDLE_LEFT);
            _spriteMap.Add(_IDLE_RIGHT.Replace("Head", ""), _IDLE_RIGHT);

            _spriteMap.Add(_WALK_DOWN.Replace("Head", ""), _WALK_DOWN);
            _spriteMap.Add(_WALK_UP.Replace("Head", ""), _WALK_UP);
            _spriteMap.Add(_WALK_LEFT.Replace("Head", ""), _WALK_LEFT);
            _spriteMap.Add(_WALK_RIGHT.Replace("Head", ""), _WALK_RIGHT);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            //check the root's direction, state and image and mimic them
            _direction = this.component.root.direction;
            _state = this.component.root.state;
            swapImage(_spriteMap[this.component.root.currentImageIndex]);
        }
    }
}
