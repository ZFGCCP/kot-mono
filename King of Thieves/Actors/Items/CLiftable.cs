using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Actors.Collision;

namespace King_of_Thieves.Actors.Items
{
    class CLiftable : Collision.CSolidTile
    {
        private CActor _collider = null;
        private CActor _thrower = null;
        protected double _mass;

        public CLiftable() :
            base()
        {
            _followRoot = true;
            
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Player.CPlayer));
            _collidables.Add(typeof(CSolidTile));

            
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();
            _userEvents.Add(0, _toss);
        }

        private void solidCollide(CActor collider)
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

            if (diffx < diffy)
                _position.X += penx; //TODO: dont make a new vector every time
            else if (diffx > diffy)
                _position.Y += peny; //Same here 
            else
                position = new Vector2(position.X + penx, position.Y + peny); //Corner cases 
        }

        private void _toss(object sender)
        {
            _direction = (DIRECTION)userParams[0];
            _state = ACTOR_STATES.TOSSING;
            CActor _sender = (CActor)sender;

            if (_sender.direction == DIRECTION.DOWN)
                startTimer1(48); //direction down needs more time to throw due to height difference
            else
                startTimer1(15);

            _thrower = _sender;
            //_sender.component.removeActor(this, true);
            this.component = this._oldComponent;
            this.component.enabled = true;
            
        }

        public override void animationEnd(object sender)
        {
            if (_state == ACTOR_STATES.SMASH) //ELLO EEELYYYYYZZAAAAA
            {
                this.name = _oldName;
                _killMe = true;
            }
        }

        public override void timer0(object sender)
        {

            _state = ACTOR_STATES.CARRY;
            this.component.enabled = false;
            this._oldComponent = this.component;
            this._oldName = this.name;
            this.name = "carryMe";
            _collider.component.addActor(this, _name);
            
        }

        public override void timer1(object sender)
        {
            _state = ACTOR_STATES.SMASH;
            _position.Y += 12;
            _thrower.component.removeActor(this, true);
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Items:Decor:ItemSmash"]);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case ACTOR_STATES.LIFT:
                    switch (_direction)
                    {
                        case DIRECTION.UP:
                            _position.Y -= 2;
                            break;

                        case DIRECTION.RIGHT:
                            _position.Y -= 1;
                            _position.X += .9f;
                            break;

                        case DIRECTION.LEFT:
                            _position.Y -= 1;
                            _position.X -= .8f;
                            break;

                        case DIRECTION.DOWN:
                            _position.Y -= .1f;
                            break;
                    }
                    break;

                case ACTOR_STATES.TOSSING:
                    
                    switch (_direction)
                    {
                        case DIRECTION.UP:
                            _position.Y -= _velocity.Y;
                            break;

                        case DIRECTION.DOWN:
                            _position.Y += _velocity.Y;
                            break;

                        case DIRECTION.LEFT:
                            _position.X -= _velocity.X;
                            break;

                        case DIRECTION.RIGHT:
                            _position.X += _velocity.X;
                            break;

                    }
                    break;
            }
        }

        public double mass
        {
            get
            {
                return _mass;
            }
        }

        public override void collide(object sender, CActor collider)
        {
            if (_state != ACTOR_STATES.LIFT || _state != ACTOR_STATES.CARRY)
            {
                if (collider is CSolidTile)
                {
                    solidCollide(collider);
                }

                if (collider is Player.CPlayer)
                {
                    //check if the player lifted this
                    if (CMasterControl.glblInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                    {
                        collider.state = ACTOR_STATES.LIFT;
                        _state = ACTOR_STATES.LIFT;
                        _collider = collider;
                        noCollide = true;

                        //center with the player's hitbox
                        DIRECTION movein = collider.direction;
                        startTimer0(15);
                        switch (movein)
                        {
                            case DIRECTION.DOWN:
                                _direction = DIRECTION.UP;
                                jumpToPoint(collider.position.X - 8, _position.Y);
                                break;

                            case DIRECTION.UP:
                                _direction = DIRECTION.DOWN;
                                jumpToPoint(collider.position.X - 8, _position.Y);
                                break;

                            case DIRECTION.LEFT:
                                _direction = DIRECTION.RIGHT;
                                jumpToPoint(_position.X, collider.position.Y - 16);
                                break;

                            case DIRECTION.RIGHT:
                                _direction = DIRECTION.LEFT;
                                jumpToPoint(_position.X, collider.position.Y - 16);
                                break;
                        }
                        _collider = collider;

                    //moveToPoint((moveTo.X + collider.position.X), (moveTo.Y + collider.position.Y), 30);
                    }
                    else
                    {
                    //get the direction the player walked into this at
                        DIRECTION movein = collider.direction;

                        switch (movein)
                        {
                            case DIRECTION.DOWN:

                                _position.Y += collider.velocity.Y;
                                break;

                            case DIRECTION.UP:
                                _position.Y -= collider.velocity.Y;
                                break;

                            case DIRECTION.LEFT:
                                _position.X -= collider.velocity.X;
                                break;

                            case DIRECTION.RIGHT:
                                _position.X += collider.velocity.X;
                                break;
                         }
                    }
                }
            }
        }
    }
}
