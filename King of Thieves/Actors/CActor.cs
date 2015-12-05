using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using Gears.Cloud;
using King_of_Thieves.Input;
using System.Timers;
using Gears.Cloud.Utility;

namespace King_of_Thieves.Actors
{
    //Actor states
    //idle: Not doing anything
    public enum ACTORTYPES
    {
        MANAGER = 0,
        INTERACTABLE
    }

    public enum DIRECTION
    {
        UP = 0,
        DOWN,
        LEFT,
        RIGHT,
        DLEFT,
        DRIGHT,
        ULEFT,
        URIGHT
    }

    public enum ACTOR_STATES
    {
        ALERT = 0,
        ATTACK,
        BEING_PICKED,
        CARRY,
        CHARGING_ARROW,
        CHARGING_SWORD,
        CHASE,
        DAWN,
        DAY,
        DECREMENT,
        DUSK,
        EXPLODE,
        FLYING,
        FOLLOW_PLAYER,
        FROZEN,
        GO_HOME,
        GOT_ITEM,
        HIDDEN,
        HOLD,
        HOLD_ARROW,
        HOLD_CANNON,
        IDLE,
        IDLE_STARE,
        INCREMENT,
        INVISIBLE,
        KNOCKBACK,
        LIFT,
        LOCKED,
        MIDNIGHT,
        MORNING,
        MOVING,
        NIGHT,
        PICKING,
        PICK_READY,
        POPDOWN,
        POPUP,
        PULLSWORD,
        ROLLING,
        SHIELD_ENGAGE,
        SHIELD_DISENGAGE,
        SHIELDING,
        SHIFT_LEFT,
        SHIFT_RIGHT,
        SHOOK_OFF,
        SHOOTING_ARROW,
        SHOOTING_CANNON,
        SMASH,
        SPIN_ATTACK,
        SWINGING,
        SWING_BOTTLE,
        SHOCKED,
        STUNNED,
        TALK_READY,
        THROW_BOOMERANG,
        THROWING,
        TOSSING,
        UNLOCKED,
        VAULT,
        VAULT_IDLE,
        WOBBLE,
        YIELD
    }

    public abstract class CActor
    {
        public IList<ACTOR_STATES> INVINCIBLE_STATES = new List<ACTOR_STATES>{ACTOR_STATES.KNOCKBACK, ACTOR_STATES.SHOCKED, ACTOR_STATES.FROZEN}.AsReadOnly();
        protected Vector2 _position = Vector2.Zero;
        protected Vector2 _oldPosition = Vector2.Zero;
        public readonly ACTORTYPES ACTORTYPE;
        protected string _name;
        protected CAnimation _sprite;
        protected DIRECTION _direction = DIRECTION.UP;
        protected double _angle;
        protected Boolean _moving = false; //used for prioritized movement
        private int _componentAddress = 0;
        protected Dictionary<uint, actorEventHandler> _userEvents;
        protected Dictionary<uint, CActor> _userEventsToFire;
        protected ACTOR_STATES _state = ACTOR_STATES.IDLE;
        public Graphics.CSprite image;
        protected Dictionary<string, Graphics.CSprite> _imageIndex;
        protected Dictionary<string, Sound.CSound> _soundIndex;
        private bool _animationHasEnded = false;
        public List<object> userParams = new List<object>();
        public List<string> mapParams = new List<string>();
        public bool _followRoot = true;
        public int layer;
        public CComponent component;
        protected Vector2 _velocity;
        protected Vector2 _oldVelocity;
        public bool noCollide = false;
        protected string _oldName = ""; //for when moving to different components
        protected CComponent _oldComponent = null;
        protected bool _killMe = false;
        protected int _hp;
        protected bool _enabled = true;
        private string _dataType;
        public static string _MAP_ICON = "MAP_ICON";
        protected bool _invulernable = false;
        protected int _collisionDirectionX = 0;
        protected int _collisionDirectionY = 0;
        private bool _flagForResourceCleanup = false;
        public bool hidden = false;

        protected int _lineOfSight;
        protected int _fovMagnitude;
        protected float _visionRange; //this is an angle
        protected float _visionSlope;
        protected int _hearingRadius; //how far away they can hear you from

        protected Collision.CHitBox _hitBox;
        protected List<Type> _collidables;
        public static bool showHitBox = false; //Draw hitboxes over actor if this is true
        protected Vector2 _motionCounter = Vector2.Zero;
        protected int _drawDepth = 8;
        protected bool _firstTick = true;
        private string _currentImageIndex = "";

        //event handlers will be added here
        public event actorEventHandler onCreate;
        public event actorEventHandler onDestroy;
        public event actorEventHandler onKeyDown;
        public event actorEventHandler onFrame;
        public event actorEventHandler onDraw;
        public event actorEventHandler onKeyRelease;
        public event collideHandler onCollide;
        public event actorEventHandler onAnimationEnd;
        public event actorEventHandler onTimer0;
        public event actorEventHandler onTimer1;
        public event actorEventHandler onTimer2;
        public event actorEventHandler onTimer3;
        public event actorEventHandler onTimer4;
        public event actorEventHandler onTimer5;
        public event actorEventHandler onTimer6;
        public event actorEventHandler onMouseClick;
        public event actorEventHandler onClick;
        public event actorEventHandler onTap;

        public virtual void create(object sender) { }
        public virtual void keyDown(object sender) { }
        public virtual void keyRelease(object sender) { }
        public virtual void frame(object sender) { }
        public virtual void draw(object sender) { }
        public virtual void collide(object sender, CActor collider) { }
        public virtual void animationEnd(object sender) { }
        public virtual void timer0(object sender)  { }
        public virtual void timer1(object sender) { }
        public virtual void timer2(object sender) { }
        public virtual void timer3(object sender) { }
        public virtual void timer4(object sender) { }
        public virtual void timer5(object sender) { }
        public virtual void timer6(object sender) { }
        public virtual void mouseClick(object sender) { }
        public virtual void click(object sender) { }
        public virtual void tap(object sender) { }

        protected virtual void cleanUp() 
        {
            if (_flagForResourceCleanup)
            {
                foreach (KeyValuePair<string, CSprite> kvp in _imageIndex)
                {
                    if (Graphics.CTextures.textures.ContainsKey(kvp.Value.atlasName))
                    {
                        Graphics.CTextures.textures[kvp.Value.atlasName].Dispose();
                        Graphics.CTextures.textures.Remove(kvp.Value.atlasName);
                    }
                    kvp.Value.clean();
                }
            }

            _imageIndex.Clear();
        }
        public virtual void destroy(object sender)
        {
            if (_hitBox != null)
            {
                _hitBox.destroy();
                _hitBox = null;
            }
        }

        protected virtual void applyEffects(){}

        protected virtual void _addCollidables() { } //Use this guy to tell the Actor what kind of actors it can collide with
        protected Random _randNum = new Random();

        private int _timer0 = -1;
        private int _timer1 = -1;
        private int _timer2 = -1;
        private int _timer3 = -1;
        private int _timer4 = -1;
        private int _timer5 = -1;
        private int _timer6 = -1;

        public CActor()
            
        {
            onCreate += new actorEventHandler(create);
            onDestroy += new actorEventHandler(destroy);
            onKeyDown += new actorEventHandler(keyDown);
            onKeyRelease += new actorEventHandler(keyRelease);
            onFrame += new actorEventHandler(frame);
            onDraw += new actorEventHandler(draw);
            onAnimationEnd += new actorEventHandler(animationEnd);
            onCollide += new collideHandler(collide);
            onMouseClick += new actorEventHandler(mouseClick);
            onTap += new actorEventHandler(tap);
            onTimer0 += new actorEventHandler(timer0);
            onTimer1 += new actorEventHandler(timer1);
            onTimer2 += new actorEventHandler(timer2);
            onTimer3 += new actorEventHandler(timer3);
            onTimer4 += new actorEventHandler(timer4);
            onTimer5 += new actorEventHandler(timer5);
            onTimer6 += new actorEventHandler(timer6);

            _name = name;
            _collidables = new List<Type>();

            try
            {
                _addCollidables();
            }
            catch (NotImplementedException)
            { ;}

            _position = position;

            try
            {
                onCreate(this);
            }
            catch (NotImplementedException)
            { }

            _registerUserEvents();
            _registerSystemEvents();
            _initializeResources();
        }

        ~CActor()
        {
            onCreate -= new actorEventHandler(create);
            onDestroy -= new actorEventHandler(destroy);
            onKeyDown -= new actorEventHandler(keyDown);
            onFrame -= new actorEventHandler(frame);
            onKeyRelease -= new actorEventHandler(keyRelease);
            onDraw -= new actorEventHandler(draw);
        }

        public string dataType
        {
            get
            {
                return _dataType;
            }
        }

        protected double _calculateAngle(Vector2 toPoint)
        {
            double angle = MathExt.MathExt.angle(_position, toPoint);
            

            return angle;
        }

        private void _registerSystemEvents()
        {
            _userEvents.Add(1000, (object sender) => _killMe = true);
        }

        public void lookAt(Vector2 position)
        {
            double angle = _calculateAngle(position);

            _directionChange(angle);
        }

        protected void _flagResourceCleanup()
        {
            _flagForResourceCleanup = true;
        }

        protected void _directionChange(double angle)
        {
            if (angle >= 225 && angle <= 315)
            {
                _direction = DIRECTION.DOWN;
            }
            else if (angle >= 135 && angle < 225)
            {
                _direction = DIRECTION.LEFT;
            }
            else if (angle >= 45 && angle < 135)
            {
                _direction = DIRECTION.UP;
            }
            else if (angle >= 0 || angle >= 315)
            {
                _direction = DIRECTION.RIGHT;
            }
        }

        public void lookAtExt(Vector2 position)
        {
            double angle = _calculateAngle(position);

            _directionChangeExt(angle);

        }

        protected void _directionChangeExt(double angle)
        {
            if (angle >= 247.5 && angle <= 292.5)
            {
                _direction = DIRECTION.DOWN;
            }
            else if (angle <= 247.5 && angle >= 202.5)
            {
                _direction = DIRECTION.DLEFT;
            }
            else if (angle >= 292.5 && angle <= 337.5)
            {
                _direction = DIRECTION.DRIGHT;
            }
            else if (angle >= 157.5 && angle < 202.5)
            {
                _direction = DIRECTION.LEFT;
            }
            else if (angle <= 157.5 && angle >= 112.5)
            {
                _direction = DIRECTION.ULEFT;
            }
            else if (angle >= 67.5 && angle < 112.5)
            {
                _direction = DIRECTION.UP;
            }
            else if (angle <= 67.5 && angle >= 22.5)
            {
                _direction = DIRECTION.URIGHT;
            }
            else if (angle <= 22.5 || angle >= 337.5)
            {
                _direction = DIRECTION.RIGHT;
            }
        }

        public string getMapHeaderInfo()
        {
            return _componentAddress + ";" + _name + ";" + dataType + ";" + position.X + ":" + position.Y;
        }

        public void startTimer0(int ticks)
        {
            _timer0 = ticks;
        }

        public void startTimer1(int ticks)
        {
            _timer1 = ticks;
        }

        public void startTimer2(int ticks)
        {
            _timer2 = ticks;
        }

        public void startTimer3(int ticks)
        {
            _timer3 = ticks;
        }

        public void startTimer4(int ticks)
        {
            _timer4 = ticks;
        }

        public void startTimer5(int ticks)
        {
            _timer5 = ticks;
        }

        public void startTimer6(int ticks)
        {
            _timer6 = ticks;
        }

        public void stopTimer0()
        {
            _timer0 = 0;
        }

        public void stopTimer1()
        {
            _timer1 = 0;
        }

        public void stopTimer2()
        {
            _timer2 = 0;
        }

        public void stopTimer3()
        {
            _timer3 = 0;
        }

        public void stopTimer4()
        {
            _timer4 = 0;
        }

        public void stopTimer5()
        {
            _timer5 = 0;
        }

        public void stopTimer6()
        {
            _timer6 = 0;
        }

        //overload this and call the base to process your own parameters
        public virtual void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            _name = name;
            _position = position;
            _componentAddress = (int)compAddress;
            _dataType = dataType;

            if (additional != null)
                mapParams = additional.ToList();
        }

        public Vector2 velocity
        {
            get
            {
                return _velocity;
            }
        }

        public int componentAddress
        {
            get
            {
                return component.address;
            }
        }

        public void setVelocity(float x, float y)
        {
            _velocity.X = x;
            _velocity.Y = y;
        }

        public ACTOR_STATES state
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public void moveInDirection(Vector2 velocity)
        {
            double ppfX = 0;
            double ppfY = 0;

            if (Math.Abs(velocity.X) > 1.0f)
            {
                ppfX = Math.Round(velocity.X);

                _position.X += (float)ppfX;
            }
            else
            {
                ppfX = Math.Pow((double)velocity.X, -1);

                if (_motionCounter.X >= Math.Abs(ppfX))
                {
                    _position.X += (1.0f * Math.Sign(velocity.X));
                    _motionCounter.X = 0;
                }
            }

            if (Math.Abs(velocity.Y) > 1.0f)
            {
                ppfY = Math.Round(velocity.Y);

                _position.Y += (float)ppfY;
            }
            else
            {
                ppfY = Math.Pow((double)velocity.Y, -1);

                if (_motionCounter.Y >= Math.Abs(ppfY))
                {
                    _position.Y += (1.0f * Math.Sign(velocity.Y));
                    _motionCounter.Y = 0;
                }
            }

            _motionCounter.X += 1;
            _motionCounter.Y += 1;
        }

        public DIRECTION moveToPoint2(float x, float y, float speed, bool calcAngle = true)
        {
            float distX = 0, distY = 0;

            distX = (x - _position.X);
            distY = (y - _position.Y);

            distX = Math.Sign(distX);
            distY = Math.Sign(distY);

            double ppf = 0;
            if (speed > 1.0f)
                ppf = Math.Round(speed);
            else
                ppf = Math.Pow((double)speed, -1);

            _motionCounter.X += 1;
            _motionCounter.Y += 1;

            if (speed > 1.0f)
            {
                _position.X += ((float)ppf * distX);
                _position.Y += ((float)ppf * distY);
            }
            else
            {
                if (_motionCounter.X >= ppf)
                {
                    _position.X += (1.0f * distX);
                    _motionCounter.X = 0;
                }
                if (_motionCounter.Y >= ppf)
                {
                    _position.Y += (1.0f * distY);
                    _motionCounter.Y = 0;
                }
            }

            Vector2 newPosition = new Vector2(x, y);

            if (calcAngle)
                _angle = _calculateAngle(newPosition);

            if (distY < 0)
                return DIRECTION.UP;
            else if (distY > 0)
                return DIRECTION.DOWN;

            if (distX < 0)
                return DIRECTION.LEFT;
            else if (distX > 0)
                return DIRECTION.RIGHT;

            return DIRECTION.DOWN;


        }

        public DIRECTION moveToPoint(float x, float y, float speed, bool calcAngle = true)
        {
            float distX = 0, distY = 0;

            distX = (x - _position.X);
            distY = (y - _position.Y);

            distX = Math.Sign(distX);
            distY = Math.Sign(distY);

            _position.X += (speed * distX);
             _position.Y += (speed* distY);
 


            Vector2 newPosition = new Vector2(x, y);

            if (calcAngle)
                _angle = _calculateAngle(newPosition);

            if (distY < 0)
                return DIRECTION.UP;
            else if (distY > 0)
                return DIRECTION.DOWN;

            if (distX < 0)
                return DIRECTION.LEFT;
            else if (distX > 0)
                return DIRECTION.RIGHT;

            return DIRECTION.DOWN;


        }

        public void jumpToPoint(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }

        public void swapImage(string imageIndex, bool triggerAnimEnd = true)
        {
            try
            {
                image = _imageIndex[imageIndex];
                _currentImageIndex = imageIndex;

                if (triggerAnimEnd)
                {
                    _animationHasEnded = true;
                }
            }
            catch (KeyNotFoundException ex)
            {

            }
        }

        public void addFireTrigger(uint userEvent, CActor sender)
        {
            _userEventsToFire.Add(userEvent, sender);
        }

        protected virtual void _registerUserEvents()
        {
            _userEvents = new Dictionary<uint, actorEventHandler>();
            _userEventsToFire = new Dictionary<uint, CActor>();
        }

        public DIRECTION direction
        {
            get
            {
                return _direction;
            }
        }

        public CAnimation spriteIndex
        {
            get
            {
                return _sprite;
            }
            set
            {
                _sprite = value;
            }
        }

        public void doCollision()
        {
            if (!_noCollide)
            {
                foreach (Type actor in _collidables)
                {

                    //fetch all actors of this type and check them for collisions
                    CActor[] collideCheck = Map.CMapManager.queryActorRegistry(actor, layer);
                    if (collideCheck == null)
                        continue;

                    foreach (CActor x in collideCheck)
                    {
                        if (_hitBox != null && x._hitBox != null && x != this && !x._noCollide && _hitBox.checkCollision(x))
                        {
                            //trigger collision event
                            onCollide(this, x);
                        }
                    }
                }
            }
        }

        public virtual void update(GameTime gameTime)
        {
            if (_killMe)
            {
                Map.CMapManager.removeFromActorRegistry(this);
                onDestroy(this);
            }
            else
            {
                if (_animationHasEnded)
                    try
                    {
                        onAnimationEnd(this);
                    }
                    catch (NotImplementedException) { ;}

                _oldPosition = _position;

                if (image != null)
                {
                    image.X = (int)_position.X;
                    image.Y = (int)_position.Y;
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysPressed)
                    onKeyDown(this);

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysReleased)
                    onKeyRelease(this);

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).mouseLeftClick)
                {
                    onMouseClick(this);


                    if (_hitBox != null && _hitBox.checkCollision(new Vector2((Master.GetInputManager().GetCurrentInputHandler() as CInput).mouseX,
                                                            (Master.GetInputManager().GetCurrentInputHandler() as CInput).mouseY)))
                    {
                        click(this);
                    }
                }

                if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).mouseLeftRelease)
                {
                    onTap(this);
                }

                //new timer stuff THANKS ASH
                if (_timer0 >= 0)
                {
                    _timer0--;

                    if (_timer0 <= 0)
                    {
                        _timer0 = -1;
                        onTimer0(this);
                    }
                }

                if (_timer1 >= 0)
                {
                    _timer1--;

                    if (_timer1 <= 0)
                    {
                        _timer1 = -1;
                        onTimer1(this);
                    }
                }

                if (_timer2 >= 0)
                {
                    _timer2--;

                    if (_timer2 <= 0)
                    {
                        _timer2 = -1;
                        onTimer2(this);
                    }
                }

                if (_timer3 >= 0)
                {
                    _timer3--;

                    if (_timer3 <= 0)
                    {
                        _timer3 = -1;
                        onTimer3(this);
                    }
                }

                if (_timer4 >= 0)
                {
                    _timer4--;

                    if (_timer4 <= 0)
                    {
                        _timer4 = -1;
                        onTimer4(this);
                    }
                }

                if (_timer5 >= 0)
                {
                    _timer5--;

                    if (_timer5 <= 0)
                    {
                        _timer5 = -1;
                        onTimer5(this);
                    }
                }

                if (_timer6 >= 0)
                {
                    _timer6--;

                    if (_timer6 <= 0)
                    {
                        _timer6 = -1;
                        onTimer6(this);
                    }
                }


                foreach (KeyValuePair<uint, CActor> ID in _userEventsToFire)
                {
                    _userEvents[ID.Key](ID.Value);
                }
            }

            _userEventsToFire.Clear();

            _animationHasEnded = false;
            userParams.Clear();

        }

        public virtual void drawMe(bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            onDraw(this);

            //Color overlay = useOverlay ? Controllers.GameControllers.CDayClock.overlay : Color.White;

            if (image != null && _state != ACTOR_STATES.INVISIBLE && !hidden)
                _animationHasEnded = image.draw((int)_position.X, (int)_position.Y, useOverlay, spriteBatch);

            if (showHitBox && _hitBox != null)
                _hitBox.draw();

        }

        public virtual void dealDamange(int damage, CActor target)
        {
            target._hp -= damage;
        }

        private void _dealForce(int force, CActor target)
        {

        }

        public bool isCollidable
        {
            get
            {
                return _hitBox == null;
            }
        }

        protected virtual void _initializeResources()
        {
            //add sprites to image index by overloading this function.
            //also add resources to the texture cache here.
            _imageIndex = new Dictionary<string, CSprite>();
            _soundIndex = new Dictionary<string, Sound.CSound>();
        }

        public Vector2 position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Vector2 oldPosition
        {
            get
            {
                return _oldPosition;
            }
            set
            {
                _oldPosition = value;
            }
            
        }

        public bool killMe
        {
            get
            {
                return _killMe;
            }
            set
            {
                _killMe = value;
            }
        }

        private bool _noCollide
        {
            get
            {
                return _hitBox == null;
            }
        }

        public King_of_Thieves.Actors.Collision.CHitBox hitBox
        {
            get
            {
                return _hitBox;
            }
        }

        public Vector2 distanceFromLastFrame
        {
            get
            {
                return (position - oldPosition);
            }
        }

        public virtual void remove()
        {
            onDestroy(this);
        }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public virtual void shock()
        {
            throw new NotImplementedException("You may not call this method from the CActor class. Method: shock()");
        }

        public virtual void freeze()
        {
            throw new NotImplementedException("You may not call this method from the CActor class. Method: freeze()");
        }

        public virtual void stun(int time)
        {
            throw new NotImplementedException("You may not call this method from the CACtor class. Method: stun()");
        }

        //this will go up to the component and trigger the specified user event in the specified actor
        //what this does is create a "packet" that will float around in some higher level scope for the component to pick up
        protected void _triggerUserEvent(int eventNum, string actorName, params object[] param)
        {
            CMasterControl.commNet[_componentAddress].Add(new CActorPacket(eventNum, actorName, this, param));
        }

        protected bool _checkIfPointInView(Vector2 point)
        {
            //build triangle points first
            Vector2 A = _position;
            Vector2 B = Vector2.Zero;
            Vector2 C = Vector2.Zero;

            B.X = (float)(Math.Cos((_angle - _visionRange / 2.0f) * (Math.PI / 180)) * _lineOfSight) + _position.X;
            B.Y = (float)((Math.Sin((_angle - _visionRange / 2.0f) * (Math.PI / 180)) * _lineOfSight) * -1.0) + _position.Y;

            C.X = (float)(Math.Cos((_angle + _visionRange / 2.0f) * (Math.PI / 180)) * _lineOfSight) + _position.X;
            C.Y = (float)((Math.Sin((_angle + _visionRange / 2.0f) * (Math.PI / 180)) * _lineOfSight) * -1.0) + _position.Y;

            return MathExt.MathExt.checkPointInTriangle(point, A, B, C);
        }

        protected bool _checkIfPointInView(Vector2 point, Vector2 origin)
        {
            //build triangle points first
            Vector2 A = origin;
            Vector2 B = Vector2.Zero;
            Vector2 C = Vector2.Zero;

            B.X = (float)(Math.Cos((_angle - _visionRange / 2.0f) * (Math.PI / 180)) * _lineOfSight) + _position.X;
            B.Y = (float)((Math.Sin((_angle - _visionRange / 2.0f) * (Math.PI / 180)) * _lineOfSight) * -1.0) + _position.Y;

            C.X = (float)(Math.Cos((_angle + _visionRange / 2.0f) * (Math.PI / 180)) * _lineOfSight) + _position.X;
            C.Y = (float)((Math.Sin((_angle + _visionRange / 2.0f) * (Math.PI / 180)) * _lineOfSight) * -1.0) + _position.Y;

            return MathExt.MathExt.checkPointInTriangle(point, A, B, C);
        }

        protected void solidCollide(CActor collider, bool knockBack = false)
        {
            //Calculate How much to move to get out of collision moving towards last collisionless point
            Collision.CHitBox otherbox = collider.hitBox;

            //Calculate how far in we went
            float distx = (collider.position.X + otherbox.center.X) - (position.X + hitBox.center.X);
            distx = (float)Math.Sqrt(distx * distx);
            float disty = (position.Y + hitBox.center.Y) - (collider.position.Y + otherbox.center.Y);
            disty = (float)Math.Sqrt(disty * disty);

            float lenx = hitBox.halfWidth + otherbox.halfWidth;
            float leny = hitBox.halfHeight + otherbox.halfHeight;

            int px = 1;
            int py = 1;

            if (collider.position.X + otherbox.center.X < position.X + hitBox.center.X)
                px = -1;
            if (collider.position.Y + otherbox.center.Y < position.Y + hitBox.center.Y)
                py = -1;

            float penx = px * (distx - lenx);
            float peny = py * (disty - leny);
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

        protected void _knockBack(float diffx, float diffy, float penx, float peny)
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

        public int drawDepth
        {
            get
            {
                return _drawDepth;
            }
            set
            {
                _drawDepth = value;
            }
        }

        public string currentImageIndex
        {
            get
            {
                return _currentImageIndex;
            }
        }

        public bool checkIfFacing(Vector2 position, DIRECTION direction)
        {
            if (_position.X >= position.X)
            {
                if (_direction == DIRECTION.LEFT && direction == DIRECTION.RIGHT)
                    return true;
            }

            if (_position.X <= position.X)
            {
                if (_direction == DIRECTION.RIGHT && direction == DIRECTION.LEFT)
                    return true;
            }

            if (_position.Y <= position.Y)
            {
                if (_direction == DIRECTION.DOWN && direction == DIRECTION.UP)
                    return true;
            }

            if (_position.Y >= position.Y)
            {
                if (_direction == DIRECTION.UP && direction == DIRECTION.DOWN)
                    return true;
            }
            return false;
        }

        protected bool _getFlagForResourceCleanup
        {
            get
            {
                return _flagForResourceCleanup;
            }
        }
    }
}
