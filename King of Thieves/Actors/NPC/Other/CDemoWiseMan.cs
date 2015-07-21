using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Other
{
    class CDemoWiseMan : CActor
    {
        private const string _SPRITE_NAMESPACE = "npc:puppup";
        private const string _IDLE = _SPRITE_NAMESPACE + ":idle";

        private bool _firstTime = false;

        private string _openingMessage = "Sup! I'm the wise guy of the demo! But even us wise guys need help! An item of mine was stolen " +
                                         "and I really need it back! It was taken by the cult known as ZFGC. I don't know who they are, but " +
                                         "they're probably a bunch of smelly basement dwellers who dress up as anime characters and " +
                                         "eat jello all day. The item is locked in the temple north of here. There's a dude walking around nearby " +
                                         "with the key. You will have to pick pocket it from him. Now because this is a demo, I'll be nice and " +
                                         "tell you the controls!";

        private string _controlsMessage = "First, these controls are a bit different than a traditional Zelda fan game. Your movement is controlled " +
                                          "by the WASD keys. If you've played Zelda before, you should be familiar with the action button. That's the" +
                                          "C key. It will allow you to do things like roll and read signs. If you haven't played Zelda before, then " +
                                          "what the heck are you doing here?! Kids these days..." +
                                          "Anyway, your items are controlled by the left and right arrow keys. For this demo, you only have access " +
                                          "to the bow. MG-Zero got a little lazy. When you have a sword, you swing it by pressing space bar. There's " +
                                          "probably some idiot out there who you can steal one from. Speaking of which! Pick pocketing is also controlled " +
                                          "by the action button. Just walk up behind someone who has made it terribly obvious they have something of value " +
                                          "on them and press C to display the pick pocket meter. All you have to do to not screw up is press C when the meter " +
                                          "is at least half full! Easy, right? Try it out when you get outside! There would normally be a pause menu, but it's " +
                                          "disabled for this demo. Maybe next time! Alright, have fun out there!";

        public CDemoWiseMan()
        {

        }

        public override void roomStart(object sender)
        {
            if (_firstTime)
            {
                CMasterControl.buttonController.createTextBox(_openingMessage);
                _state = ACTOR_STATES.IDLE;
                _firstTime = false;
            }
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if (_state == ACTOR_STATES.IDLE)
            {
                if (Actors.HUD.Text.CTextBox.messageFinished)
                {
                    startTimer0(30);
                    _state = ACTOR_STATES.IDLE_STARE;
                }
            }
        }

        public override void timer0(object sender)
        {
            CMasterControl.buttonController.createTextBox(_controlsMessage);
        }
    }
}
