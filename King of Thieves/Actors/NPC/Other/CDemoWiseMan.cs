using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Input;
using Gears.Cloud;

namespace King_of_Thieves.Actors.NPC.Other
{
    class CDemoWiseMan : CActor
    {
        private const string _SPRITE_NAMESPACE = "npc:puppup";
        private const string _IDLE = _SPRITE_NAMESPACE + ":idle";

        private bool _firstTime = true;
        private bool _playerInSight = false;

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

        private string _greatSuccess = "WHOA!! IS THAT..?! IT IS!! I can't believe you found it!" +
                                       "How can I ever repay you? I don't quite know yet..Well, I suppose" +
                                       "You'll just have to wait until the full game for repayment! Until then, " +
                                       "feel free to roam around. Thanks for playing!";

        public CDemoWiseMan()
        {
            Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/friendly/demoPup");

            Graphics.CTextures.addTexture(_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 23, 21, 0, "0:0", "0:0"));

            _imageIndex.Add(_IDLE, new Graphics.CSprite(_IDLE));
            swapImage(_IDLE);

            _lineOfSight = 60;
            _angle = 180;
            _direction = DIRECTION.LEFT;
            _visionRange = 90;
        }

        public override void roomStart(object sender)
        {
            if (_firstTime)
            {
                CMasterControl.buttonController.createTextBox(_openingMessage);
                _state = ACTOR_STATES.IDLE;
                _firstTime = false;
            }
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["bgm:houseIntro"]);
            startTimer2(86);
        }

        public override void timer2(object sender)
        {
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["bgm:house"]);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);

            if (_state == ACTOR_STATES.IDLE)
            {
                if (Actors.HUD.Text.CTextBox.messageFinished)
                {
                    startTimer0(30);
                    _state = ACTOR_STATES.IDLE_STARE;
                }
            }
            else
            {
                if (_checkIfPointInView(playerPos) && checkIfFacing(playerPos, Player.CPlayer.glblDirection))
                {
                    _state = ACTOR_STATES.TALK_READY;
                    CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.TALK);
                    _playerInSight = true;
                }
                else
                {
                    if (_playerInSight && _state == ACTOR_STATES.TALK_READY)
                    {
                        _state = ACTOR_STATES.IDLE_STARE;
                        CMasterControl.buttonController.changeActionIconState(HUD.buttons.HUD_ACTION_OPTIONS.NONE);
                        _playerInSight = false;
                    }
                }
            }
        }

        public override void timer0(object sender)
        {
            CMasterControl.buttonController.createTextBox(_controlsMessage);
        }

        public override void keyRelease(object sender)
        {
            base.keyRelease(sender);

            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;

            if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.C))
            {
                if (!CMasterControl.buttonController.textBoxWait)
                {
                    if (CMasterControl.buttonController.actionIconState == HUD.buttons.HUD_ACTION_OPTIONS.TALK && _state == ACTOR_STATES.TALK_READY)
                        startTimer1(2);
                }
            }
        }

        public override void timer1(object sender)
        {
            if (CMasterControl.buttonController.playerHasHydrant)
            {
                CMasterControl.buttonController.createTextBox(_greatSuccess);
                _lineOfSight = 0;
            }
            else
                CMasterControl.buttonController.createTextBox("Woof! Woof!");
        }
    }
}
