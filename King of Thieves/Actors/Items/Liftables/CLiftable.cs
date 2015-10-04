using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.Liftables
{
    class CLiftable : CActor
    {
        protected const string _SPRITE_NAMESPACE = "items:liftable:";
        protected const string _IDLE = _SPRITE_NAMESPACE + "idle";
        protected const string _CARRY_LEFT_RIGHT = _SPRITE_NAMESPACE + ":carryLeftRight";
        protected const string _CARRY_UP_DOWN = _SPRITE_NAMESPACE + ":carryUpDown";
        protected const string _BREAK = _SPRITE_NAMESPACE + ":break";

        public CLiftable() :
            base()
        {
            _state = ACTOR_STATES.IDLE;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
        }

        public virtual void toss()
        {
            _state = ACTOR_STATES.TOSSING;
        }

        public virtual void lift()
        {
            _state = ACTOR_STATES.LIFT;
        }

        protected virtual void _break()
        {
            _state = ACTOR_STATES.EXPLODE;
        }

        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case ACTOR_STATES.EXPLODE:
                    _killMe = true;
                    break;
            }
        }

    }
}
