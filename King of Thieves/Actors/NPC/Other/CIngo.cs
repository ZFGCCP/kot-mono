using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Other
{
    class CIngo : CActor
    {
        private string[] _openDoorDialog = { "FINALLY!! Those wretched snakes are gone! Those poor scum probably let them in the town!", "Filthy! Disgusting!" };
        private string _welcome = "Yes, yes, I have many things for sale! Don't touch anything unless you're buying it! Look, pay and GET OUT!!!";
        private string _scold = "HEY!! I SAID DON'T TOUCH ANYTHING!!";
        private string _hide = "How did you find this?! I thought I told you not to touch-";
        private string[] _hidePart2 = { "..Oh. It-it's YOU!!", "Well I'll just..umm...leave you to your sh-sh-SHOPPING!!!!" }; 

        public CIngo() :
            base()
        {

        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            if (Convert.ToBoolean(additional[0]))
                _state = ACTOR_STATES.IDLE_STARE;
            else
                _state = ACTOR_STATES.IDLE;
        }

        public override void roomStart(object sender)
        {
            if (_state == ACTOR_STATES.IDLE_STARE)
                startTimer0(360);
        }

        public override void timer0(object sender)
        {
            _state = ACTOR_STATES.IDLE;
            startTimer1(180);
        }

        public override void timer1(object sender)
        {
            _state = ACTOR_STATES.IDLE_STARE;
            startTimer0(360);
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case ACTOR_STATES.IDLE_STARE:
                    if (_watch())
                        CMasterControl.buttonController.createTextBox(_scold);
                    break;
            }
        }

        private bool _watch()
        {
            if (this.component.actors != null && this.component.actors.Count() > 0)
            {

                return true;
            }
            return false;
        }

        private void _lookAway()
        {

        }
        
    }
}
