using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.NPC.Enemies.Chuchus
{
    //this class should implement everything that's consistent across all chu chus
    public abstract class CBaseChuChu : CBaseEnemy
    {
        //chuchu states
        //wobble: move around
        //popup: come out of the goo pile and start wobbling towards the player
        //attack: jump at the player

        private Vector2 _jumpTo;

        public CBaseChuChu(int sight, float fov, int foh, params dropRate[] drops)
            : base(drops)
        {
            _lineOfSight = sight;
            _visionRange = fov;
            _hearingRadius = foh;
            _state = ACTOR_STATES.IDLE;
            image = _imageIndex["chuChuIdle"];
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
        }

        protected override void idle()
        {
            base.idle();

            if (!_huntPlayer)
                return;


            //70% chance of the chu chu popping up to chase the player
            if (_randNum.Next(0, 1000000) <= 10000)
            {
                swapImage("chuChuPopUp");
                _state = ACTOR_STATES.POPUP;
            }
        }
        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case ACTOR_STATES.ATTACK:
                    _state = ACTOR_STATES.WOBBLE;
                    swapImage("chuChuWobble", false);
                    break;

                case ACTOR_STATES.POPUP:
                    _state = ACTOR_STATES.WOBBLE;
                    swapImage("chuChuWobble", false);
                    break;

                case ACTOR_STATES.POPDOWN:
                    _state = ACTOR_STATES.IDLE;
                    swapImage("chuChuIdle", false);
                    break;
            }
        }

        public override void timer0(object sender)
        {

            _state = ACTOR_STATES.POPDOWN;
            swapImage("chuChuPopDown", false);
            base.timer0(sender);
        }

        protected override void chase()
        {
            //moveToPoint((int)Player.CPlayer.glblX, (int)Player.CPlayer.glblY, 3);
            moveToPoint(Player.CPlayer.glblX, Player.CPlayer.glblY, .25f);

            if (_randNum.Next(0,100000) <= 250)
            {
                _jumpTo = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
                _state = ACTOR_STATES.ATTACK;
                swapImage("chuChuHop", false);
            }

            if (MathExt.MathExt.distance(_position, new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY)) > _hearingRadius)
            {
                _state = ACTOR_STATES.IDLE;
                startTimer0(1);

                
                    

            }

            //base.chase();
        }

        //chu chus are generally retarded and will only have the hop attack
        //override this in the child classes if other functionality is needed
        protected virtual void attack()
        {
            moveToPoint((int)_jumpTo.X, (int)_jumpTo.Y, 1.0f);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case ACTOR_STATES.WOBBLE:
                    chase();
                    break;

                case ACTOR_STATES.ATTACK:
                    attack();
                    break;

                case ACTOR_STATES.POPUP:
                    break;

                case ACTOR_STATES.IDLE:
                    idle();
                    break;

                case ACTOR_STATES.POPDOWN:
                    break;

            }
        }
    }
}
