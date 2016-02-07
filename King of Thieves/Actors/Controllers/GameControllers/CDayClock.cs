using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Controllers.GameControllers
{
    enum CLOCKBLOCK
    {
        OUTSIDE = 0,
        CAVE, //used for REALLY dark caves ;)
        INSIDE
    }

    enum MOON
    {
        NEW = 0,
        CRESCENTL,
        HALFL,
        GIBOUSL,
        FULL,
        GIBOUSR,
        HALFR,
        CRESCENTR
    }

    class CDayClock : CActor
    {
        private static CDayClock _singleton = null;
        private static Color _timeOverlay = Color.White;
        public bool enabled = true;
        private LinkedList<MOON> _lunarCycle = new LinkedList<MOON>();
        private LinkedListNode<MOON> _moonPhase;
        private bool _hasMoonPearl = false;

        private double[] _internalCounter = new double[3];

        public void changeClockEnvironment(CLOCKBLOCK environment)
        {
            if (environment == CLOCKBLOCK.OUTSIDE)
                enabled = true;
            else
            {
                enabled = false;

                switch (environment)
                {
                    case CLOCKBLOCK.CAVE:
                        _timeOverlay = new Color(40, 40, 40);
                        break;

                    case CLOCKBLOCK.INSIDE:
                        _timeOverlay = new Color(240, 240, 240);
                        break;
                }
            }
        }

        public static CDayClock instantiateSingleton()
        {
            if (_singleton == null)
                _singleton = new CDayClock();

            return _singleton;
        }

        private CDayClock()
        {
            startTimer0(1); //5 minutes during day
            _state = ACTOR_STATES.DAY;

            //fill moon cycle
            _lunarCycle.AddFirst(0);

            for (int i = 1; i < 8; i++)
                _lunarCycle.AddAfter(_lunarCycle.Last, (MOON)i);

            _moonPhase = _lunarCycle.First;
        }

        public MOON moonPhase
        {
            get
            {
                return _moonPhase.Value;
            }
        }

        public static Color overlay
        {
            get
            {
                return _timeOverlay;
            }
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);

            switch (_state)
            {
                case ACTOR_STATES.DAY:
                    startTimer0(120000); //2 minutes during dusk
                    _state = ACTOR_STATES.DUSK;
                    break;

                case ACTOR_STATES.DUSK:
                    //screen fades to orange
                    startTimer0(60000); //1 minute during night,
                    _state = ACTOR_STATES.NIGHT;
                    CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Background:Nature:Wolf"]);

                    if (_moonPhase == _lunarCycle.Last)
                        _moonPhase = _lunarCycle.First;
                    else
                        _moonPhase = _moonPhase.Next;

                    break;

                case ACTOR_STATES.NIGHT:
                    //screen fades to black
                    startTimer0(240000); //4 minutes during midNight
                    _state = ACTOR_STATES.MIDNIGHT;
                    break;

                case ACTOR_STATES.MIDNIGHT:
                    startTimer0(60000); //1 minute during dawn
                    _state = ACTOR_STATES.DAWN;
                    break;

                case ACTOR_STATES.DAWN:
                    //screen fades to orange
                    startTimer0(120000); //2 minutes during morning
                    CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Background:Nature:Rooster"]);
                    _state = ACTOR_STATES.MORNING;
                    break;

                case ACTOR_STATES.MORNING:
                    //screen fades to white
                    startTimer0(480000); //5 minutes to a day
                    _state = ACTOR_STATES.DAY;
                    _timeOverlay.B = 255;
                    break;
            }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            //uncomment this line for demos
            enabled = false; 

            if (enabled)
            {

                switch (_state)
                {
                    case ACTOR_STATES.DUSK:
                        //fade to orange
                        //FF9000
                        //_internalCounter[0] += gameTime.ElapsedGameTime.Milliseconds * .00086;
                        //_internalCounter[1] += gameTime.ElapsedGameTime.Milliseconds * .00125;
                        //_internalCounter[2] += gameTime.ElapsedGameTime.Milliseconds * .00175;
                        _internalCounter[1] += gameTime.ElapsedGameTime.Milliseconds * .000925;
                        _internalCounter[2] += gameTime.ElapsedGameTime.Milliseconds * .002125;


                        if (_internalCounter[1] >= 1)
                        {
                            _timeOverlay.G -= (byte)1;
                            _internalCounter[1] = 0;
                        }
                        if (_internalCounter[2] >= 1)
                        {
                            _timeOverlay.B -= (byte)1;
                            _internalCounter[2] = 0;
                        }
                        break;

                    case ACTOR_STATES.NIGHT:
                        //fade to blue

                        //_internalCounter[0] += gameTime.ElapsedGameTime.Milliseconds * .00251;
                        //_internalCounter[1] += gameTime.ElapsedGameTime.Milliseconds * .000616;
                        //_internalCounter[2] += gameTime.ElapsedGameTime.Milliseconds * .00251;
                        _internalCounter[0] += gameTime.ElapsedGameTime.Milliseconds * .0030833333;
                        _internalCounter[1] += gameTime.ElapsedGameTime.Milliseconds * .0014;
                        _internalCounter[2] += gameTime.ElapsedGameTime.Milliseconds * .00425;

                        if (_internalCounter[0] >= 1)
                        {
                            _timeOverlay.R -= (byte)1;
                            _internalCounter[0] = 0;
                        }
                        if (_internalCounter[1] >= 1)
                        {
                            _timeOverlay.G -= (byte)1;
                            _internalCounter[1] = 0;
                        }
                        if (_internalCounter[2] >= 1)
                        {
                            if (_timeOverlay.B < 255)
                                _timeOverlay.B += (byte)1;

                            _internalCounter[2] = 0;
                        }
                        break;

                    case ACTOR_STATES.DAWN:
                        //back to orange
                        _internalCounter[0] += gameTime.ElapsedGameTime.Milliseconds * .0030833333;
                        _internalCounter[1] += gameTime.ElapsedGameTime.Milliseconds * .0014;
                        _internalCounter[2] += gameTime.ElapsedGameTime.Milliseconds * .00425;

                        if (_internalCounter[0] >= 1)
                        {
                            if (_timeOverlay.R < 255)
                                _timeOverlay.R += (byte)1;
                            _internalCounter[0] = 0;
                        }
                        if (_internalCounter[1] >= 1)
                        {
                            if (_timeOverlay.G < 255)
                                _timeOverlay.G += (byte)1;

                            _internalCounter[1] = 0;
                        }
                        if (_internalCounter[2] >= 1)
                        {
                            _timeOverlay.B -= (byte)1;
                            _internalCounter[2] = 0;
                        }
                        break;

                    case ACTOR_STATES.MORNING:
                        //back to white
                        _internalCounter[1] += gameTime.ElapsedGameTime.Milliseconds * .000925;
                        _internalCounter[2] += gameTime.ElapsedGameTime.Milliseconds * .002125;


                        if (_internalCounter[1] >= 1)
                        {
                            if (_timeOverlay.G < 255)
                             _timeOverlay.G += (byte)1;

                            _internalCounter[1] = 0;
                        }
                        if (_internalCounter[2] >= 1)
                        {
                            if (_timeOverlay.B < 255)
                                _timeOverlay.B += (byte)1;

                            _internalCounter[2] = 0;
                        }
                        break;
                }
            }
        }
    }
}
