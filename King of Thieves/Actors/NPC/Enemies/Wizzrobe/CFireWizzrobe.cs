using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Wizzrobe
{
    class CFireWizzrobe : CBaseWizzrobe
    {

        private static int _fireWizzrobeCount = 0;
        //texture atlas constants
        protected readonly static string _SPRITE_NAMESPACE = "Npc:FireWizzrobe";
        protected readonly static string _WIZZROBE_IDLE_DOWN = _SPRITE_NAMESPACE + ":idleDown";
        protected readonly static string _WIZZROBE_ATTACK_DOWN = _SPRITE_NAMESPACE + ":attackDown";
        protected readonly static string _WIZZROBE_IDLE_LEFT = _SPRITE_NAMESPACE + ":idleLeft";
        protected readonly static string _WIZZROBE_ATTACK_LEFT = _SPRITE_NAMESPACE + ":attackLeft";
        protected readonly static string _WIZZROBE_IDLE_RIGHT = _SPRITE_NAMESPACE + ":idleRight";
        protected readonly static string _WIZZROBE_ATTACK_RIGHT = _SPRITE_NAMESPACE + ":attackRight";
        protected readonly static string _WIZZROBE_IDLE_UP = _SPRITE_NAMESPACE + ":idleUp";
        protected readonly static string _WIZZROBE_ATTACK_UP = _SPRITE_NAMESPACE + ":attackUp";

        public CFireWizzrobe() :
            base(WIZZROBE_TYPE.FIRE)
        {
            //we have to cache the sprites
            if (_fireWizzrobeCount <= 0)
            {
                Graphics.CTextures.addTexture(_WIZZROBE_IDLE_DOWN, new Graphics.CTextureAtlas(_NPC_WIZZROBE, 32, 32, 0, "0:2", "0:2"));
                Graphics.CTextures.addTexture(_WIZZROBE_IDLE_LEFT, new Graphics.CTextureAtlas(_NPC_WIZZROBE, 32, 32, 0, "2:2", "2:2"));
                Graphics.CTextures.addTexture(_WIZZROBE_IDLE_UP, new Graphics.CTextureAtlas(_NPC_WIZZROBE, 32, 32, 0, "4:2", "4:2"));

                Graphics.CTextures.addTexture(_WIZZROBE_ATTACK_DOWN, new Graphics.CTextureAtlas(_NPC_WIZZROBE, 32, 32, 0, "1:2", "1:2"));
                Graphics.CTextures.addTexture(_WIZZROBE_ATTACK_LEFT, new Graphics.CTextureAtlas(_NPC_WIZZROBE, 32, 32, 0, "3:2", "3:2"));
                Graphics.CTextures.addTexture(_WIZZROBE_ATTACK_UP, new Graphics.CTextureAtlas(_NPC_WIZZROBE, 32, 32, 0, "5:2", "5:2"));
            }

            _imageIndex.Add(_MAP_ICON, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_WIZZROBE_IDLE_DOWN));
            _imageIndex.Add(_IDLE_LEFT, new Graphics.CSprite(_WIZZROBE_IDLE_LEFT));
            _imageIndex.Add(_IDLE_RIGHT, new Graphics.CSprite(_WIZZROBE_IDLE_LEFT, true));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_WIZZROBE_IDLE_UP));

            _imageIndex.Add(_ATTACK_DOWN, new Graphics.CSprite(_WIZZROBE_ATTACK_DOWN));
            _imageIndex.Add(_ATTACK_LEFT, new Graphics.CSprite(_WIZZROBE_ATTACK_LEFT));
            _imageIndex.Add(_ATTACK_RIGHT, new Graphics.CSprite(_WIZZROBE_ATTACK_LEFT, true));
            _imageIndex.Add(_ATTACK_UP, new Graphics.CSprite(_WIZZROBE_ATTACK_UP));

            _fireWizzrobeCount += 1;


        }

        protected override void cleanUp()
        {
            if (_fireWizzrobeCount <= 0)
            {
                Graphics.CTextures.cleanUp(_SPRITE_NAMESPACE);
                _fireWizzrobeCount = 0;
                base.cleanUp();
            }

        }

        public override void destroy(object sender)
        {
            _fireWizzrobeCount--;

            if (_fireWizzrobeCount <= 0)
            {
                cleanUp();
                _fireWizzrobeCount = 0;
            }

            base.destroy(sender);
        }

        protected override void _fireProjectile()
        {
            Vector2 projectileVelo = Vector2.Zero;

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    projectileVelo.Y = 3;
                    break;

                case DIRECTION.UP:
                    projectileVelo.Y = -3;
                    break;

                case DIRECTION.RIGHT:
                    projectileVelo.X = 3;
                    break;

                case DIRECTION.LEFT:
                    projectileVelo.X = -3;
                    break;
            }

            Map.CMapManager.addActorToComponent(new Actors.Projectiles.CFireBall(_direction, projectileVelo, _position), componentAddress);
        }

    }
}
