using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;

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

        public override void roomStart(object sender)
        {
            if (_firstTick)
            {
                indicator.CIndicatorPickpocketPetty petty = new indicator.CIndicatorPickpocketPetty();
                petty.init(_name + "keyIndicator", new Microsoft.Xna.Framework.Vector2(_position.X + 5, _position.Y - 40), "", this.componentAddress);
                Map.CMapManager.addActorToComponent(petty, this.componentAddress);

                CActor head = new CKeyGuyHead();
                head.init(_name + "head", _position, "", this.componentAddress);
                Map.CMapManager.addActorToComponent(head, this.componentAddress);

                _firstTick = false;
            }

            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["bgm:kakariko"]);

            startTimer4(120);
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

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Collision.CSolidTile));
        }

        public override void keyRelease(object sender)
        {
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
            if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.C))
            {

                if (_state == ACTOR_STATES.BEING_PICKED)
                {
                    if (CMasterControl.pickPocketMeter.amount >= 50)
                    {
                        //pick success
                        _triggerUserEvent(0, this.name + "keyIndicator");
                        _hasItemToPick = false;
                        _backLineOfSight = 0;
                        _backVisionRange = 0;
                        CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.NONE);
                        CMasterControl.buttonController.giveKey();
                    }
                    _state = ACTOR_STATES.IDLE;
                }

                if (!CMasterControl.buttonController.textBoxWait)
                {
                    if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.TALK && _state == ACTOR_STATES.TALK_READY)
                        startTimer0(2);
                }

                if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.PICK && _state == ACTOR_STATES.PICK_READY && CMasterControl.pickPocketMeter == null)
                    startTimer2(2);

            }
        }

        public override void timer0(object sender)
        {
            CMasterControl.buttonController.createTextBox("Get out of my sight!");
        }

        public override void timer2(object sender)
        {
            CMasterControl.pickPocketMeter = new HUD.other.CPickPocketMeter(3);
            _state = ACTOR_STATES.BEING_PICKED;
        }

        public override void timer4(object sender)
        {
            if (_state == ACTOR_STATES.IDLE)
            {
                _direction = (DIRECTION)_randNum.Next(0, 3);

                switch (_direction)
                {
                    case DIRECTION.DOWN:
                        _angle = 270;
                        _backAngle = 90;
                        swapImage(_FACE_DOWN);
                        break;

                    case DIRECTION.LEFT:
                        _angle = 180;
                        _backAngle = 0;
                        swapImage(_FACE_LEFT);
                        break;

                    case DIRECTION.RIGHT:
                        _angle = 0;
                        _backAngle = 180;
                        swapImage(_FACE_RIGHT);
                        break;

                    case DIRECTION.UP:
                        _angle = 90;
                        _backAngle = 270;
                        swapImage(_FACE_UP);
                        break;
                }
            }

            startTimer4(120);
        }
    }
}
