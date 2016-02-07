using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Other.TownsFolk
{
    class CGlootan : CBaseNpc
    {
        private const string _SPRITE_NAMESPACE = "npc:townsFolk:";
        private const string _GLOOTAN_IDLE = _SPRITE_NAMESPACE + "glootanIdle";

        public CGlootan() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/friendly/friendlyNPCs");

                Graphics.CTextures.addTexture(_GLOOTAN_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:3", "0:3"));
            }

            _imageIndex.Add(_GLOOTAN_IDLE, new Graphics.CSprite(_GLOOTAN_IDLE));
            _state = ACTOR_STATES.IDLE;
            swapImage(_GLOOTAN_IDLE);
            _hearingRadius = 30;
        }
    }
}
