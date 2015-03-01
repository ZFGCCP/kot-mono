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
        private bool _rollReleased = true;
        private static Vector2 _readableCoords = new Vector2();
        public static readonly Vector2 carrySpot = new Vector2(-6, -10); //will need to be played with
        private bool _carrying = false;
        private double _carryWeight = 0;
        private bool _acceptInput = true;
        private bool _usingItem = false;
        private int _collisionDirectionX = 0;
        private int _collisionDirectionY = 0;
        private Keys _lastHudKeyPressed = Keys.None;
        private string _lastArrowShotName = "";
        private string _lastBombShotName = "";
        private const int _MAX_BOMB_VELO = 5;
        private int _bombVelo = 0;

        private const string _THROW_BOOMERANG_DOWN = "PlayerThrowBoomerangDown";
        private const string _THROW_BOOMERANG_UP = "PlayerThrowBoomerangUp";
        private const string _THROW_BOOMERANG_LEFT = "PlayerThrowBoomerangLeft";
        private const string _THROW_BOOMERANG_RIGHT = "PlayerThrowBoomerangRight";
        private const string _GOT_ITEM = "PlayerGotItem";

        public CPlayer() :
            base()
        {

            _name = "Player";
            _position = Vector2.Zero;
            //resource init
            _hitBox = new Collision.CHitBox(this, 10, 18, 12, 15);

            
            image = _imageIndex["PlayerWalkDown"];
            _velocity = new Vector2(0, 0);
        }

        protected override void _initializeResources()
        {

            //We're creating A LOT of strings here -- This needs to be handled with a constants class or something
            base._initializeResources();
            _imageIndex.Add(_MAP_ICON, new Graphics.CSprite("Player:WalkDown"));

            _imageIndex.Add("PlayerWalkDown", new Graphics.CSprite("Player:WalkDown"));
            _imageIndex.Add("PlayerWalkLeft", new Graphics.CSprite("Player:WalkLeft"));
            _imageIndex.Add("PlayerWalkRight", new Graphics.CSprite("Player:WalkLeft", true));
            _imageIndex.Add("PlayerWalkUp", new Graphics.CSprite("Player:WalkUp"));

            _imageIndex.Add("PlayerIdleDown", new Graphics.CSprite("Player:IdleDown"));
            _imageIndex.Add("PlayerIdleUp", new Graphics.CSprite("Player:IdleUp"));
            _imageIndex.Add("PlayerIdleLeft", new Graphics.CSprite("Player:IdleLeft"));
            _imageIndex.Add("PlayerIdleRight", new Graphics.CSprite("Player:IdleLeft", true));

            _imageIndex.Add("PlayerSwingUp", new Graphics.CSprite("Player:SwingUp"));
            _imageIndex.Add("PlayerSwingDown", new Graphics.CSprite("Player:SwingDown"));
            _imageIndex.Add("PlayerSwingRight", new Graphics.CSprite("Player:SwingLeft", true));
            _imageIndex.Add("PlayerSwingLeft", new Graphics.CSprite("Player:SwingLeft"));

            _imageIndex.Add("PlayerRollDown", new Graphics.CSprite("Player:RollDown"));
            _imageIndex.Add("PlayerRollUp", new Graphics.CSprite("Player:RollUp"));
            _imageIndex.Add("PlayerRollLeft", new Graphics.CSprite("Player:RollLeft"));
            _imageIndex.Add("PlayerRollRight", new Graphics.CSprite("Player:RollLeft", true));

            _imageIndex.Add("PlayerLiftDown", new Graphics.CSprite("Player:LiftDown", Graphics.CTextures.textures["Player:LiftDown"]));
            _imageIndex.Add("PlayerLiftUp", new Graphics.CSprite("Player:LiftUp", Graphics.CTextures.textures["Player:LiftUp"]));
            _imageIndex.Add("PlayerLiftLeft", new Graphics.CSprite("Player:LiftLeft", Graphics.CTextures.textures["Player:LiftLeft"]));
            _imageIndex.Add("PlayerLiftRight", new Graphics.CSprite("Player:LiftLeft", Graphics.CTextures.textures["Player:LiftLeft"], null, true));

            _imageIndex.Add("PlayerCarryDown", new Graphics.CSprite("Player:CarryDown", Graphics.CTextures.textures["Player:CarryDown"]));
            _imageIndex.Add("PlayerCarryUp", new Graphics.CSprite("Player:CarryUp", Graphics.CTextures.textures["Player:CarryUp"]));
            _imageIndex.Add("PlayerCarryLeft", new Graphics.CSprite("Player:CarryLeft", Graphics.CTextures.textures["Player:CarryLeft"]));
            _imageIndex.Add("PlayerCarryRight", new Graphics.CSprite("Player:CarryLeft", Graphics.CTextures.textures["Player:CarryLeft"], null, true));

            _imageIndex.Add("PlayerLiftIdleDown", new Graphics.CSprite("Player:LiftIdleDown", Graphics.CTextures.textures["Player:LiftIdleDown"]));
            _imageIndex.Add("PlayerLiftIdleUp", new Graphics.CSprite("Player:LiftIdleUp", Graphics.CTextures.textures["Player:LiftIdleUp"]));
            _imageIndex.Add("PlayerLiftIdleLeft", new Graphics.CSprite("Player:LiftIdleLeft", Graphics.CTextures.textures["Player:LiftIdleLeft"]));
            _imageIndex.Add("PlayerLiftIdleRight", new Graphics.CSprite("Player:LiftIdleLeft", Graphics.CTextures.textures["Player:LiftIdleLeft"], null, true));

            _imageIndex.Add("PlayerThrowDown", new Graphics.CSprite("Player:ThrowDown", Graphics.CTextures.textures["Player:ThrowDown"]));
            _imageIndex.Add("PlayerThrowUp", new Graphics.CSprite("Player:ThrowUp", Graphics.CTextures.textures["Player:ThrowUp"]));
            _imageIndex.Add("PlayerThrowLeft", new Graphics.CSprite("Player:ThrowLeft", Graphics.CTextures.textures["Player:ThrowLeft"]));
            _imageIndex.Add("PlayerThrowRight", new Graphics.CSprite("Player:ThrowLeft", Graphics.CTextures.textures["Player:ThrowLeft"], null, true));

            _imageIndex.Add("PlayerShockDown", new Graphics.CSprite("Player:ShockDown", Graphics.CTextures.textures["Player:ShockDown"]));
            _imageIndex.Add("PlayerShockUp", new Graphics.CSprite("Player:ShockUp", Graphics.CTextures.textures["Player:ShockUp"]));
            _imageIndex.Add("PlayerShockLeft", new Graphics.CSprite("Player:ShockLeft", Graphics.CTextures.textures["Player:ShockLeft"]));
            _imageIndex.Add("PlayerShockRight", new Graphics.CSprite("Player:ShockLeft", Graphics.CTextures.textures["Player:ShockLeft"], null, true));

            _imageIndex.Add("PlayerFreezeDown", new Graphics.CSprite("Player:FreezeDown", Graphics.CTextures.textures["Player:FreezeDown"]));
            _imageIndex.Add("PlayerFreezeUp", new Graphics.CSprite("Player:FreezeUp", Graphics.CTextures.textures["Player:FreezeUp"]));
            _imageIndex.Add("PlayerFreezeLeft", new Graphics.CSprite("Player:FreezeLeft", Graphics.CTextures.textures["Player:FreezeLeft"]));
            _imageIndex.Add("PlayerFreezeRight", new Graphics.CSprite("Player:FreezeLeft", Graphics.CTextures.textures["Player:FreezeLeft"], null, true));

            _imageIndex.Add("PlayerChargeArrowDown", new Graphics.CSprite(Graphics.CTextures.PLAYER_CHARGE_ARROW_DOWN, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_CHARGE_ARROW_DOWN]));
            _imageIndex.Add("PlayerChargeArrowUp", new Graphics.CSprite(Graphics.CTextures.PLAYER_CHARGE_ARROW_UP, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_CHARGE_ARROW_UP]));
            _imageIndex.Add("PlayerChargeArrowLeft", new Graphics.CSprite(Graphics.CTextures.PLAYER_CHARGE_ARROW_LEFT, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_CHARGE_ARROW_LEFT]));
            _imageIndex.Add("PlayerChargeArrowRight", new Graphics.CSprite(Graphics.CTextures.PLAYER_CHARGE_ARROW_LEFT, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_CHARGE_ARROW_LEFT], null, true));

            _imageIndex.Add("PlayerHoldArrowDown", new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_ARROW_DOWN, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_HOLD_ARROW_DOWN]));
            _imageIndex.Add("PlayerHoldArrowUp", new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_ARROW_UP, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_HOLD_ARROW_UP]));
            _imageIndex.Add("PlayerHoldArrowLeft", new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_ARROW_LEFT, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_HOLD_ARROW_LEFT]));
            _imageIndex.Add("PlayerHoldArrowRight", new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_ARROW_LEFT, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_HOLD_ARROW_LEFT], null, true));

            _imageIndex.Add("PlayerShootArrowDown", new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_ARROW_DOWN, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_SHOOT_ARROW_DOWN]));
            _imageIndex.Add("PlayerShootArrowUp", new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_ARROW_UP, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_SHOOT_ARROW_UP]));
            _imageIndex.Add("PlayerShootArrowLeft", new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_ARROW_LEFT, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_SHOOT_ARROW_LEFT]));
            _imageIndex.Add("PlayerShootArrowRight", new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_ARROW_LEFT, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_SHOOT_ARROW_LEFT], null, true));

            _imageIndex.Add("PlayerHoldCannonDown", new Graphics.CSprite(Graphics.CTextures.PLAYER_HOLD_CANNON_DOWN, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_HOLD_CANNON_DOWN]));

            _imageIndex.Add("PlayerShootCannonDown", new Graphics.CSprite(Graphics.CTextures.PLAYER_SHOOT_CANNON_DOWN, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_SHOOT_CANNON_DOWN]));

            _imageIndex.Add(_THROW_BOOMERANG_DOWN, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROW_BOOMERANG_DOWN, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_THROW_BOOMERANG_DOWN]));
            _imageIndex.Add(_THROW_BOOMERANG_LEFT, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROW_BOOMERANG_LEFT, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_THROW_BOOMERANG_LEFT]));
            _imageIndex.Add(_THROW_BOOMERANG_RIGHT, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROW_BOOMERANG_LEFT, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_THROW_BOOMERANG_LEFT], null, true));
            _imageIndex.Add(_THROW_BOOMERANG_UP, new Graphics.CSprite(Graphics.CTextures.PLAYER_THROW_BOOMERANG_UP, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_THROW_BOOMERANG_UP]));

            _imageIndex.Add(_GOT_ITEM, new Graphics.CSprite(Graphics.CTextures.PLAYER_GOT_ITEM, Graphics.CTextures.textures[Graphics.CTextures.PLAYER_GOT_ITEM]));
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
                        _collideWithNpcResponse(collider);
                    }
                    else if (collider is Projectiles.CIceBall)
                        _collideWithNpcResponse(collider, false);
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

        private void solidCollide(CActor collider, bool knockBack = false)
        {
            //Calculate How much to move to get out of collision moving towards last collisionless point
			CHitBox otherbox = collider.hitBox;
			
			//Calculate how far in we went
			float distx = (collider.position.X + otherbox.center.X) - (position.X + hitBox.center.X);
			distx = (float)Math.Sqrt(distx * distx);
			float disty = (position.Y + hitBox.center.Y) - (collider.position.Y + otherbox.center.Y);
			disty = (float)Math.Sqrt(disty * disty);
			
			float lenx = hitBox.halfWidth + otherbox.halfWidth;
			float leny = hitBox.halfHeight + otherbox.halfHeight;
			
			int px = 1;
			int py = 1;
			
			if (collider.position.X+otherbox.center.X < position.X+hitBox.center.X)
				px = -1;
			if (collider.position.Y+otherbox.center.Y < position.Y+hitBox.center.Y)
				py = -1;
			
			float penx = px*(distx - lenx);
			float peny = py*(disty - leny);
			//Resolve closest to previous position
			float diffx = (position.X + penx) - _oldPosition.X;
			diffx *= diffx;
			float diffy = (position.Y + peny) - _oldPosition.Y;
			diffy *= diffy;

            if (!knockBack)
                _escapeCollide(diffx, diffy, penx, peny);
            else
                _knockBack(diffx, diffy, px, py);
        }

        private void _knockBack(float diffx, float diffy, float penx, float peny)
        {

            if (diffx < diffy)
                _collisionDirectionX = (int)-penx;
            else if (diffx > diffy)
                _collisionDirectionY = (int)-peny;
            else
            {
                _collisionDirectionX = (int)-penx;
                _collisionDirectionY = (int)-peny;
            }
        }

        private void _escapeCollide(float diffx, float diffy, float penx, float peny)
        {
            if (diffx < diffy)
                _position.X += penx; //TODO: dont make a new vector every time
            else if (diffx > diffy)
                _position.Y += peny; //Same here 
            else
            {
                _position.X += penx; 
                _position.Y += peny; //Corner cases 
            }
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
            }

            
        }

        public override void keyDown(object sender)
        {
            if (_acceptInput)
            {
                if (_state == ACTOR_STATES.IDLE || _state == ACTOR_STATES.MOVING)
                {
                    //Store this so we can type less
                    CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;

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
                        _state = ACTOR_STATES.SHIELDING;

                    if (!_usingItem)
                    {
                        if (input.keysPressed.Contains(Keys.A))
                        {
                            if (_carrying)
                            {
                                swapImage("PlayerCarryLeft");
                                _velocity.X = -1;
                            }
                            else if (_state == ACTOR_STATES.SHIELDING)
                            {
                                _velocity.X = -2.5f;
                            }
                            else
                            {
                                image = _imageIndex["PlayerWalkLeft"];
                                _velocity.X = -1;
                            }

                            _position.X += _velocity.X;
                            _direction = DIRECTION.LEFT;
                            _state = ACTOR_STATES.MOVING;
                        }

                        if (input.keysPressed.Contains(Keys.D))
                        {
                            if (_carrying)
                            {
                                swapImage("PlayerCarryRight");
                                _velocity.X = 1;
                            }
                            else if (_state == ACTOR_STATES.SHIELDING)
                            {
                                _velocity.X = 2.5f;
                            }
                            else
                            {
                                image = _imageIndex["PlayerWalkRight"];
                                _velocity.X = 1;
                            }

                            _position.X += _velocity.X;
                            _direction = DIRECTION.RIGHT;
                            _state = ACTOR_STATES.MOVING;
                        }

                        if (input.keysPressed.Contains(Keys.W))
                        {
                            if (_carrying)
                            {
                                _velocity.Y = -1;
                                swapImage("PlayerCarryUp");
                            }
                            else if (_state == ACTOR_STATES.SHIELDING)
                            {
                                _velocity.Y = -2.5f;
                            }
                            else
                            {
                                _velocity.Y = -1;
                                image = _imageIndex["PlayerWalkUp"];
                            }

                            _position.Y += _velocity.Y;
                            _direction = DIRECTION.UP;
                            _state = ACTOR_STATES.MOVING;
                        }

                        if (input.keysPressed.Contains(Keys.S))
                        {
                            
                            if (_carrying)
                            {
                                swapImage("PlayerCarryDown");
                                _velocity.Y = 1;
                            }
                            else if (_state == ACTOR_STATES.SHIELDING)
                            {
                                _velocity.Y = 2.5f;
                            }
                            else
                            {
                                image = _imageIndex["PlayerWalkDown"];
                                _velocity.Y = 1;
                            }

                            _position.Y += _velocity.Y;
                            _direction = DIRECTION.DOWN;
                            _state = ACTOR_STATES.MOVING;
                        }

                        _oldVelocity = _velocity;
                    }

                    if (input.keysPressed.Contains(Keys.Space))
                    {
                        _state = ACTOR_STATES.SWINGING;
                        _swordReleased = false;
                    }
                }
            }
            _velocity.X = 0;
            _velocity.Y = 0;
        }

        public override void keyRelease(object sender)
        {
            if (_acceptInput)
            {
                CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
                if (input.keysReleased.Contains(Keys.T))
                {
                    Items.Drops.CRupeeDrop rupee = new Items.Drops.CRupeeDrop();
                    rupee.init("rupeeTest", _position - new Vector2(20, 20), "", this.componentAddress, "P");
                    Map.CMapManager.addActorToComponent(rupee, this.componentAddress);
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
                            _queueUpArrow();
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
                    _state = ACTOR_STATES.IDLE;

                if (input.keysReleased.Contains(Keys.C))
                {
                    //check HUD state
                    if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.OPEN)
                    {
                        _state = ACTOR_STATES.GOT_ITEM;
                        swapImage(_GOT_ITEM);
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
                                    swapImage("PlayerThrowDown");
                                    break;

                                case DIRECTION.UP:
                                    swapImage("PlayerThrowUp");
                                    break;

                                case DIRECTION.LEFT:
                                    swapImage("PlayerThrowLeft");
                                    break;

                                case DIRECTION.RIGHT:
                                    swapImage("PlayerThrowRight");
                                    break;
                            }

                            _carrying = false;
                            return;
                        }
                        _state = ACTOR_STATES.ROLLING;
                        _rollReleased = false;
                        //get the FUCK out of this
                        return;
                    }
                }
            }


        }



        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
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
                        swapImage("PlayerIdleDown");
                    }
                    break;

                case ACTOR_STATES.LIFT:
                    switch (_direction)
                    {
                        case DIRECTION.DOWN:
                            swapImage("PlayerLiftDown");
                            break;

                        case DIRECTION.UP:
                            swapImage("PlayerLiftUp");
                            break;

                        case DIRECTION.LEFT:
                            swapImage("PlayerLiftLeft");
                            break;

                        case DIRECTION.RIGHT:
                            swapImage("PlayerLiftRight");
                            break;

                    }
                    
                    break;

                case ACTOR_STATES.ROLLING:
                    if (!_rollReleased)
                    {
                        _rollReleased = true;
                        CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Player:Attack3"]);
                    }
                        switch (_direction)
                        {

                            case DIRECTION.DOWN:
                                swapImage("PlayerRollDown");
                                _position.Y += 2;
                                break;

                            case DIRECTION.UP:
                                swapImage("PlayerRollUp");
                                _position.Y -= 2;
                                break;

                            case DIRECTION.LEFT:
                                swapImage("PlayerRollLeft");
                                _position.X -= 2;
                                break;

                            case DIRECTION.RIGHT:
                                swapImage("PlayerRollRight");
                                _position.X += 2;
                                break;
                        }
                    
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
                                swapImage("PlayerSwingUp");
                                swordPos.X = _position.X - 13;
                                swordPos.Y = _position.Y - 13;
                                break;

                            case DIRECTION.LEFT:
                                swapImage("PlayerSwingLeft");
                                swordPos.X = _position.X - 18;
                                swordPos.Y = _position.Y - 10;
                                break;

                            case DIRECTION.RIGHT:
                                swapImage("PlayerSwingRight");
                                swordPos.X = _position.X - 12;
                                swordPos.Y = _position.Y - 10;
                                break;

                            case DIRECTION.DOWN:
                                swapImage("PlayerSwingDown");
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
                                swapImage("PlayerLiftIdleDown");
                            else
                                swapImage("PlayerIdleDown", false);
                            break;

                        case DIRECTION.UP:
                            if (_carrying)
                                swapImage("PlayerLiftIdleUp");
                            else
                                swapImage("PlayerIdleUp", false);
                            break;

                        case DIRECTION.LEFT:
                            if (_carrying)
                                swapImage("PlayerLiftIdleLeft");
                            else
                                swapImage("PlayerIdleLeft", false);
                            break;

                        case DIRECTION.RIGHT:
                            if (_carrying)
                                swapImage("PlayerLiftIdleRight");
                            else
                                swapImage("PlayerIdleRight", false);
                            break;
                    }

                    break;

                case ACTOR_STATES.HOLD_CANNON:
                    if (_bombVelo < 120)
                        _bombVelo++;
                    break;
            }
            _readableCoords = _position;
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
            _collidables.Add(typeof(Projectiles.CEnergyWave));

            //world things
            _collidables.Add(typeof(Actors.Collision.CSolidTile));
            _collidables.Add(typeof(Actors.Items.decoration.CPot));
            _collidables.Add(typeof(Actors.Items.decoration.CChest));
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
                    swapImage("PlayerShockDown");
                    break;

                case DIRECTION.UP:
                    swapImage("PlayerShockUp");
                    break;

                case DIRECTION.LEFT:
                    swapImage("PlayerShockLeft");
                    break;

                case DIRECTION.RIGHT:
                    swapImage("PlayerShockRight");
                    break;
            }
        }

        public override void stun(int time)
        {
            _state = ACTOR_STATES.STUNNED;
            _acceptInput = false;
            startTimer3(time);
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
                    swapImage("PlayerFreezeDown");
                    break;

                case DIRECTION.UP:
                    swapImage("PlayerFreezeUp");
                    break;

                case DIRECTION.LEFT:
                    swapImage("PlayerFreezeLeft");
                    break;

                case DIRECTION.RIGHT:
                    swapImage("PlayerFreezeRight");
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

        private void _beginArrowCharge()
        {
            _state = ACTOR_STATES.CHARGING_ARROW;

            switch (_direction)
            {
                case DIRECTION.LEFT:
                    swapImage("PlayerChargeArrowLeft");
                    break;

                case DIRECTION.RIGHT:
                    swapImage("PlayerChargeArrowRight");
                    break;

                case DIRECTION.DOWN:
                    swapImage("PlayerChargeArrowDown");
                    break;

                case DIRECTION.UP:
                    swapImage("PlayerChargeArrowUp");
                    break;
            }
        }

        private void _queueUpArrow()
        {
            Vector2 arrowVelocity = Vector2.Zero;
            Projectiles.CArrow arrow = new Actors.Projectiles.CArrow(_direction,arrowVelocity,_position);
            Map.CMapManager.addActorToComponent(arrow, this.componentAddress);
            _lastArrowShotName = arrow.name;
        }

        private void _holdArrow()
        {
            _queueUpArrow();
            state = ACTOR_STATES.HOLD_ARROW;

            switch (_direction)
            {
                case DIRECTION.LEFT:
                    swapImage("PlayerHoldArrowLeft");
                    break;

                case DIRECTION.RIGHT:
                    swapImage("PlayerHoldArrowRight");
                    break;

                case DIRECTION.DOWN:
                    swapImage("PlayerHoldArrowDown");
                    break;

                case DIRECTION.UP:
                    swapImage("PlayerHoldArrowUp");
                    break;
            }
        }

        private void _shootArrow()
        {
            if (_lastHudKeyPressed == Keys.Left)
                state = ACTOR_STATES.SHOOTING_ARROW;

            _triggerUserEvent(0, _lastArrowShotName);

            _lastArrowShotName = string.Empty;

            switch (_direction)
            {
                case DIRECTION.LEFT:
                    swapImage("PlayerShootArrowLeft");
                    break;

                case DIRECTION.RIGHT:
                    swapImage("PlayerShootArrowRight");
                    break;

                case DIRECTION.DOWN:
                    swapImage("PlayerShootArrowDown");
                    break;

                case DIRECTION.UP:
                    swapImage("PlayerShootArrowUp");
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
                    swapImage("PlayerHoldCannonDown");
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
                    swapImage("PlayerShootCannonDown");
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
                    _beginArrowCharge();
                    break;

                case HUD.buttons.HUDOPTIONS.BOMB_CANNON:
                    _holdCannon();
                    break;

                case HUD.buttons.HUDOPTIONS.BOOMERANG:
                    _throwBoomerang();
                    break;
            }

            
        }
    }
}
