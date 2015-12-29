using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Rump
{
    class CRump : CBaseEnemy
    {


        private string _dialog = "Looking for me? It would seem you have a small problem! You see..Someone seems to have let a small family of snakes out into your town." +
                                 "So you've come for MY help?! Hyeheheh! What would you need? A dark curse? Perhaps a potion that makes you think you can talk to animals!" +
                                 "Whatever you want, it's yours..! For a price! How about a deal? I give you this sword...";

        private string _dialogContinued = "Aaand you use it to kill all those snakes.  In return, I ask that you bring me a bottle of gold fairy dust!";

        private string _dialogContinued2 = "Not an easy task?!";
        private string _dialogContinued3 = "It's simple, dearie!  Just find the curiosity shop and it's yours! And of course by yours, I mean mine! Hyeheheheh!";

        //curiosity shop dialog
        private string[] _shopDialog = { "Excellent work, dearie! This shop keeper has been a thorn in my side for far too long! Now..", "...Kill him!","...?! Letting him get away..Oh fine! It wasn't part of our agreement anyway! Now, give the fairy dust to me!" };
        private string[] _shopDialog2 = { "No! NO! We had a DEAL! You WILL pay the price!" };

        //end combat dialog
        private string _endDialog = "Enough! I should have just done this in the first place!";
        private string _endDialog2 = "?! ..W-WHAT ARE YOU..?! NO!!!!";
        private static bool _openingDialog = true;

        private const string _SPRITE_NAMESPACE = "rump";
        private const string _RUMP_IDLE_DOWN = _SPRITE_NAMESPACE + ":idleDown";
        private const string _RUMP_GESTURE = _SPRITE_NAMESPACE + ":gesture";
        private const string _RUMP_GESTURE_IDLE = _SPRITE_NAMESPACE + ":gestureIdle";

        private bool _battleMode = false;
        private bool _shopMode = false;

        public CRump() :
            base()
        {

            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/rump");

                Graphics.CTextures.addTexture(_RUMP_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "0:0"));
                Graphics.CTextures.addTexture(_RUMP_GESTURE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "1:0", "6:0", 15));
                Graphics.CTextures.addTexture(_RUMP_GESTURE_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "7:0", "7:0"));
            }

            _imageIndex.Add(_RUMP_IDLE_DOWN, new Graphics.CSprite(_RUMP_IDLE_DOWN));
            _imageIndex.Add(_RUMP_GESTURE_IDLE, new Graphics.CSprite(_RUMP_GESTURE_IDLE));
            _imageIndex.Add(_RUMP_GESTURE, new Graphics.CSprite(_RUMP_GESTURE));

            _hitBox = new Collision.CHitBox(this, 10, 18, 12, 15);
        }

        public override void roomStart(object sender)
        {
            if (_openingDialog)
            {
                CMasterControl.buttonController.createTextBox(_dialog, _dialogContinued, _dialogContinued2);
                _state = ACTOR_STATES.TALK_READY;
                swapImage(_RUMP_IDLE_DOWN,false);
            }
            else if(_battleMode)
            {
                _vanish();
            }
        }

        public override void create(object sender)
        {
            if (_shopMode)
            {
                _state = ACTOR_STATES.IDLE;
                startTimer3(70);
                swapImage(_RUMP_IDLE_DOWN);
                Graphics.CEffects.createEffect(Graphics.CEffects.SMOKE_POOF, _position);
            }
        }

        public override void init(string name, Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            if (additional != null && additional.Length > 0)
            {
                if (additional[0] == "true")
                    _shopMode = true;
                else if (additional[0] == "false")
                    _battleMode = true;
            }
            else
                _openingDialog = true;

            base.init(name, position, dataType, compAddress, additional);

        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (_battleMode)
            {

            }
            else
            {
                if (_state == ACTOR_STATES.TALK_READY && Actors.HUD.Text.CTextBox.messageFinished)
                {
                    if (_openingDialog)
                    {
                        _openingDialog = false;
                        _taunt();
                    }
                    else
                        _killMe = true;
                }
                else if (_state == ACTOR_STATES.IDLE_STARE && Actors.HUD.Text.CTextBox.messageFinished)
                {
                    startTimer0(60);
                }
                else if (_state == ACTOR_STATES.ALERT && Actors.HUD.Text.CTextBox.messageFinished)
                {
                    startTimer2(60);
                }
            }
        }

        public override void timer3(object sender)
        {
            int ingoAddress = Map.CMapManager.getActorComponentAddress("ingo");

            CMasterControl.commNet[ingoAddress].Add(new CActorPacket(0, "ingo", this));
        }

        private void _taunt()
        {
            swapImage(_RUMP_GESTURE);
        }

        private void _chargeFireBall()
        {

        }

        private void _shootFireBall()
        {

        }

        private void _shopDialogBegin(object sender)
        {
            startTimer4(30);
            _state = ACTOR_STATES.USER_STATE0;
        }

        protected override void dialogBegin(object sender)
        {
            if (_state == ACTOR_STATES.USER_STATE0)
                _currentDialog = _shopDialog;
            else if (_state == ACTOR_STATES.USER_STATE1)
                _currentDialog = _shopDialog2;

            base.dialogBegin(sender);
        }

        protected override void dialogEnd(object sender)
        {
            if (_state == ACTOR_STATES.USER_STATE0)
            {
                _state = ACTOR_STATES.USER_STATE1;
                startTimer4(60);
            }
            else if(_state == ACTOR_STATES.USER_STATE1)
            {
                CMasterControl.mapManager.cacheMaps(false, "rumpleBattle.xml");
                CMasterControl.mapManager.swapMap("rumpleBattle.xml", "player", new Vector2(129, 161),Map.CMapManager.TRANSITION_RUMPLE_SWIRL);
            }
            base.dialogEnd(sender);
        }


        public override void timer4(object sender)
        {
            _triggerTextEvent();
        }
        protected override void _registerUserEvents()
        {
            base._registerUserEvents();
            _userEvents.Add(0, _shopDialogBegin);
        }

        private void _appear()
        {

            _state = ACTOR_STATES.IDLE;
            Vector2 playerPos = (Vector2)Map.CMapManager.propertyGetter("player", Map.EActorProperties.POSITION);
            lookAt(playerPos);

            /*switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(_IDLE_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(_IDLE_UP);
                    break;

                case DIRECTION.LEFT:
                    swapImage(_IDLE_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(_IDLE_RIGHT);
                    break;
            }*/
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Npc:wizzrobe:vanish"]);
            Graphics.CEffects.createEffect(Graphics.CEffects.SMOKE_POOF, new Vector2(_position.X - 13, _position.Y - 5));
        }

        private void _vanish(bool showEffect = true)
        {
            if (showEffect)
            {
                Graphics.CEffects.createEffect(Graphics.CEffects.SMOKE_POOF, new Vector2(_position.X - 13, _position.Y - 5));
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Npc:wizzrobe:vanish"]);
            }

            _state = ACTOR_STATES.INVISIBLE;

            Random rand = new Random();
            startTimer1(180);
            rand = null;

        }

        public override void timer1(object sender)
        {
            _appear();
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

        public override void collide(object sender, CActor collider)
        {
            if (collider is Player.CPlayer)
            {
                collider.dealDamange(2, collider);
            }
            if (collider is Items.Swords.CSword)
            {
                collider.shock();
            }
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
            _collidables.Add(typeof(Projectiles.CArrow));
            _collidables.Add(typeof(Items.Swords.CSword));
        }


    }
}
