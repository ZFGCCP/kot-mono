using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Graphics;
using King_of_Thieves.Input;
using Gears.Cloud;

namespace King_of_Thieves.Actors.HUD.other
{
    class CPickPocketMeter : magic.CMagicMeter
    {
        private int _speed = 1;
        private bool _pickSuccess = false;

        public CPickPocketMeter(int speed = 1) :
            base()
        {
            _meter = new CRect(100, 2, 0, 0, new Microsoft.Xna.Framework.Color(248, 208, 32));
            _fixedPosition.X = 100;
            _fixedPosition.Y = 120;
            int remainder = 100 % speed;

            _speed = speed;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if (_amount <= 0)
            {
                _state = ACTOR_STATES.INCREMENT;
                _amount = 0;
            }
            else if (_amount >= _capacity)
            {
                _state = ACTOR_STATES.DECREMENT;
                _amount = _capacity;
            }

            if (_state == ACTOR_STATES.DECREMENT)
                subtractMagic(_speed);
            else if (_state == ACTOR_STATES.INCREMENT)
                addMagic(_speed);
        }

        public override void keyRelease(object sender)
        {
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;

            if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.C))
            {
                if (_amount >= 80 && _amount <= 100)
                {
                    _pickSuccess = true;
                }
            }
        }

        public bool pickSuccess
        {
            get
            {
                return _pickSuccess;
            }
        }
    }
}
