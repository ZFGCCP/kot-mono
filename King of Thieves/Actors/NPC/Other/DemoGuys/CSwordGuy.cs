using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Gears.Cloud;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Other.DemoGuys
{
    class CSwordGuy : CActor
    {
        private int _backLineOfSight = 0;
        private int _backVisionRange = 0;

        protected double _backAngle = 0;
        private bool _playerInSight = false;
        private bool _hasItemToPick = true;

        public CSwordGuy()
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
        }

        public override void roomStart(object sender)
        {
            indicator.CIndicatorPickpocketPetty petty = new indicator.CIndicatorPickpocketPetty();
            petty.init(_name + "swordIndicator", new Microsoft.Xna.Framework.Vector2(_position.X + 5, _position.Y - 20), "", this.componentAddress);
            Map.CMapManager.addActorToComponent(petty, this.componentAddress);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            if (MathExt.MathExt.checkPointInCircle(playerPos, _position, _hearingRadius))
            {
                if (_checkIfPointInView(playerPos) && checkIfFacing(playerPos, Player.CPlayer.glblDirection))
                {
                    _state = ACTOR_STATES.TALK_READY;
                    CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.TALK);
                    _playerInSight = true;
                }
                else if (_checkIfPointBehind(playerPos) && checkIfBackFacing(playerPos, Player.CPlayer.glblDirection) && _hasItemToPick)
                {
                    CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.PICK);
                }
                else
                {
                    if (_playerInSight)
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
                        _triggerUserEvent(0, this.name + "loadedItem");
                        _triggerUserEvent(0, this.name + "pickpocketPettyIndicator");
                        _hasItemToPick = false;
                        _backLineOfSight = 0;
                        _backVisionRange = 0;
                        CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.NONE);
                    }
                    else
                    {
                        startTimer3(2);
                        _triggerUserEvent(0, this.name + "pickpocketPettyIndicator");
                        _hasItemToPick = false;
                        _backLineOfSight = 0;
                        _backVisionRange = 0;
                    }
                    _state = ACTOR_STATES.IDLE;
                }

                if (!CMasterControl.buttonController.textBoxWait)
                {
                    if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.TALK)
                        startTimer0(2);
                }

                if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.PICK && CMasterControl.pickPocketMeter == null)
                    startTimer2(2);

            }
        }

        public override void timer3(object sender)
        {
            CMasterControl.buttonController.createTextBox("HEY!! BACK OFF, KID!!!");
        }
    }
}
