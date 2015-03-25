using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Zombie
{
    class CGibdo : CBaseZombie
    {
        private const string _WALK_DOWN = _SPRITE_NAMESPACE + ":gibdoWalkDown";
        private const string _WALK_UP = _SPRITE_NAMESPACE + ":gibdoWalkUp";
        private const string _WALK_RIGHT = _SPRITE_NAMESPACE + ":gibdoWalkRight";
        private const string _WALK_LEFT = _SPRITE_NAMESPACE + ":gibdoWalkLeft";

        private const string _GRAB_DOWN = _SPRITE_NAMESPACE + ":gibdoGrabDown";
        private const string _GRAB_UP = _SPRITE_NAMESPACE + ":gibdoGrabUp";
        private const string _GRAB_RIGHT = _SPRITE_NAMESPACE + ":gibdoGrabRight";
        private const string _GRAB_LEFT = _SPRITE_NAMESPACE + ":gibdoGrabLeft";

        private static int _gibdoCount = 0;

        public CGibdo()
            : base()
        {
            _state = ACTOR_STATES.MOVING;

            if (_gibdoCount <= 0)
            {
                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 40, 31, 1, "0:1", "6:1", 3));
                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 40, 31, 1, "0:0", "6:0", 3));
                Graphics.CTextures.addTexture(_WALK_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 40, 31, 1, "0:2", "6:2", 3));

                Graphics.CTextures.addTexture(_GRAB_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 40, 31, 1, "0:4", "5:4", 3));
                Graphics.CTextures.addTexture(_GRAB_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 40, 31, 1, "0:3", "5:3", 3));
                Graphics.CTextures.addTexture(_GRAB_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 40, 31, 1, "0:5", "5:5", 3));
            }

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_LEFT));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_LEFT,true));

            _imageIndex.Add(_GRAB_DOWN, new Graphics.CSprite(_GRAB_DOWN));
            _imageIndex.Add(_GRAB_UP, new Graphics.CSprite(_GRAB_UP));
            _imageIndex.Add(_GRAB_LEFT, new Graphics.CSprite(_GRAB_LEFT));
            _imageIndex.Add(_GRAB_RIGHT, new Graphics.CSprite(_GRAB_LEFT,true));
        }

        public override void destroy(object sender)
        {
            _gibdoCount -= 1;
            base.destroy(sender);
        }

        protected override void cleanUp()
        {
            base.cleanUp();
        }
    }
}
