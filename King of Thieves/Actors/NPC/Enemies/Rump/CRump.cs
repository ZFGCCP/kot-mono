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
        private string[] _endDialog = { "Enough! I should have just done this in the first place!" };
        private string[] _endDialog2 = { "THE FAIRY DUST?! ..W-WHAT ARE YOU..?! NO!!!!" };
        private static bool _openingDialog = true;

        private const string _SPRITE_NAMESPACE = "rump";
        private const string _RUMP_IDLE_DOWN = _SPRITE_NAMESPACE + ":idleDown";
        private const string _RUMP_GESTURE = _SPRITE_NAMESPACE + ":gesture";
        private const string _RUMP_GESTURE_IDLE = _SPRITE_NAMESPACE + ":gestureIdle";

        private const string _RUMP_THROW_FIREBALL_UP = _SPRITE_NAMESPACE + ":throwUp";
        private const string _RUMP_THROW_FIREBALL_DOWN = _SPRITE_NAMESPACE + ":throwDown";
        private const string _RUMP_THROW_FIREBALL_LEFT = _SPRITE_NAMESPACE + ":throwLeft";
        private const string _RUMP_THROW_FIREBALL_RIGHT = _SPRITE_NAMESPACE + ":throwRight";

        private bool _battleMode = false;
        private bool _shopMode = false;
        private int _currentPositionIndex = 0;
        private bool _wasStruckByArrow = false;

        private bool _isReal = false;

        private static List<Vector2> _allowedPositionList = new List<Vector2>();
        private static Stack<Vector2> _removedPositions = new Stack<Vector2>();
        private bool _defeatVanish = false;

        public CRump() :
            base()
        {

            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/rump");

                Graphics.CTextures.addTexture(_RUMP_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:0", "0:0"));
                Graphics.CTextures.addTexture(_RUMP_GESTURE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "1:0", "6:0", 15));
                Graphics.CTextures.addTexture(_RUMP_GESTURE_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "7:0", "7:0"));

                Graphics.CTextures.addTexture(_RUMP_THROW_FIREBALL_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:3", "10:3",15));
                Graphics.CTextures.addTexture(_RUMP_THROW_FIREBALL_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:1", "10:1", 15));
                Graphics.CTextures.addTexture(_RUMP_THROW_FIREBALL_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:2", "10:2", 15));
            }

            _imageIndex.Add(_RUMP_IDLE_DOWN, new Graphics.CSprite(_RUMP_IDLE_DOWN));
            _imageIndex.Add(_RUMP_GESTURE_IDLE, new Graphics.CSprite(_RUMP_GESTURE_IDLE));
            _imageIndex.Add(_RUMP_GESTURE, new Graphics.CSprite(_RUMP_GESTURE));

            _imageIndex.Add(_RUMP_THROW_FIREBALL_UP, new Graphics.CSprite(_RUMP_THROW_FIREBALL_UP));
            _imageIndex.Add(_RUMP_THROW_FIREBALL_DOWN, new Graphics.CSprite(_RUMP_THROW_FIREBALL_DOWN));
            _imageIndex.Add(_RUMP_THROW_FIREBALL_LEFT, new Graphics.CSprite(_RUMP_THROW_FIREBALL_LEFT));
            _imageIndex.Add(_RUMP_THROW_FIREBALL_RIGHT, new Graphics.CSprite(_RUMP_THROW_FIREBALL_LEFT,true));

            _hitBox = new Collision.CHitBox(this, 10, 18, 12, 15);
            _followRoot = false;
            _hp = 2;
        }

        //only call this one once!
        private void _fullReconstructAllowedPositions()
        {
            _allowedPositionList.Clear();
            _removedPositions.Clear();
            _allowedPositionList.Add(new Vector2(38, 38));
            _allowedPositionList.Add(new Vector2(73, 38));
            _allowedPositionList.Add(new Vector2(200, 38));
            _allowedPositionList.Add(new Vector2(38, 135));
            _allowedPositionList.Add(new Vector2(231, 177));
            _allowedPositionList.Add(new Vector2(91, 210));
            _allowedPositionList.Add(new Vector2(175, 210));
        }

        //use this one otherwise
        private void _reconstructAllowedPositions()
        {
            while (_removedPositions.Count > 0)
                _allowedPositionList.Add(_removedPositions.Pop());
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
                {
                    _battleMode = true;
                    _fullReconstructAllowedPositions();
                }

                if (additional.Length >= 2 && additional[1] == "true")
                    _isReal = true;
                else
                    _isReal = false;
            }
            else
                _openingDialog = true;

            base.init(name, position, dataType, compAddress, additional);

        }

        public override void update(GameTime gameTime)
        {
            if (_hp <= 0)
            {
                _hp = 1;
                _state = ACTOR_STATES.VAULT;
            }

            base.update(gameTime);

            if(!_battleMode)
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
                else if(_state == ACTOR_STATES.ATTACK)
                {
                    _currentDialog = _endDialog;
                    _triggerTextEvent();
                }
            }
            else
            {
                if (_state == ACTOR_STATES.VAULT)
                {
                    _vanish(true, true);
                    _killClones();
                }
            }
        }

        public override void timer3(object sender)
        {
            int ingoAddress = Map.CMapManager.getActorComponentAddress("ingo");

            CMasterControl.commNet[ingoAddress].Add(new CActorPacket(0, "ingo", this));
        }

        public override void destroy(object sender)
        {
            base.destroy(sender);

            if (_battleMode)
            {
                CRump rump = new CRump();
                rump.init("rumpEndSequence", _position, "", CReservedAddresses.NON_ASSIGNED);
                rump._state = ACTOR_STATES.ATTACK;
                Map.CMapManager.addComponent(rump, new Dictionary<string, CActor>());
            }
        }

        private void _taunt()
        {
            swapImage(_RUMP_GESTURE);
        }

        private void _chargeFireBall()
        {
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            lookAt(playerPos);
            _state = ACTOR_STATES.CHARGING_ARROW;
            switch (_direction)
            {
                case DIRECTION.LEFT:
                    swapImage(_RUMP_THROW_FIREBALL_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(_RUMP_THROW_FIREBALL_RIGHT);
                    break;

                case DIRECTION.DOWN:
                    swapImage(_RUMP_THROW_FIREBALL_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(_RUMP_THROW_FIREBALL_UP);
                    break;
            }
        }

        private void _shootFireBall()
        {
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            double angleBetween = MathExt.MathExt.angle(_position, playerPos);

            Vector2 velocity = MathExt.MathExt.calculateVectorComponents(2.0f, (float)angleBetween);
            Projectiles.CFireBall fireBall = new Projectiles.CFireBall(_direction, velocity, _position);
            fireBall.init("fireBall" + _name, _position, "", this.componentAddress);
            Map.CMapManager.addActorToComponent(fireBall, this.componentAddress);
        }

        private void _shopDialogBegin(object sender)
        {
            startTimer4(30);
            _state = ACTOR_STATES.USER_STATE0;
        }

        private void _arrowResponse()
        {
            ((CRump)component.root)._instructClones();
            if (_isReal)
            {
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Npc:bossHit"]);
                dealDamange(1, this);
            }
        }

        private void _instructClones()
        {
            foreach(KeyValuePair<String,CActor> kvp in component.actors)
            {
                if (kvp.Value is CRump)
                {
                    CRump rump = (CRump)kvp.Value;
                    rump._vanish();
                    rump.stopTimer5();
                }
            }

            ((CRump)component.root)._vanish();
            ((CRump)component.root).stopTimer5();
        }

        private void _killClones()
        {
            foreach (KeyValuePair<String, CActor> kvp in component.actors)
            {
                if (kvp.Value is CRump)
                {
                    CRump rump = (CRump)kvp.Value;
                    rump.killMe = true;
                }
            }
        }

        protected override void dialogBegin(object sender)
        {
            if (_state == ACTOR_STATES.USER_STATE0)
                _currentDialog = _shopDialog;
            else if (_state == ACTOR_STATES.USER_STATE1)
                _currentDialog = _shopDialog2;
            else if (_state == ACTOR_STATES.CHASE)
                _currentDialog = _endDialog;
            else if (_state == ACTOR_STATES.PANIC)
                _currentDialog = _endDialog2;

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
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Background:teleportWoosh"]);
            }
            else if(_state == ACTOR_STATES.CHASE)
                startTimer6(180);
            else if(_state == ACTOR_STATES.PANIC)
                CMasterControl.commNet[CReservedAddresses.PLAYER].Add(new CActorPacket(1, "player", this));

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
            _userEvents.Add(1, _notTheDust);
        }

        private void _appear()
        {

            if (!_defeatVanish)
            {
                _state = ACTOR_STATES.IDLE;
                Vector2 playerPos = (Vector2)Map.CMapManager.propertyGetter("player", Map.EActorProperties.POSITION);

                int positionIndex = _randNum.Next(0, _allowedPositionList.Count - 1);
                jumpToPoint(_allowedPositionList[positionIndex].X, _allowedPositionList[positionIndex].Y);
                lookAt(playerPos);
                _currentPositionIndex = positionIndex;

                _removeCurrentPosition();
                swapImage(_RUMP_IDLE_DOWN);
                startTimer5(180);
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
            }
            else
            {
                _state = ACTOR_STATES.CHASE;
                _triggerTextEvent();
                CMasterControl.commNet[CReservedAddresses.PLAYER].Add(new CActorPacket(0, "player", this));
            }
            CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Npc:wizzrobe:vanish"]);
            Graphics.CEffects.createEffect(Graphics.CEffects.SMOKE_POOF, new Vector2(_position.X - 13, _position.Y - 5));
            
        }

        public override void timer5(object sender)
        {
            _chargeFireBall();
        }

        private void _vanish(bool showEffect = true, bool goCenter = false)
        {
            if (showEffect)
            {
                Graphics.CEffects.createEffect(Graphics.CEffects.SMOKE_POOF, new Vector2(_position.X - 13, _position.Y - 5));
                CMasterControl.audioPlayer.addSfx(CMasterControl.audioPlayer.soundBank["Npc:wizzrobe:vanish"]);
            }

            _state = ACTOR_STATES.INVISIBLE;

            Vector2 _currentPosition = new Vector2(_position.X, _position.Y);

            if (!goCenter)
            {
                jumpToPoint(1000, 1000);

                startTimer1(180);

                if (!_isReal)
                {
                    if (!_wasStruckByArrow)
                        _allowedPositionList.Add(_currentPosition);
                }

                if ((_isReal && _wasStruckByArrow) || _removedPositions.Count == 0 || _allowedPositionList.Count < 3)
                    _fullReconstructAllowedPositions();

                _wasStruckByArrow = false;
            }
            else
            {
                jumpToPoint(128, 128);
                startTimer1(60);
                _defeatVanish = true;
            }

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

                case ACTOR_STATES.CHARGING_ARROW:
                    _shootFireBall();
                    _vanish();
                    break;
            }
        }

        public override void collide(object sender, CActor collider)
        {
            if (collider is Player.CPlayer)
                collider.dealDamange(2, collider);

            if (collider is Items.Swords.CSword && !_isReal)
                collider.shock();

            if (collider is Projectiles.CArrow)
            {
                _wasStruckByArrow = true;

                if (!_isReal)
                {
                    _removeCurrentPosition(true);
                    Items.Drops.CArrowDrop arrow = new Items.Drops.CArrowDrop();
                    arrow.init(_name + "_arrowDrop" + Collision.CHitBox.produceRandomName(), _position, "", this.componentAddress);
                    arrow.layer = this.layer;
                    Map.CMapManager.addActorToComponent(arrow, this.componentAddress);
                }

                collider.killMe = true;

                _arrowResponse();
            }
        }

        private void _removeCurrentPosition(bool cacheIt = false)
        {
            Vector2 cache = Vector2.Zero;

            if (_allowedPositionList.Count <= _currentPositionIndex)
                return;

            cache = _allowedPositionList[_currentPositionIndex];

            _allowedPositionList.RemoveAt(_currentPositionIndex);

            if (cacheIt)
                _removedPositions.Push(cache);
        }

        protected override void _addCollidables()
        {
            _collidables.Add(typeof(Player.CPlayer));
            _collidables.Add(typeof(Items.Swords.CSword));
            _collidables.Add(typeof(Projectiles.CArrow));
        }

        public bool isReal
        {
            get
            {
                return _isReal;
            }
        }

        private void _notTheDust(object sender)
        {
            _state = ACTOR_STATES.PANIC;
            _triggerTextEvent();
        }
    }
}
