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

        private static int _liftableCount = 0;

        public CLiftable() :
            base()
        {
            _state = ACTOR_STATES.IDLE;
            _liftableCount++;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
        }

        public virtual void toss()
        {
            _state = ACTOR_STATES.TOSSING;
            startTimer1(30);

            switch (component.root.direction)
            {
                case DIRECTION.DOWN:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, 1);
                    break;

                case DIRECTION.UP:
                    _velocity = new Microsoft.Xna.Framework.Vector2(0, -1);
                    break;

                case DIRECTION.RIGHT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(1, 0);
                    break;

                case DIRECTION.LEFT:
                    _velocity = new Microsoft.Xna.Framework.Vector2(-1, 0);
                    break;
            }
        }

        public virtual void lift()
        {
            _state = ACTOR_STATES.LIFT;
            Map.CMapManager.swapDrawDepth(9, this);
        }

        protected virtual void _break()
        {
            _state = ACTOR_STATES.EXPLODE;
            swapImage(_BREAK);
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

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            if (_state == ACTOR_STATES.TOSSING)
            {
                moveInDirection(_velocity);
            }
        }

        public override void timer1(object sender)
        {
            _break();
        }

    }
}
