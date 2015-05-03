using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC
{
    class CTownsFolk : CActor
    {
        private string _SPRITE_NAMESPACE = "";
        private const string _SHEET01 = "sprites/npc/friendly/townsfolk01";

        private string _WALK_UP = "walkUp";
        private string _WALK_DOWN = "walkDown";
        private string _WALK_LEFT = "walkLeft";
        private string _WALK_RIGHT = "walkRight";

        private string _IDLE_UP = "idleUp";
        private string _IDLE_DOWN = "idleDown";
        private string _IDLE_LEFT = "idleLeft";
        private string _IDLE_RIGHT = "idleRight";

        public CTownsFolk() :
            base()
        {
            _SPRITE_NAMESPACE = "npc:townsfolk01";

            _WALK_UP = _SPRITE_NAMESPACE + _WALK_UP;
            _WALK_DOWN = _SPRITE_NAMESPACE + _WALK_DOWN;
            _WALK_LEFT = _SPRITE_NAMESPACE + _WALK_LEFT;
            _WALK_RIGHT = _SPRITE_NAMESPACE + _WALK_RIGHT;

            _IDLE_UP = _SPRITE_NAMESPACE + _IDLE_UP;
            _IDLE_DOWN = _SPRITE_NAMESPACE + _IDLE_DOWN;
            _IDLE_LEFT = _SPRITE_NAMESPACE + _IDLE_LEFT;
            _IDLE_RIGHT = _SPRITE_NAMESPACE + _IDLE_RIGHT;
        }
    }
}
