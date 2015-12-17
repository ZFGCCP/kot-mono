using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Rump
{
    class CRump : CActor
    {


        private string _dialog = "Looking for me? It would seem you have a small problem! You see..Someone seems to have let a small family of snakes out into your town." +
                                 "So you've come for MY help?! Hyeheheh! What would you need? A dark curse? Perhaps a potion that makes you think you can talk to animals!" +
                                 "Whatever you want, it's yours..! For a price! How about a deal? I give you this sword...";

        private string _dialogContinued = "Aaand you use it to kill all those snakes.  In return, I ask that you bring me a bottle of gold fairy dust!";

        private string _dialogContinued2 = "Not an easy task?!";
        private string _dialogContinued3 = "It's simple, dearie!  Just find the curiosity shop and it's yours! And of course by yours, I mean mine! Hyeheheheh!";

        //curiosity shop dialog
        private string _shopDialog = "Excellent work, dearie! This shop keeper has been a thorn in my side for far too long! Now..";
        private string _shopDialog2 = "...Kill him!";
        private string _shopDialog3 = "...?! Letting him get away..Oh fine! It wasn't part of our agreement anyway! Now, give the fairy dust to me!";
        private string _shopDialog4 = "No! NO! We had a DEAL! You WILL pay the price!";

        //end combat dialog
        private string _endDialog = "Enough! I should have just done this in the first place!";
        private string _endDialog2 = "?! ..W-WHAT ARE YOU..?! NO!!!!";
        private static bool _openingDialog = true;

        private const string _SPRITE_NAMESPACE = "rump";
        private const string _RUMP_IDLE_DOWN = _SPRITE_NAMESPACE + ":idleDown";
        private const string _RUMP_GESTURE = _SPRITE_NAMESPACE + ":gesture";
        private const string _RUMP_GESTURE_IDLE = _SPRITE_NAMESPACE + ":gestureIdle";

        public CRump() :
            base()
        {
            Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/rump");

            Graphics.CTextures.addTexture(_RUMP_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "0:0"));
            Graphics.CTextures.addTexture(_RUMP_GESTURE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "1:0", "6:0", 15));
            Graphics.CTextures.addTexture(_RUMP_GESTURE_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "7:0", "7:0"));

            _imageIndex.Add(_RUMP_IDLE_DOWN, new Graphics.CSprite(_RUMP_IDLE_DOWN));
            _imageIndex.Add(_RUMP_GESTURE_IDLE, new Graphics.CSprite(_RUMP_GESTURE_IDLE));
            _imageIndex.Add(_RUMP_GESTURE, new Graphics.CSprite(_RUMP_GESTURE));
        }

        public override void roomStart(object sender)
        {
            if (_openingDialog)
            {
                CMasterControl.buttonController.createTextBox(_dialog, _dialogContinued, _dialogContinued2);
                _state = ACTOR_STATES.TALK_READY;
                swapImage(_RUMP_IDLE_DOWN,false);
            }
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _openingDialog = true;

        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (_state == ACTOR_STATES.TALK_READY && Actors.HUD.Text.CTextBox.messageFinished)
            {
                if (_openingDialog)
                {
                    _openingDialog = false;
                    swapImage(_RUMP_GESTURE);
                }
                else
                {
                    Graphics.CEffects.createEffect(Graphics.CEffects.SMOKE_POOF, new Vector2(_position.X - 10,_position.Y));
                    _killMe = true;
                }
            }
        }

        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case ACTOR_STATES.TALK_READY:
                    swapImage(_RUMP_GESTURE_IDLE);
                    CMasterControl.buttonController.createTextBox(_dialogContinued3);
                    break;
            }
        }


    }
}
