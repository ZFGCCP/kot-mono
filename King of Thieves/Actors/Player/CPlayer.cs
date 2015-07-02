using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework.Input;
using King_of_Thieves.Actors.Collision;

namespace King_of_Thieves.Actors.Player
{
    class CPlayer : CActor
    {
        private bool _swordReleased = true;
        private static double _readableAngle = 0;
        private static Vector2 _readableCoords = new Vector2();
        private static DIRECTION _readableDirection = DIRECTION.DOWN;
        public static readonly Vector2 carrySpot = new Vector2(-6, -10); //will need to be played with
        private bool _carrying = false;
        private double _carryWeight = 0;
        private bool _acceptInput = true;
        private bool _usingItem = false;
        private Keys _lastHudKeyPressed = Keys.None;
        private string _lastArrowShotName = "";
        private string _lastBombShotName = "";
        private const int _MAX_BOMB_VELO = 5;
        private int _bombVelo = 0;
        private string _currentShieldSprite = "";
        private string _currentShieldIdleSprite = "";
        private Projectiles.ARROW_TYPES _arrowType = Projectiles.ARROW_TYPES.STANDARD;
        private bool _wearingShadowCloak = false;
        public bool cloneExists = false;
        private bool _canMoveClone = false;

        private const string _THROW_BOOMERANG_DOWN = "PlayerThrowBoomerangDown";
        private const string _THROW_BOOMERANG_UP = "PlayerThrowBoomerangUp";
        private const string _THROW_BOOMERANG_LEFT = "PlayerThrowBoomerangLeft";
        private const string _THROW_BOOMERANG_RIGHT = "PlayerThrowBoomerangRight";
        private const string _GOT_ITEM = "PlayerGotItem";
        private bool _canOpenManu = false;

        public CPlayer() :
            base()
        {

            _name = "Player";
            _position = Vector2.Zero;
            //resource init
            _hitBox = new Collision.CHitBox(this, 10, 18, 12, 15);

            
            image = _imageIndex[Graphics.CTextures.PLAYER_IDLEDOWN];
            _velocity = new Vector2(0, 0);
            startTimer5(15);
            _drawDepth = 9;
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
        }

        protected override void _initializeResources()
        {

            //We're creating A LOT of strings here -- This needs to be handled with a constants class or something
            base._initializeResources();
            _imageIndex.Add(_MAP_ICON, new Graphics.CSprite(Graphics.CTextures.PLAYER_WALKDOWN));

            _imageIndex.Add(Graphics.CTextures.PLAYER_WALKDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_WALKDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_WALKLEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_WALKLEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_WALKRIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_WALKLEFT, true));
            _imageIndex.Add(Graphics.CTextures.PLAYER_WALKUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_WALKUP));

            _imageIndex.Add(Graphics.CTextures.PLAYER_IDLEDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_IDLEDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_IDLEUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_IDLEUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_IDLELEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_IDLELEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_IDLERIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_IDLELEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_SWINGUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_SWINGUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SWINGDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SWINGDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SWINGLEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SWINGLEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SWINGRIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SWINGLEFT,true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_ROLLDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_ROLLDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_ROLLUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_ROLLUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_ROLLLEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_ROLLLEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_ROLLRIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_ROLLLEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_LIFTDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_LIFTDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_LIFTUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_LIFTUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_LIFTLEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_LIFTLEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_LIFTRIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_LIFTLEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_CARRYDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_CARRYDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_CARRYUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_CARRYUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_CARRYLEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_CARRYLEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_CARRYRIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_CARRYLEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_LIFTIDLEDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_LIFTIDLEDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_LIFTIDLEUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_LIFTIDLEUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_LIFTIDLELEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_LIFTIDLELEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_LIFTIDLERIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_LIFTIDLELEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_THROWDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROWDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_THROWUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROWUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_THROWLEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROWLEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_THROWRIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROWLEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOCKDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOCKDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOCKUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOCKUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOCKLEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOCKLEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOCKRIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOCKLEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_FREEZEDOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_FREEZEDOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_FREEZEUP, new Graphics.CSprite(Graphics.CTextures.PLAYER_FREEZEUP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_FREEZELEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_FREEZELEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_FREEZERIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_FREEZELEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_CHARGE_ARROW_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_CHARGE_ARROW_DOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_CHARGE_ARROW_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_CHARGE_ARROW_UP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_CHARGE_ARROW_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_CHARGE_ARROW_LEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_CHARGE_ARROW_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_CHARGE_ARROW_LEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_HOLD_ARROW_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_ARROW_DOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_HOLD_ARROW_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_ARROW_UP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_HOLD_ARROW_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_ARROW_LEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_HOLD_ARROW_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_ARROW_LEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOOT_ARROW_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_ARROW_DOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOOT_ARROW_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_ARROW_UP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOOT_ARROW_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_ARROW_LEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOOT_ARROW_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_ARROW_LEFT, true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_HOLD_CANNON_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_CANNON_DOWN));

            _imageIndex.Add(Graphics.CTextures.PLAYER_SHOOT_CANNON_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_CANNON_DOWN));

            _imageIndex.Add(_THROW_BOOMERANG_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROW_BOOMERANG_DOWN));
            _imageIndex.Add(_THROW_BOOMERANG_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROW_BOOMERANG_LEFT));
            _imageIndex.Add(_THROW_BOOMERANG_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROW_BOOMERANG_LEFT,true));
            _imageIndex.Add(_THROW_BOOMERANG_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROW_BOOMERANG_UP));

            _imageIndex.Add(_GOT_ITEM, new Graphics.CSprite(Graphics.CTextures.PLAYER_GOT_ITEM));

            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_DOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_DOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_WALK_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_WALK_DOWN));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_IDLE_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_IDLE_DOWN));

            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_LEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_LEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_WALK_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_WALK_LEFT));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_IDLE_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_IDLE_LEFT));

            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_LEFT,true));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_LEFT,true));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_WALK_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_WALK_LEFT,true));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_IDLE_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_IDLE_LEFT,true));

            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_UP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_UP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_WALK_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_WALK_UP));
            _imageIndex.Add(Graphics.CTextures.PLAYER_SHIELD_IDLE_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_SHIELD_IDLE_UP));
        }

        public override void timer5(object sender)
        {
            base.timer5(sender);
            _canOpenManu = true;
        }

        public override void collide(object sender, CActor collider)
        {
            if (!collider.noCollide && (collider is CSolidTile || collider is Items.decoration.CChest))
            {
                solidCollide(collider);
            }
            else
            {
                //every enemy should have some knockback.
                //we'll add an attribute here eventually for enemies that
                //don't (ex beamos)
                //As for damage, it will ultimately be up to the
                //enemy actor to handle
                if (!INVINCIBLE_STATES.Contains(_state))
                {
                    if (collider is NPC.Enemies.CBaseEnemy ||
                        collider is Projectiles.CEnergyWave||
                        collider is Projectiles.CFireBall)
                    {
                        if (!(collider is NPC.Enemies.Zombie.CBaseZombie))
                            _collideWithNpcResponse(collider);
                    }
                    else if (collider is Projectiles.CIceBall)
                        _collideWithNpcResponse(collider, false);
                }

                if (collider is NPC.Other.CTownsFolk)
                {
                    solidCollide(collider);
                }
            }
        }

        private void _destroyHeldItems()
        {
            if (_lastArrowShotName != string.Empty)
                CMasterControl.commNet[componentAddress].Add(new CActorPacket(1000, _lastArrowShotName, this));
        }

        private void _collideWithNpcResponse(CActor collider, bool knockBack = true)
        {
            //start a moveback timer
            //change state to knockBack
            startTimer0(10);
            if (knockBack)
                _state = ACTOR_STATES.KNOCKBACK;

            _acceptInput = false;
            solidCollide(collider, true);
            _destroyHeldItems();
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();
        }

        public override void create(object sender)
        {
            throw new NotImplementedException();
        }

        public override void destroy(object sender)
        {
            throw new NotImplementedException();
        }

        public override void draw(object sender)
        {
        }

        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case ACTOR_STATES.SWINGING:
                    _state = ACTOR_STATES.IDLE;
                    break;

                case ACTOR_STATES.ROLLING:
                    _state = ACTOR_STATES.IDLE;
                    break;

                case ACTOR_STATES.LIFT:
                    _state = ACTOR_STATES.IDLE;
                    _carrying = true;
                    break;

                case ACTOR_STATES.THROWING:
                    _state = ACTOR_STATES.IDLE;
                    break;

                case ACTOR_STATES.CHARGING_ARROW:
                    _holdArrow();
                    break;

                case ACTOR_STATES.SHOOTING_ARROW:
                    _state = ACTOR_STATES.IDLE;
                    break;

                case ACTOR_STATES.SHOOTING_CANNON:
                    _state = ACTOR_STATES.IDLE;
                    break;

                case ACTOR_STATES.THROW_BOOMERANG:
                    _state = ACTOR_STATES.IDLE;
                    Projectiles.CBoomerang boomerang = new Projectiles.CBoomerang(_oldVelocity, position, direction, 2);
                    Map.CMapManager.addActorToComponent(boomerang, this.componentAddress);
                    _usingItem = false;
                    break;

                case ACTOR_STATES.SHIELD_ENGAGE:
                    _state = ACTOR_STATES.SHIELDING;
                    _acceptInput = true;

                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            _currentShieldSprite = Graphics.CTextures.PLAYER_SHIELD_WALK_DOWN;
                            _currentShieldIdleSprite = Graphics.CTextures.PLAYER_SHIELD_IDLE_DOWN;
                            break;

                        case DIRECTION.LEFT:
                            _currentShieldSprite = Graphics.CTextures.PLAYER_SHIELD_WALK_LEFT;
                            _currentShieldIdleSprite = Graphics.CTextures.PLAYER_SHIELD_IDLE_LEFT;
                            break;

                        case DIRECTION.RIGHT:
                            _currentShieldSprite = Graphics.CTextures.PLAYER_SHIELD_WALK_RIGHT;
                            _currentShieldIdleSprite = Graphics.CTextures.PLAYER_SHIELD_IDLE_RIGHT;
                            break;

                        case DIRECTION.UP:
                            _currentShieldSprite = Graphics.CTextures.PLAYER_SHIELD_WALK_UP;
                            _currentShieldIdleSprite = Graphics.CTextures.PLAYER_SHIELD_IDLE_UP;
                            break;

                        default:
                            break;
                    }
                    break;

                case ACTOR_STATES.SHIELD_DISENGAGE:
                    _state = ACTOR_STATES.IDLE;
                    break;
            }

            
        }

        public override void keyDown(object sender)
        {
            if (_acceptInput)
            {
                //Store this so we can type less
                CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
                if (_state == ACTOR_STATES.IDLE || _state == ACTOR_STATES.MOVING)
                {
                    if (input.keysPressed.Contains(Keys.End))
                    {
                        Graphics.CGraphics.changeResolution(320, 240);
                        Master.Pop();
                    }

                    if (input.keysPressed.Contains(Keys.Left) && _lastHudKeyPressed != Keys.Left)
                        _useItem(0);
                    else if (input.keysPressed.Contains(Keys.Right) && _lastHudKeyPressed != Keys.Right)
                        _useItem(-1);
                    else if (input.keysPressed.Contains(Keys.LeftShift))
                    {
                        if (_state != ACTOR_STATES.SHIELDING && _state != ACTOR_STATES.SHIELD_ENGAGE)
                        {
                            _triggerUserEvent(0, "shield", _direction, _position.X, _position.Y);
                            _state = ACTOR_STATES.SHIELD_ENGAGE;

                            switch (_direction)
                            {
                                case DIRECTION.DOWN:
                                    swapImage(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_DOWN);
                                    break;

                                case DIRECTION.LEFT:
                                    swapImage(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_LEFT);
                                    break;

                                case DIRECTION.RIGHT:
                                    swapImage(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_RIGHT);
                                    break;

                                case DIRECTION.UP:
                                    swapImage(Graphics.CTextures.PLAYER_SHIELD_ENGAGE_UP);
                                    break;
                            }
                            
                            _acceptInput = false;
                            return;
                        }
                    }

                    if (!_usingItem)
                    {
                        if (input.keysPressed.Contains(Keys.A))
                        {
                            if (_carrying)
                            {
                                swapImage(Graphics.CTextures.PLAYER_CARRYLEFT);
                                _velocity.X = -1;
                                _state = ACTOR_STATES.MOVING;
                            }
                            else
                            {
                                image = _imageIndex[Graphics.CTextures.PLAYER_WALKLEFT];
                                _velocity.X = -1;
                                _state = ACTOR_STATES.MOVING;
                            }

                            _direction = DIRECTION.LEFT;
                        }

                        if (input.keysPressed.Contains(Keys.D))
                        {
                            if (_carrying)
                            {
                                swapImage(Graphics.CTextures.PLAYER_CARRYRIGHT);
                                _velocity.X = 1;
                                _state = ACTOR_STATES.MOVING;
                            }
                            else
                            {
                                image = _imageIndex[Graphics.CTextures.PLAYER_WALKRIGHT];
                                _velocity.X = 1;
                                _state = ACTOR_STATES.MOVING;
                            }

                            _direction = DIRECTION.RIGHT;
                        }

                        if (input.keysPressed.Contains(Keys.W))
                        {
                            if (_carrying)
                            {
                                _velocity.Y = -1;
                                swapImage(Graphics.CTextures.PLAYER_CARRYUP);
                                _state = ACTOR_STATES.MOVING;
                            }
                            else
                            {
                                _velocity.Y = -1;
                                image = _imageIndex[Graphics.CTextures.PLAYER_WALKUP];
                                _state = ACTOR_STATES.MOVING;
                            }

                            _direction = DIRECTION.UP;
                        }

                        if (input.keysPressed.Contains(Keys.S))
                        {
                            
                            if (_carrying)
                            {
                                swapImage(Graphics.CTextures.PLAYER_CARRYDOWN);
                                _velocity.Y = 1;
                                _state = ACTOR_STATES.MOVING;
                            }
                            else
                            {
                                image = _imageIndex[Graphics.CTextures.PLAYER_WALKDOWN];
                                _velocity.Y = 1;
                                _state = ACTOR_STATES.MOVING;
                            }

                            _direction = DIRECTION.DOWN;
                        }

                        moveInDirection(_velocity);
                        _oldVelocity = _velocity;
                    }
                    if (input.keysPressed.Contains(Keys.Space))
                    {
                        if (_wearingShadowCloak)
                            _createShadowClone();
                        else
                        {
                            _state = ACTOR_STATES.SWINGING;
                            _swordReleased = false;
                        }
                    }
                }
                else if (_state == ACTOR_STATES.SHIELDING)
                {
                    if (input.keysPressed.Contains(Keys.A))
                        _velocity.X = -.5f;
                    if (input.keysPressed.Contains(Keys.D))
                        _velocity.X = .5f;
                    if (input.keysPressed.Contains(Keys.S))
                        _velocity.Y = .5f;
                    if (input.keysPressed.Contains(Keys.W))
                        _velocity.Y = -.5f;

                    if (_velocity.X == 0 && _velocity.Y == 0)
                        swapImage(_currentShieldIdleSprite);
                    else
                        swapImage(_currentShieldSprite);

                    moveInDirection(_velocity);
                    _oldVelocity = _velocity;
                }
            }
            _velocity.X = 0;
            _velocity.Y = 0;
        }

        public override void keyRelease(object sender)
        {
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
            if (_acceptInput)
            {
                if (input.keysReleased.Contains(Keys.T))
                {
                    /*Items.Drops.CRupeeDrop rupee = new Items.Drops.CRupeeDrop();
                    rupee.init("rupeeTest", _position - new Vector2(20, 20), "", this.componentAddress, "P");
                    Map.CMapManager.addActorToComponent(rupee, this.componentAddress);*/
                    CMasterControl.buttonController.modifyBombs(10);
                }

                if (!(Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysPressed)
                {
                    if (_state == ACTOR_STATES.MOVING)
                        _state = ACTOR_STATES.IDLE;
                }

                if (input.keysReleased.Contains(Keys.Left) || input.keysReleased.Contains(Keys.Right))
                {
                    switch (state)
                    {
                        case ACTOR_STATES.CHARGING_ARROW:
                            _queueUpArrow(ref _arrowType);
                            _shootArrow();
                            break;

                        case ACTOR_STATES.HOLD_ARROW:
                            _shootArrow();
                            break;

                        case ACTOR_STATES.HOLD_CANNON:
                            _shootCannon();
                            break;
                    }

                    _lastHudKeyPressed = Keys.None;
                }

                if (input.keysReleased.Contains(Keys.LeftShift))
                {
                    if (_state == ACTOR_STATES.SHIELDING || _state == ACTOR_STATES.SHIELD_ENGAGE)
                    {
                        _triggerUserEvent(1, "shield", _direction, _position.X, _position.Y);
                        switch (_direction)
                        {
                            case DIRECTION.DOWN:
                                swapImage(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_DOWN);
                                break;

                            case DIRECTION.LEFT:
                                swapImage(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_LEFT);
                                break;

                            case DIRECTION.RIGHT:
                                swapImage(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_RIGHT);
                                break;

                            case DIRECTION.UP:
                                swapImage(Graphics.CTextures.PLAYER_SHIELD_DISENGAGE_UP);
                                break;
                        }
                        _state = ACTOR_STATES.SHIELD_DISENGAGE;
                    }
                }

                if (input.keysReleased.Contains(Keys.C))
                {
                    //check HUD state
                    if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.OPEN)
                    {
                        _state = ACTOR_STATES.GOT_ITEM;
                        swapImage(_GOT_ITEM);
                        _acceptInput = false;
                    }
                    else if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.TALK && !CMasterControl.buttonController.textBoxWait)
                    {
                        _state = ACTOR_STATES.TALK_READY;
                        _acceptInput = false;
                    }
                    else if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.PICK)
                    {
                        _state = ACTOR_STATES.PICKING;
                        _acceptInput = false;
                    }


                    if (_state == ACTOR_STATES.MOVING)
                    {
                        if (_carrying)
                        {
                            _triggerUserEvent(0, "carryMe", _direction);
                            _state = ACTOR_STATES.THROWING;

                            switch (_direction)
                            {
                                case DIRECTION.DOWN:
                                    swapImage(Graphics.CTextures.PLAYER_THROWDOWN);
                                    break;

                                case DIRECTION.UP:
                                    swapImage(Graphics.CTextures.PLAYER_THROWUP);
                                    break;

                                case DIRECTION.LEFT:
                                    swapImage(Graphics.CTextures.PLAYER_THROWLEFT);
                                    break;

                                case DIRECTION.RIGHT:
                                    swapImage(Graphics.CTextures.PLAYER_THROWRIGHT);
                                    break;
                            }

                            _carrying = false;
                            return;
                        }
                        _state = ACTOR_STATES.ROLLING;
                        CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Player:Attack3"]);
                        switch (_direction)
                        {
                            case DIRECTION.DOWN:
                                swapImage(Graphics.CTextures.PLAYER_ROLLDOWN);
                                break;

                            case DIRECTION.UP:
                                swapImage(Graphics.CTextures.PLAYER_ROLLUP);
                                break;

                            case DIRECTION.LEFT:
                                swapImage(Graphics.CTextures.PLAYER_ROLLLEFT);
                                break;

                            case DIRECTION.RIGHT:
                                swapImage(Graphics.CTextures.PLAYER_ROLLRIGHT);
                                break;
                        }
                        //get the FUCK out of this
                        return;
                    }
                }


                if (_state == ACTOR_STATES.HOLD_ARROW && input.keysReleased.Contains(Keys.R))
                {
                    switch (CMasterControl.buttonController.buttonLeftItem)
                    {
                        case HUD.buttons.HUDOPTIONS.ARROWS:
                            CMasterControl.buttonController.switchLeftItem(HUD.buttons.HUDOPTIONS.FIRE_ARROWS);
                            _changeArrowType(Projectiles.ARROW_TYPES.FIRE);
                            break;

                        case HUD.buttons.HUDOPTIONS.FIRE_ARROWS:
                            CMasterControl.buttonController.switchLeftItem(HUD.buttons.HUDOPTIONS.ICE_ARROWS);
                            _changeArrowType(Projectiles.ARROW_TYPES.ICE);
                            break;

                        case HUD.buttons.HUDOPTIONS.ICE_ARROWS:
                            CMasterControl.buttonController.switchLeftItem(HUD.buttons.HUDOPTIONS.ARROWS);
                            _changeArrowType(Projectiles.ARROW_TYPES.STANDARD);
                            break;
                    }

                    
                }
            }

            if (_canOpenManu && input.keysReleased.Contains(Keys.Enter))
                Master.Push(new usr.local.GameMenu.CPauseMenu(CMasterControl.itemPauseMenu(), CMasterControl.questPauseMenu()));
        }



        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            _velocity.X = 0;
            _velocity.Y = 0;

            switch (_state)
            {
                case ACTOR_STATES.GOT_ITEM:
                    if (Actors.HUD.Text.CTextBox.messageFinished)
                    {
                        _state = ACTOR_STATES.IDLE;
                        _direction = DIRECTION.DOWN;
                        CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.NONE);
                        _angle = 270;
                        _acceptInput = true;
                        swapImage(Graphics.CTextures.PLAYER_IDLEDOWN);
                    }
                    break;

                case ACTOR_STATES.TALK_READY:
                    if (Actors.HUD.Text.CTextBox.messageFinished)
                    {
                        _state = ACTOR_STATES.IDLE;
                        CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.NONE);
                        _acceptInput = true;
                    }
                    break;

                case ACTOR_STATES.PICKING:
                    if (CMasterControl.pickPocketMeter != null && CMasterControl.pickPocketMeter.justFinished)
                    {
                        _state = ACTOR_STATES.IDLE;
                        CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.NONE);
                        _acceptInput = true;
                    }
                    break;

                case ACTOR_STATES.LIFT:
                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            swapImage(Graphics.CTextures.PLAYER_LIFTDOWN);
                            break;

                        case DIRECTION.UP:
                            swapImage(Graphics.CTextures.PLAYER_LIFTUP);
                            break;

                        case DIRECTION.LEFT:
                            swapImage(Graphics.CTextures.PLAYER_LIFTLEFT);
                            break;

                        case DIRECTION.RIGHT:
                            swapImage(Graphics.CTextures.PLAYER_LIFTRIGHT);
                            break;

                    }
                    
                    break;

                case ACTOR_STATES.ROLLING:
                    Vector2 rollVelo = Vector2.Zero;
                    switch (_direction)
                    {

                        case DIRECTION.DOWN:
                            rollVelo.Y += 2;
                            break;

                        case DIRECTION.UP:
                            rollVelo.Y -= 2;
                            break;

                        case DIRECTION.LEFT:
                            rollVelo.X -= 2;
                            break;

                        case DIRECTION.RIGHT:
                            rollVelo.X += 2;
                            break;
                    }
                    moveInDirection(rollVelo);
                    break;

                case ACTOR_STATES.KNOCKBACK:

                    if (_collisionDirectionX != 0 && _collisionDirectionY != 0)
                    {
                        _position.X += (float)Math.Sqrt(4 * _collisionDirectionX);
                        _position.Y += (float)Math.Sqrt(4 * _collisionDirectionY);
                    }
                    else if (_collisionDirectionX != 0)
                        _position.X += (float)(4 * _collisionDirectionX);

                    else if (_collisionDirectionY != 0)
                        _position.Y += (float)(4 * _collisionDirectionY);

                    break;

                case ACTOR_STATES.SWINGING:
                    if (!_swordReleased)
                    {
                        _swordReleased = true;
                        Vector2 swordPos = Vector2.Zero;
                        Random random = new Random();
                        int attackSound = random.Next(0, 3);

                        Sound.CSound[] temp = new Sound.CSound[4];

                        temp[0] = CMasterControl.audioPlayer.soundBank["Player:Attack1"];
                        temp[1] = CMasterControl.audioPlayer.soundBank["Player:Attack2"];
                        temp[2] = CMasterControl.audioPlayer.soundBank["Player:Attack3"];
                        temp[3] = CMasterControl.audioPlayer.soundBank["Player:Attack4"];

                        CMasterControl.audioPlayer.addSfx(temp[attackSound]);
                        CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Player:SwordSlash"]);

                        switch (_direction)
                        {
                            case DIRECTION.UP:
                                swapImage(Graphics.CTextures.PLAYER_SWINGUP);
                                swordPos.X = _position.X - 13;
                                swordPos.Y = _position.Y - 13;
                                break;

                            case DIRECTION.LEFT:
                                swapImage(Graphics.CTextures.PLAYER_SWINGLEFT);
                                swordPos.X = _position.X - 18;
                                swordPos.Y = _position.Y - 10;
                                break;

                            case DIRECTION.RIGHT:
                                swapImage(Graphics.CTextures.PLAYER_SWINGRIGHT);
                                swordPos.X = _position.X - 12;
                                swordPos.Y = _position.Y - 10;
                                break;

                            case DIRECTION.DOWN:
                                swapImage(Graphics.CTextures.PLAYER_SWINGDOWN);
                                swordPos.X = _position.X - 17;
                                swordPos.Y = _position.Y - 13;
                                break;
                        }

                        _triggerUserEvent(0, "sword", _direction, swordPos.X, swordPos.Y);
                    }

                    break;
                case ACTOR_STATES.IDLE:
                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            if (_carrying)
                                swapImage(Graphics.CTextures.PLAYER_LIFTIDLEDOWN);
                            else
                                swapImage(Graphics.CTextures.PLAYER_IDLEDOWN, false);
                            break;

                        case DIRECTION.UP:
                            if (_carrying)
                                swapImage(Graphics.CTextures.PLAYER_LIFTIDLEUP);
                            else
                                swapImage(Graphics.CTextures.PLAYER_IDLEUP, false);
                            break;

                        case DIRECTION.LEFT:
                            if (_carrying)
                                swapImage(Graphics.CTextures.PLAYER_LIFTIDLELEFT);
                            else
                                swapImage(Graphics.CTextures.PLAYER_IDLELEFT, false);
                            break;

                        case DIRECTION.RIGHT:
                            if (_carrying)
                                swapImage(Graphics.CTextures.PLAYER_LIFTIDLERIGHT);
                            else
                                swapImage(Graphics.CTextures.PLAYER_IDLERIGHT, false);
                            break;
                    }

                    break;

                case ACTOR_STATES.HOLD_CANNON:
                    if (_bombVelo < 120)
                        _bombVelo++;
                    break;
            }

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _angle = 270;
                    break;

                case DIRECTION.LEFT:
                    _angle = 180;
                    break;

                case DIRECTION.RIGHT:
                    _angle = 0;
                    break;

                case DIRECTION.UP:
                    _angle = 90;
                    break;
            }

            _readableCoords = _position;
            _readableAngle = _angle;
            _readableDirection = _direction;
        }

        public override void timer0(object sender)
        {
            if (INVINCIBLE_STATES.Contains(_state))
            {
                _state = ACTOR_STATES.IDLE;
                _acceptInput = true;
                _collisionDirectionX = 0;
                _collisionDirectionY = 0;
            }
        }

        public override void timer2(object sender)
        {
            if (_state == ACTOR_STATES.FROZEN)
            {
                dealDamange(2, this);
                startTimer2(80);
            }
        }

        public static float glblX
        {
            get
            {
                return _readableCoords.X;
            }
        }

        public static float glblY
        {
            get
            {
                return _readableCoords.Y;
            }
        }

        protected override void _addCollidables()
        {
            //_collidables.Add(typeof(Actors.NPC.Enemies.Keese.CKeese));

            _collidables.Add(typeof(NPC.Enemies.Keese.CKeeseFire));
            _collidables.Add(typeof(NPC.Enemies.Keese.CKeeseIce));
            _collidables.Add(typeof(NPC.Enemies.Keese.CKeese));
            _collidables.Add(typeof(NPC.Enemies.Keese.CKeeseShadow));
            _collidables.Add(typeof(NPC.Enemies.Keese.CKeeseThunder));
            _collidables.Add(typeof(NPC.Enemies.Zombie.CBaseZombie));
            _collidables.Add(typeof(Projectiles.CEnergyWave));

            //world things
            _collidables.Add(typeof(Actors.Collision.CSolidTile));
            _collidables.Add(typeof(Actors.Items.decoration.CPot));
            _collidables.Add(typeof(Actors.Items.decoration.CChest));

            //other NPCs
            _collidables.Add(typeof(Actors.NPC.Other.CTownsFolk));
        }

        public override void shock()
        {
            _state = ACTOR_STATES.SHOCKED;
            _acceptInput = false;
            startTimer0(80);
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Player:Electrocute"]);
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.PLAYER_SHOCKDOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.PLAYER_SHOCKUP);
                    break;

                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.PLAYER_SHOCKLEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.PLAYER_SHOCKRIGHT);
                    break;
            }
        }

        public override void stun(int time)
        {
            _state = ACTOR_STATES.STUNNED;
            _acceptInput = false;

            if (time > -1)
                startTimer3(time);
        }

        public override void timer3(object sender)
        {
            _state = ACTOR_STATES.IDLE;
            _acceptInput = true;
            
        }

        public override void freeze()
        {
            _state = ACTOR_STATES.FROZEN;
            _acceptInput = false;
            startTimer0(240);
            startTimer2(80);
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.PLAYER_FREEZEDOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.PLAYER_FREEZEUP);
                    break;

                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.PLAYER_FREEZELEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.PLAYER_FREEZERIGHT);
                    break;
            }
        }

        public override void dealDamange(int damage, CActor target)
        {
            base.dealDamange(damage, target);

            //update the HUD
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Player:Hurt1"]);
            CMasterControl.healthController.modifyHp(-damage);
        }

        private void _beginArrowCharge(Projectiles.ARROW_TYPES arrowType)
        {
            _state = ACTOR_STATES.CHARGING_ARROW;
            _arrowType = arrowType;

            switch (_direction)
            {
                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.PLAYER_CHARGE_ARROW_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.PLAYER_CHARGE_ARROW_RIGHT);
                    break;

                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.PLAYER_CHARGE_ARROW_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.PLAYER_CHARGE_ARROW_UP);
                    break;
            }
        }

        private void _queueUpArrow(ref Projectiles.ARROW_TYPES arrowType)
        {
            if (arrowType != Projectiles.ARROW_TYPES.STANDARD && !CMasterControl.magicMeter.checkMagicAmount(2))
                arrowType = Projectiles.ARROW_TYPES.STANDARD;

            Vector2 arrowVelocity = Vector2.Zero;
            Projectiles.CArrow arrow = new Actors.Projectiles.CArrow(_direction,arrowVelocity,_position, arrowType);
            Map.CMapManager.addActorToComponent(arrow, this.componentAddress);
            _lastArrowShotName = arrow.name;
        }

        private void _changeArrowType(Projectiles.ARROW_TYPES arrowType)
        {
            CMasterControl.commNet[this.componentAddress].Add(new CActorPacket(1, _lastArrowShotName, this, arrowType));
        }

        private void _holdArrow()
        {
            _queueUpArrow(ref _arrowType);
            state = ACTOR_STATES.HOLD_ARROW;

            switch (_direction)
            {
                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.PLAYER_HOLD_ARROW_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.PLAYER_HOLD_ARROW_RIGHT);
                    break;

                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.PLAYER_HOLD_ARROW_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.PLAYER_HOLD_ARROW_UP);
                    break;
            }
        }

        private void _shootArrow()
        {
            //if (_lastHudKeyPressed == Keys.Left)
                state = ACTOR_STATES.SHOOTING_ARROW;

            _triggerUserEvent(0, _lastArrowShotName);

            _lastArrowShotName = string.Empty;

            if (_arrowType != Projectiles.ARROW_TYPES.STANDARD)
                CMasterControl.magicMeter.subtractMagic(2);

            switch (_direction)
            {
                case DIRECTION.LEFT:
                    swapImage(Graphics.CTextures.PLAYER_SHOOT_ARROW_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(Graphics.CTextures.PLAYER_SHOOT_ARROW_RIGHT);
                    break;

                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.PLAYER_SHOOT_ARROW_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(Graphics.CTextures.PLAYER_SHOOT_ARROW_UP);
                    break;
            }

            _usingItem = false;
        }

        private void _throwBoomerang()
        {
            state = ACTOR_STATES.THROW_BOOMERANG;

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(_THROW_BOOMERANG_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(_THROW_BOOMERANG_UP);
                    break;

                case DIRECTION.LEFT:
                    swapImage(_THROW_BOOMERANG_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(_THROW_BOOMERANG_RIGHT);
                    break;
            }
        }

        private void _holdCannon()
        {
            state = ACTOR_STATES.HOLD_CANNON;


            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.PLAYER_HOLD_CANNON_DOWN);
                    break;
            }
        }

        private void _shootCannon()
        {
            //if (_lastHudKeyPressed == Keys.Left)
                state = ACTOR_STATES.SHOOTING_CANNON;

            Vector2 bombVelo = Vector2.Zero;
            Vector2 bombPos = _position;
            double veloScale = Math.Floor((double)(_bombVelo / 10.0));
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(Graphics.CTextures.PLAYER_SHOOT_CANNON_DOWN);
                    bombVelo.Y = (float)veloScale;
                    bombPos.Y += 30;
                    bombPos.X -= 3;
                    break;
            }

            Projectiles.CBomb bomb = new Projectiles.CBomb(direction, bombVelo, bombPos, 4);
            Map.CMapManager.addActorToComponent(bomb, this.componentAddress);
            _bombVelo = 0;
            _usingItem = false;
            
        }

        private void _swingBottle()
        {
            _state = ACTOR_STATES.SWING_BOTTLE;

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage("playerSwingDown");
                    break;

                case DIRECTION.LEFT:
                    swapImage("playerSwingLeft");
                    break;

                case DIRECTION.RIGHT:
                    swapImage("playerSwingRight");
                    break;

                case DIRECTION.UP:
                    swapImage("playerSwingUp");
                    break;
            }
        }

        //non negative == left
        //negative == right
        private void _useItem(sbyte leftOrRight)
        {
            _usingItem = true;
            HUD.buttons.HUDOPTIONS option = 0;
            if (leftOrRight >= 0)
            {
                option = CMasterControl.buttonController.buttonLeftItem;
                _lastHudKeyPressed = CMasterControl.glblInput.getKeyIfDown(Keys.Left);
            }
            else
            {
                option = CMasterControl.buttonController.buttonRightItem;
                _lastHudKeyPressed = CMasterControl.glblInput.getKeyIfDown(Keys.Right);
            }

            switch (option)
            {
                case HUD.buttons.HUDOPTIONS.ARROWS:
                    _beginArrowCharge(Projectiles.ARROW_TYPES.STANDARD);
                    break;

                case HUD.buttons.HUDOPTIONS.FIRE_ARROWS:
                    _beginArrowCharge(Projectiles.ARROW_TYPES.FIRE);
                    break;

                case HUD.buttons.HUDOPTIONS.EMPTY_BOTTLE:
                    _swingBottle();
                    break;

                case HUD.buttons.HUDOPTIONS.GREEN_POTION:
                    _useGreenPotion();
                    break;

                case HUD.buttons.HUDOPTIONS.ICE_ARROWS:
                    _beginArrowCharge(Projectiles.ARROW_TYPES.ICE);
                    break;

                case HUD.buttons.HUDOPTIONS.BOMB_CANNON:
                    _holdCannon();
                    break;

                case HUD.buttons.HUDOPTIONS.BOOMERANG:
                    _throwBoomerang();
                    break;

                case HUD.buttons.HUDOPTIONS.BLUE_POTION:
                    _useBluePotion();
                    break;

                case HUD.buttons.HUDOPTIONS.RED_POTION:
                    _useRedPotion();
                    break;

                case HUD.buttons.HUDOPTIONS.SHADOW_MEDALLION:
                    _useShadowCloak();
                    break;
            }
        }

        private void _useRedPotion()
        {
            CMasterControl.healthController.modifyHp(40);
            _usingItem = false;
        }

        private void _useGreenPotion()
        {
            CMasterControl.magicMeter.addMagic(50);
            _usingItem = false;
        }

        private void _useBluePotion()
        {
            _useRedPotion();
            _useGreenPotion();
        }

        private void _useShadowCloak()
        {
            _wearingShadowCloak = !_wearingShadowCloak;
            _usingItem = false;
        }

        private void _createShadowClone()
        {
            Vector2 pos = new Vector2(_position.X, _position.Y);
            if (cloneExists)
            {
                if (_canMoveClone)
                {
                    _canMoveClone = false;
                    component.actors[_name + "Clone"].jumpToPoint(pos.X,pos.Y);
                    ((CShadowClone)component.actors[_name + "Clone"]).changeDirection(_direction);
                    startTimer1(30);
                }
            }
            else
            {
                CActor clone = new CShadowClone();
                clone.init(_name + "Clone", pos, "", this.componentAddress, ((int)_direction).ToString());
                Map.CMapManager.addActorToComponent(clone, componentAddress);
                cloneExists = true;

                startTimer1(30);
            }
        }

        public override void timer1(object sender)
        {
            if (cloneExists)
                _canMoveClone = true;
        }

        public static double glblAngle
        {
            get
            {
                return _readableAngle;
            }
        }

        public static DIRECTION glblDirection
        {
            get
            {
                return _readableDirection;
            }
        }
    }
}
