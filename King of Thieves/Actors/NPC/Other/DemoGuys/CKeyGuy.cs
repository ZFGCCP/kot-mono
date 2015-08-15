using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

            _hitBox = new Collision.CHitBox(this, 10, 20, 16, 16);

            _imageIndex.Add(_FACE_DOWN, new Graphics.CSprite(Graphics.CTextures.DEMO_FOLK_KEY_GUY_DOWN));
            _imageIndex.Add(_FACE_LEFT, new Graphics.CSprite(Graphics.CTextures.DEMO_FOLK_KEY_GUY_LEFT));
            _imageIndex.Add(_FACE_RIGHT, new Graphics.CSprite(Graphics.CTextures.DEMO_FOLK_KEY_GUY_LEFT, true));
            _imageIndex.Add(_FACE_UP, new Graphics.CSprite(Graphics.CTextures.DEMO_FOLK_KEY_GUY_UP));

            swapImage(_FACE_LEFT);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            if (MathExt.MathExt.checkPointInCircle(playerPos, _position, _hearingRadius))
            {
                if (_checkIfPointInView(playerPos) && checkIfFacing(playerPos, Player.CPlayer.glblDirection) && _state != ACTOR_STATES.BEING_PICKED)
                {
                    _state = ACTOR_STATES.TALK_READY;
                    CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.TALK);
                    _playerInSight = true;
                }
                else if (_checkIfPointBehind(playerPos) && checkIfBackFacing(playerPos, Player.CPlayer.glblDirection) && _hasItemToPick && _state != ACTOR_STATES.BEING_PICKED)
                {
                    CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.PICK);
                    _state = ACTOR_STATES.PICK_READY;
                }
                else
                {
                    if (_playerInSight && (_state == ACTOR_STATES.PICK_READY || _state == ACTOR_STATES.TALK_READY))
                    {
                        _state = ACTOR_STATES.IDLE;
                        CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.NONE);
                        _playerInSight = false;
                    }
                }
            }
        }

        private bool _checkIfPointBehind(Vector2 point)
        {

            //build triangle points first
            Vector2 A = _position;
            Vector2 B = Vector2.Zero;
            Vector2 C = Vector2.Zero;

            B.X = (float)(Math.Cos((_backAngle - _backVisionRange / 2.0f) * (Math.PI / 180)) * _backLineOfSight) + _position.X;
            B.Y = (float)((Math.Sin((_backAngle - _backVisionRange / 2.0f) * (Math.PI / 180)) * _backLineOfSight) * -1.0) + _position.Y;

            C.X = (float)(Math.Cos((_backAngle + _backVisionRange / 2.0f) * (Math.PI / 180)) * _backLineOfSight) + _position.X;
            C.Y = (float)((Math.Sin((_backAngle + _backVisionRange / 2.0f) * (Math.PI / 180)) * _backLineOfSight) * -1.0) + _position.Y;

            return MathExt.MathExt.checkPointInTriangle(point, A, B, C);
        }


        public bool checkIfBackFacing(Vector2 position, DIRECTION direction)
        {
            if (_position.X >= position.X)
            {
                if (_direction == DIRECTION.RIGHT && direction == DIRECTION.RIGHT)
                    return true;
            }

            if (_position.X <= position.X)
            {
                if (_direction == DIRECTION.LEFT && direction == DIRECTION.LEFT)
                    return true;
            }

            if (_position.Y <= position.Y)
            {
                if (_direction == DIRECTION.UP && direction == DIRECTION.UP)
                    return true;
            }

            if (_position.Y >= position.Y)
            {
                if (_direction == DIRECTION.DOWN && direction == DIRECTION.DOWN)
                    return true;
            }
            return false;
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
            if (collider is Collision.CSolidTile)
                solidCollide(collider);
        }
    }
}
