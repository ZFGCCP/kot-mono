using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves
{
    class CTimer
    {
        private System.DateTime _startTime;
        private System.DateTime _stopTime;
        private int _ticks = 0; //in ms
        private bool _active = false;

        public CTimer()
        {

        }

        public void start(int ticks)
        {
            _ticks = ticks;
            _startTime = DateTime.Now;
            _stopTime = _startTime.AddMilliseconds(_ticks);
            _active = true;
        }

        public void stop()
        {
            _ticks = 0;
            _stopTime = DateTime.Now;
            _active = false;
        }

        public bool runTime()
        {
            if (_stopTime <= DateTime.Now)
            {
                _ticks = 0;
                return true;
            }
            
            return false;
        }

        public bool isActive
        {
            get
            {
                return _active;
            }
        }

    }
}
