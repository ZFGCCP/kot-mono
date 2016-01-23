using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Other.TownsFolk
{
    class CAngelo : CBaseNpc
    {
        private const string _SPRITE_NAMESPACE = "npc:townsFolk:";
        private const string _ANGELO_IDLE = _SPRITE_NAMESPACE + "angeloIdle";
        private const string _ANGELO_WALK = _SPRITE_NAMESPACE + "angeloWalk";

        private string[] _dialog1 = { "Ahh, I just came outside to see what the ruckus was all about.  Something about snakes I hear?", "Would be mighty nice to have me one of those as a reference for my sculptures!" };

        public CAngelo() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/friendly/friendlyNPCs");

                Graphics.CTextures.addTexture(_ANGELO_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "1:0", "1:0"));
                Graphics.CTextures.addTexture(_ANGELO_WALK, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "3:0",10));
            }

            _imageIndex.Add(_ANGELO_IDLE, new Graphics.CSprite(_ANGELO_IDLE));
            _imageIndex.Add(_ANGELO_WALK, new Graphics.CSprite(_ANGELO_WALK));
            _state = ACTOR_STATES.IDLE;
            swapImage(_ANGELO_IDLE);
            _hearingRadius = 30;
        }
    }
}
