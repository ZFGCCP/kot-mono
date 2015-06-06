using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Bokoblin
{
    class CBokoblin : CBaseEnemy
    {
        private const string _SPRITE_NAMESPACE = "npc:bokoblin";

        private const string _WALK_DOWN = _SPRITE_NAMESPACE + ":walkDown";
        private const string _WALK_UP = _SPRITE_NAMESPACE + ":walkUp";
        private const string _WALK_LEFT = _SPRITE_NAMESPACE + ":walkLeft";
        private const string _WALK_RIGHT = _SPRITE_NAMESPACE + ":walkRight";

        private static int _bokoblinCount = 0;

        public CBokoblin() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/bokoblin");

                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41,30,"0:0","3:0",30));
                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 30, "0:4", "3:4", 30));
                Graphics.CTextures.addTexture(_WALK_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 41, 41, 30, "0:2", "3:2", 30));
            }

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_RIGHT));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_RIGHT,true));

            _bokoblinCount += 1;
            _angle = 270;
            _direction = DIRECTION.DOWN;
            swapImage(_WALK_DOWN);
            _state = ACTOR_STATES.MOVING;
        }

        public override void destroy(object sender)
        {
            _bokoblinCount -= 1;
            _doNpcCountCheck(ref _bokoblinCount);
            base.destroy(sender);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);


        }
    }
}
