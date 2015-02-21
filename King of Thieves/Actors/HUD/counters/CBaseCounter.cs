using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.HUD.counters
{
    class CBaseCounter : CHUDElement
    {
        private static SpriteFont _sherwood = CMasterControl.glblContent.Load<SpriteFont>(@"Fonts/sherwood");
        private int _capacity;
        private int _amount;
        private int _incrementAmount = 0;
        private bool _instantaneousUpdate = false;

        public CBaseCounter(int capacity, int amount)
            : base()
        {
            _capacity = capacity;
            _amount = amount;
            _state = ACTOR_STATES.IDLE;
        }

        public void increment(int amount, bool instant = false)
        {
            _state = ACTOR_STATES.INCREMENT;
            _incrementAmount = amount;
            _instantaneousUpdate = instant;
        
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            if (_state == ACTOR_STATES.INCREMENT)
            {
                if (_instantaneousUpdate)
                {
                    int allowedIncrement = _capacity - _amount;
                    if (allowedIncrement < _incrementAmount)
                        _incrementAmount = allowedIncrement;

                    _amount += _incrementAmount;
                }
                else
                {
                    if (_amount < _capacity && _incrementAmount > 0)
                    {
                        _amount += 1;
                        _incrementAmount -= 1;
                    }
                }
            }
        }

        public override void draw(object sender)
        {
            base.draw(sender);

        }
    }
}
