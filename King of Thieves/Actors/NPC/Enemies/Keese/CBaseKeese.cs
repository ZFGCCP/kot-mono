using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    enum KEESETYPE
    {
        NORMAL = 0,
        FIRE,
        ICE,
        THUNDER,
        SHADOW
    }

    class CBaseKeese : CBaseEnemy
    {
        protected KEESETYPE _type;
        private double _dirChangeProbability; //The probability that the keese will change direction while flying(between 1 and 0)

        private int _flyTimeMax; //Max ammount of frames the Keese will stay in the air
        private int _flyTimeMin; //The minimum ammount of frames the Keese will have to stay in the air once it has started flying
        private int _flyTime; //how many frames since the Keese went into the air

        private int _groundTimeMax; //The max ammount of frames the Keese will stay on the ground
        private int _groundTimeMin; //The minimum ammount of frames the Keese will stay on the ground before it can fly again(Rest)
        private int _groundTime; //How many frames since it landed

        private Vector2 _attackVector; //The vector between the Keese and the player when the player first enters attack range
        private bool _attacking; //Set to true once an attack vector has been set, and false once the attack is done
        private bool _attacked; //Only attack once per flight
        private int _attackTime; //Number of frames it has been chasing

        private float _movementSpeed; //Speed while moving normaly
        private float _attackSpeed; //Movement speed when attacking

        public CBaseKeese(int foh, params dropRate[] drops)
            : base(drops)
        {
            _hearingRadius = foh;
            image = _imageIndex["keeseIdle"];
            _state = ACTOR_STATES.IDLE;
            _followRoot = false;

            _attacking = false;
            _attacked = false;
            _attackTime = 0;

            _movementSpeed = 1f;
            _attackSpeed = 1.5f;

            _type = KEESETYPE.NORMAL;
            _dirChangeProbability = 0.10;

            _flyTimeMax = 360; //6 seconds
            _flyTimeMin = 60; //1 second
            _flyTime = 0;

            _groundTimeMax = 180; //3 seconds
            _groundTimeMin = 60; //1 second
            _groundTime = 0;

            _attackVector = new Vector2();
            _attacking = false;
        }

        public override void collide(object sender, CActor collider)
        {
            if (collider is Items.Swords.CSword)
            {
                if (_state != ACTOR_STATES.KNOCKBACK)
                {
                    //start a moveback timer
                    //change state to knockBack
                    startTimer0(10);
                    _state = ACTOR_STATES.KNOCKBACK; 
                }
            }
        }

        public override void timer0(object sender)
        {
            _state = ACTOR_STATES.IDLE;
            _killMe = true;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
            _collidables.Add(typeof(Items.Swords.CSword));
        }

        protected override void applyEffects()
        {
            throw new NotImplementedException();
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            _hitBox = new Collision.CHitBox(this, 12, 8, 8, 12);

            //Initialize effect depending on type
            CActor effect = null;
            switch (_type)
            {
                case KEESETYPE.NORMAL:
                    //Do Nothing
                    return; //Skip effects
                case KEESETYPE.FIRE:
                    effect = new Effects.CFire();
                break;
                case KEESETYPE.ICE:
                    effect = new Effects.CIce();
                break;
                case KEESETYPE.SHADOW:
                    effect = new Effects.CShadow();
                break;
                case KEESETYPE.THUNDER:
                    effect = new Effects.CThunder();
                break;
            }

            if (component != null)
                component.addActor(effect, "keeseEffect");

            effect.init("keeseEffect", new Vector2(_position.X, _position.Y), "", compAddress);
            effect.layer = layer;

            component.rootDrawHeight = 1; //Draw effect behind
        }

        protected override void idle()
        {
            //Check if we should fly
            _groundTime++;
            if (_groundTime >= _groundTimeMin)
            {
                if (_groundTime > _groundTimeMax || _groundTime == _randNum.Next(_groundTimeMin, _groundTimeMax))
                {
                    //Fly
                    _state = ACTOR_STATES.FLYING;
                    swapImage("keeseFly");
                    _flyTime = 0; //Reset flytime
                }
            }

        }

        protected override void chase()
        {
            //Follow the attack vector
            if(!_attacking)
            {
                _attackVector.X = Player.CPlayer.glblX - _position.X;
                _attackVector.Y = Player.CPlayer.glblY - _position.Y;
                _attackVector.Normalize(); //Make it a unit vector(Direction, not speed)
                _attacking = true;
                _attackTime = 0;
            }

            //Move in the attack vector
            _attackTime++;
            _position += _attackVector * _attackSpeed;
            if (_attackTime > _hearingRadius / _attackSpeed) //Move only the max hearing distance
            {
                _attacked = true;
                _attacking = false;
                _state = ACTOR_STATES.FLYING;
            }

        }

        protected virtual void fly()
        {
            //Check if we are to change direction
            if (_randNum.NextDouble() >= 1 - _dirChangeProbability)
                _direction = (DIRECTION)_randNum.Next(0, 4);

            //Fly in the current direction
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _position.Y += _movementSpeed;
                    break;

                case DIRECTION.LEFT:
                    _position.X -= _movementSpeed;
                    break;

                case DIRECTION.RIGHT:
                    _position.X += _movementSpeed;
                    break;

                case DIRECTION.UP:
                    _position.Y -= _movementSpeed;
                    break;
            }

            //Check if we should land
            _flyTime++;
            if (_flyTime >= _flyTimeMin)
            {
                if (_flyTime > _flyTimeMax || _flyTime == _randNum.Next(_flyTimeMin, _flyTimeMax))
                {
                    //Land
                    _state = ACTOR_STATES.IDLE;
                    swapImage("keeseIdle");
                    _groundTime = 0; //Reset ground time
                    _attacked = false;
                    return; //Bypass attack check
                }
            }

            //Check if we should attack the player
            if (!_attacked && hunt())
                _state = ACTOR_STATES.CHASE;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case ACTOR_STATES.IDLE:
                    idle();
                    break;

                case ACTOR_STATES.FLYING:
                    fly();
                    break;

                case ACTOR_STATES.CHASE:
                    chase();
                    break;
            }

            //Make sure we don't go out of bounds
            //if(_position.X < 0)
            //    _position.X = 0;
            //TODO: Do we know map sizes yet?
            //MGZero: Nope
        }
    }
}
