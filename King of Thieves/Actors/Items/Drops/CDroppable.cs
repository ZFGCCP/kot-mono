using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.Drops
{
    public abstract class CDroppable : CActor
    {
        protected int _capacity; //how much this item provides ex: 1 heart, 10 bombs, 5 arrows
        protected const float _YIELD_VELOCITY = .5f;
        protected Vector2 _floatPosition = Vector2.Zero;
        protected string _yieldMessage = "";
        private bool _isImportant = false;


        public CDroppable(bool isImportant = false)
            : base()
        {
            _isImportant = isImportant;
        }

        public bool isImportant
        {
            get
            {
                return _isImportant;
            }
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
            _killMe = true;
        }

        protected virtual void _yieldToPlayer()
        {
            CMasterControl.buttonController.createTextBox(_yieldMessage);

            if (isImportant)
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Background:itemFanfare"]);
            else
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Background:itemFanfareSmall"]);

            _killMe = true;
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();

            _userEvents.Add(0, userEventYield);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (_state == ACTOR_STATES.YIELD)
                moveToPoint2(_floatPosition.X, _floatPosition.Y, _YIELD_VELOCITY);
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _state = ACTOR_STATES.IDLE;
            _yieldToPlayer();
        }

        protected void userEventYield(object sender)
        {
            _state = ACTOR_STATES.YIELD;
            _floatPosition.X = _position.X;
            _floatPosition.Y = _position.Y - 20;
            startTimer0(60);
        }
    }
}
