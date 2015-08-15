using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using King_of_Thieves.Input;
using Gears.Cloud;

namespace King_of_Thieves.Actors.NPC.Other
{
    class CTownsFolk : CActor
    {
        private string _SPRITE_NAMESPACE = "";
        private const string _SHEET01 = "sprites/npc/friendly/townsfolk01";

        private string _WALK_UP = "walkUp";
        private string _WALK_DOWN = "walkDown";
        private string _WALK_LEFT = "walkLeft";
        private string _WALK_RIGHT = "walkRight";

        private string _IDLE_UP = "idleUp";
        private string _IDLE_DOWN = "idleDown";
        private string _IDLE_LEFT = "idleLeft";
        private string _IDLE_RIGHT = "idleRight";

        private int _backLineOfSight = 0;
        private int _backVisionRange = 0;

        private bool _hasPettyItem = false;
        private bool _itemGiven = false;
        private bool _playerInSight = false;

        protected double _backAngle = 0;

        public CTownsFolk() :
            base()
        {
            _SPRITE_NAMESPACE = "npc:townsfolk01";

            _WALK_UP = _SPRITE_NAMESPACE + _WALK_UP;
            _WALK_DOWN = _SPRITE_NAMESPACE + _WALK_DOWN;
            _WALK_LEFT = _SPRITE_NAMESPACE + _WALK_LEFT;
            _WALK_RIGHT = _SPRITE_NAMESPACE + _WALK_RIGHT;

            _IDLE_UP = _SPRITE_NAMESPACE + _IDLE_UP;
            _IDLE_DOWN = _SPRITE_NAMESPACE + _IDLE_DOWN;
            _IDLE_LEFT = _SPRITE_NAMESPACE + _IDLE_LEFT;
            _IDLE_RIGHT = _SPRITE_NAMESPACE + _IDLE_RIGHT;

            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.rawTextures.Add(_SPRITE_NAMESPACE, CMasterControl.glblContent.Load<Texture2D>(_SHEET01));

                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:2", "2:2", 4));
                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:0", "3:0", 4));
                Graphics.CTextures.addTexture(_WALK_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:1", "2:1", 4));

                Graphics.CTextures.addTexture(_IDLE_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:2", "0:2", 0));
                Graphics.CTextures.addTexture(_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:0", "0:0", 0));
                Graphics.CTextures.addTexture(_IDLE_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:1", "0:1", 0));
            }

            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_IDLE_UP));
            _imageIndex.Add(_IDLE_LEFT, new Graphics.CSprite(_IDLE_RIGHT,true));
            _imageIndex.Add(_IDLE_RIGHT, new Graphics.CSprite(_IDLE_RIGHT));

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_RIGHT, true));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_RIGHT));

            _direction = DIRECTION.DOWN;
            _angle = 270;
            _backAngle = 90;
            _state = ACTOR_STATES.IDLE;
            swapImage(_WALK_DOWN);
            _lineOfSight = 50;
            _visionRange = 60;
            _hearingRadius = 30;
            _backLineOfSight = 20;
            _backVisionRange = 50;

            _hitBox = new Collision.CHitBox(this, 10, 20, 16, 16);

            startTimer1(120);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if (_firstTick)
            {
                CTownsFolkHead head = new CTownsFolkHead();
                head.init(_name + "head", _position, "", this.componentAddress, _SPRITE_NAMESPACE);
                Map.CMapManager.addActorToComponent(head, this.componentAddress);
            }

            if (!_itemGiven)
            {
                _itemGiven = true;
                //if (_randNum.Next(100) >= 50)
                //{
                    _hasPettyItem = true;
                    indicator.CIndicatorPickpocketPetty petty = new indicator.CIndicatorPickpocketPetty();
                    petty.init(_name + "pickpocketPettyIndicator", new Microsoft.Xna.Framework.Vector2(_position.X + 5, _position.Y - 20), "", this.componentAddress);
                    Map.CMapManager.addActorToComponent(petty, this.componentAddress);

                    Actors.Items.Drops.CRupeeDrop rupeeDrop = new Items.Drops.CRupeeDrop();
                    rupeeDrop.init(this.name + "loadedItem", _position, "", this.componentAddress, "G", "false", "false");
                    Map.CMapManager.addActorToComponent(rupeeDrop, this.componentAddress);
                //}
            }
            if (_state == ACTOR_STATES.MOVING)
                moveInDirection(_velocity);

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
            _firstTick = false;
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
                        _hasPettyItem = false;
                        _backLineOfSight = 0;
                        _backVisionRange = 0;
                        CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.NONE);
                    }
                    else
                    {
                        startTimer3(2);
                        _triggerUserEvent(0, this.name + "pickpocketPettyIndicator");
                        _hasPettyItem = false;
                        _backLineOfSight = 0;
                        _backVisionRange = 0;
                    }
                    _state = ACTOR_STATES.IDLE;
                }

                if (!CMasterControl.buttonController.textBoxWait)
                {
                    if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.TALK && _state == ACTOR_STATES.TALK_READY)
                        startTimer0(2);
                }

                if(CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.PICK && _state == ACTOR_STATES.PICK_READY && CMasterControl.pickPocketMeter == null)
                    startTimer2(2);
               
            }
        }

        public override void timer0(object sender)
        {
            CMasterControl.buttonController.createTextBox("Bah! Out of my way, filth!!");
        }

        public override void timer1(object sender)
        {
            if (_state != ACTOR_STATES.BEING_PICKED)
                _changeDirection();

            startTimer1(120);
        }

        public override void timer2(object sender)
        {
            CMasterControl.pickPocketMeter = new HUD.other.CPickPocketMeter(3);
            _state = ACTOR_STATES.BEING_PICKED;
        }

        public override void timer3(object sender)
        {
            CMasterControl.buttonController.createTextBox("?! Y-YOU LITTLE!! GUARDS!! SEIZE HIM!!!");
            HUD.notoriety.CNotorietyIcon.raiseNotoriety();
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

        private void _changeDirection()
        {
            _direction = (DIRECTION)_randNum.Next(0, 5);
            _state = _randNum.Next(0, 100) >= 50 ? ACTOR_STATES.IDLE : ACTOR_STATES.MOVING;

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _angle = 270;
                    _backAngle = 90;
                    _velocity = new Vector2(0, .25f);

                    if (_state == ACTOR_STATES.MOVING)
                        swapImage(_WALK_DOWN);
                    else
                        swapImage(_IDLE_DOWN);

                    break;

                case DIRECTION.LEFT:
                    _angle = 180;
                    _backAngle = 0;
                    _velocity = new Vector2(-.25f, 0);

                    if (_state == ACTOR_STATES.MOVING)
                        swapImage(_WALK_LEFT);
                    else
                        swapImage(_IDLE_LEFT);

                    break;

                case DIRECTION.RIGHT:
                    _angle = 0;
                    _backAngle = 180;
                    _velocity = new Vector2(.25f, 0);

                    if (_state == ACTOR_STATES.MOVING)
                        swapImage(_WALK_RIGHT);
                    else
                        swapImage(_IDLE_RIGHT);

                    break;

                case DIRECTION.UP:
                    _angle = 90;
                    _backAngle = 270;
                    _velocity = new Vector2(0, -.25f);

                    if (_state == ACTOR_STATES.MOVING)
                        swapImage(_WALK_UP);
                    else
                        swapImage(_IDLE_UP);

                    break;
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

        private bool _hasItemToPick
        {
            get
            {
                return (_hasPettyItem);
            }
        }
    }
}
