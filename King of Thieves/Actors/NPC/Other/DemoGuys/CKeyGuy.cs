using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Other.DemoGuys
{
    class CKeyGuy : CActor
    {
        private int _backLineOfSight = 0;
        private int _backVisionRange = 0;

        protected double _backAngle = 0;
        private bool _playerInSight = false;
        private bool _hasItemToPick = true;

        private const string _SPRITE_NAMESPACE = "demoFolk:";
        private const string _FACE_DOWN = _SPRITE_NAMESPACE + "faceDown";
        private const string _FACE_LEFT = _SPRITE_NAMESPACE + "faceLeft";
        private const string _FACE_RIGHT = _SPRITE_NAMESPACE + "faceRight";
        private const string _FACE_UP = _SPRITE_NAMESPACE + "faceUp";

        public CKeyGuy() :
            base()
        {
            _direction = DIRECTION.DOWN;
            _angle = 270;
            _backAngle = 90;
            _state = ACTOR_STATES.IDLE;
            _lineOfSight = 50;
            _visionRange = 60;
            _hearingRadius = 30;
            _backLineOfSight = 20;
            _backVisionRange = 50;
        }
    }
}
