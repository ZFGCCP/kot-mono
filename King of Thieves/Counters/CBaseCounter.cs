using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Counters
{
    class CBaseCounter
    {
        private int _value;
        private int _min;
        private int _max;

        public CBaseCounter(int min, int max, int value)
        {
            _min = min;
            _max = max;

            if (value > max)
                throw new ArgumentException("Value was higher than the max.");
            else if (value < min)
                throw new ArgumentException("Value was lower than the min.");

            _value = value;
        }

        public void change(int val)
        {
            if (val + _value < _min)
                throw new ArgumentException("Value was lower than the min.");
            else if (val + _value > _min)
                throw new ArgumentException("Value was lower than the min.");

            _value += val;
        }

        public int value
        {
            get
            {
                return _value;
            }
        }

        public int max
        {
            get
            {
                return _max;
            }
        }

        public int min
        {
            get
            {
                return _min;
            }
        }
    }
}
