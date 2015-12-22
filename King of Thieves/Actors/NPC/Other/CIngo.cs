using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Other
{
    class CIngo : CActor
    {
        private string[] _openDoorDialog = { "FINALLY!! Those wretched snakes are gone! Those poor scum probably let them in the town!", "Filthy! Disgusting!"};
        private string _welcome = "Yes, yes, I have many things for sale! Don't touch anything unless you're buying it! Look, pay and GET OUT!!!";
        private string _scold = "HEY!! I SAID DON'T TOUCH ANYTHING!!";
        private string _hide = "How did you find this?! I thought I told you not to touch-";
        private string[] _hidePart2 = { "..Oh. It-it's YOU!!", "Well I'll just..umm...leave you to your sh-sh-SHOPPING!!!!" };

        private const string _SPRITE_NAMESPACE = "npc:ingo";

        private const string _WALK_DOWN = _SPRITE_NAMESPACE + ":walkDown";
        private const string _WALK_UP = _SPRITE_NAMESPACE + ":walkUp";
        private const string _IDLE_DOWN = _SPRITE_NAMESPACE + ":idleDown";
        private const string _IDLE_UP = _SPRITE_NAMESPACE + ":idleUp";
        private const string _LOOK_AROUND = _SPRITE_NAMESPACE + ":lookARound";

        public CIngo() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/friendly/ingo");

                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 32, 1,"0:0","5:0",10));
                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 32, 1, "0:1", "5:1", 10));
                Graphics.CTextures.addTexture(_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 32, 1, "2:0", "2:0", 0));
                Graphics.CTextures.addTexture(_IDLE_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 32, 1, "2:1", "2:1", 0));
                Graphics.CTextures.addTexture(_LOOK_AROUND, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 32, 1, "0:2", "1:2", 1));
            }

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_IDLE_UP));
            _imageIndex.Add(_LOOK_AROUND, new Graphics.CSprite(_LOOK_AROUND));

            _state = ACTOR_STATES.IDLE;
            _hearingRadius = 80;

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

            _position += _velocity;

            switch (_state)
            {
                case ACTOR_STATES.IDLE_STARE:
                    if (_watch())
                        CMasterControl.buttonController.createTextBox(_scold);
                    break;

                case ACTOR_STATES.IDLE:
                    if (NPC.Enemies.Rope.CBaseRope.ropeCount <= 1)
                    {
                        _state = ACTOR_STATES.MOVING;
                        swapImage(_WALK_DOWN);
                        startTimer2(60);
                        _velocity.Y = .5f;
                    }
                    break;

                case ACTOR_STATES.WOBBLE:
                    if (Actors.HUD.Text.CTextBox.messageFinished)
                    {
                        _state = ACTOR_STATES.GO_HOME;
                        swapImage(_WALK_UP);
                        _velocity.Y = -.5f;
                        startTimer3(60);
                    }
                    break;

                case ACTOR_STATES.TALK_READY:
                    if (MathExt.MathExt.checkPointInCircle(_position,new Vector2(Player.CPlayer.glblX,Player.CPlayer.glblY),_hearingRadius))
                    {
                        _state = ACTOR_STATES.WOBBLE;
                        CMasterControl.buttonController.createTextBox(_openDoorDialog);
                    }
                    break;
            }
        }

        public override void timer3(object sender)
        {
            _killMe = true;
        }

        public override void timer2(object sender)
        {
            _state = ACTOR_STATES.ALERT;
            _velocity = Vector2.Zero;
            swapImage(_LOOK_AROUND);
        }

        public override void animationEnd(object sender)
        {
            switch (_state)
            {
                case ACTOR_STATES.ALERT:
                    _state = ACTOR_STATES.TALK_READY;
                    swapImage(_IDLE_DOWN);
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
