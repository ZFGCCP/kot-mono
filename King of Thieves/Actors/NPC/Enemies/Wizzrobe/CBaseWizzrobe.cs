using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.Wizzrobe
{
    enum WIZZROBE_TYPE
    {
        NORMAL = 0,
        FIRE,
        ICE,
        LIGHTNING
    }

    abstract class CBaseWizzrobe : CBaseEnemy
    {
        protected readonly WIZZROBE_TYPE _type;
        private const int _IDLE_TIME = 240; //the time between appearing and attacking/dissapearing
        private readonly int[] _VANISH_TIME = {240,180,300}; //the time they are invisible for
        private const int _ATTACK_TIME = 120; //the time for playing the attack frames
        protected readonly static string _NPC_WIZZROBE = "npc:wizzrobe";
        private static int _wizzrobeCount = 0;
        private static Vector2 _energyBallPos1 = new Vector2();
        private static Vector2 _energyBallPos2 = new Vector2();

        //image index constants
        protected readonly static string _IDLE_DOWN = "idleDown";
        protected readonly static string _ATTACK_DOWN = "attackDown";
        protected readonly static string _IDLE_LEFT = "idleLeft";
        protected readonly static string _ATTACK_LEFT = "attackLeft";
        protected readonly static string _IDLE_RIGHT = "idleRight";
        protected readonly static string _ATTACK_RIGHT = "attackRight";
        protected readonly static string _IDLE_UP = "idleUp";
        protected readonly static string _ATTACK_UP = "attackUp";
        protected readonly static string _BEAM_DOWN = "beamDown";
        protected readonly static string _BEAM_UP = "beamUp";
        protected readonly static string _BEAM_LEFT = "beamLeft";
        protected readonly static string _BEAM_RIGHT = "beamRight";

        public CBaseWizzrobe(WIZZROBE_TYPE type, params dropRate[] drops)
            : base(drops)
        {
            _type = type;
            //cache the textures needed
            if (!Graphics.CTextures.rawTextures.ContainsKey(_NPC_WIZZROBE))
                Graphics.CTextures.rawTextures.Add(_NPC_WIZZROBE, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/wizzrobe"));


            _wizzrobeCount += 1;
            _direction = DIRECTION.DOWN;
            
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if (_state == ACTOR_STATES.IDLE)
            {
                Vector2 playerPos = (Vector2)Map.CMapManager.propertyGetter("player", Map.EActorProperties.POSITION);
                lookAt(playerPos);

                switch (_direction)
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
                }
            }

        }

        public override void create(object sender)
        {
            _vanish(false);
            base.create(sender);
        }

        public override void destroy(object sender)
        {
            _wizzrobeCount--;

            if (_wizzrobeCount <= 0)
            {
                cleanUp();
                _wizzrobeCount = 0;
            }

            base.destroy(sender);
        }

        protected override void cleanUp()
        {
            Graphics.CTextures.rawTextures.Remove(_NPC_WIZZROBE);
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _vanish();
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            _appear();
        }

        public override void timer2(object sender)
        {
            base.timer2(sender);
            _startAttack();
        }

        public override void timer3(object sender)
        {
            base.timer3(sender);
            _attack();
        }

        private void _appear()
        {
            
            _state = ACTOR_STATES.IDLE;
            startTimer2(_IDLE_TIME);
            Vector2 playerPos = (Vector2)Map.CMapManager.propertyGetter("player", Map.EActorProperties.POSITION);
            _randomizePosition(playerPos);
            lookAt(playerPos);

            switch (_direction)
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
            }
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
            startTimer1(_VANISH_TIME[rand.Next(2)]);
            rand = null;
            
        }

        private void _startAttack()
        {
            
            
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _energyBallPos1.X = _position.X - 15;
                    _energyBallPos1.Y = _position.Y - 5;

                    _energyBallPos2.X = _position.X + 10;
                    _energyBallPos2.Y = _position.Y - 5;
                    swapImage(_ATTACK_DOWN);
                    break;

                case DIRECTION.UP:
                    _energyBallPos1.X = _position.X - 15;
                    _energyBallPos1.Y = _position.Y - 5;

                    _energyBallPos2.X = _position.X + 10;
                    _energyBallPos2.Y = _position.Y - 5;
                    swapImage(_ATTACK_UP);
                    break;

                case DIRECTION.LEFT:
                    _energyBallPos1.X = _position.X - 13;
                    _energyBallPos1.Y = _position.Y;

                    _energyBallPos2.X = _position.X - 13;
                    _energyBallPos2.Y = _position.Y;
                    swapImage(_ATTACK_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    _energyBallPos1.X = _position.X + 7;
                    _energyBallPos1.Y = _position.Y;

                    _energyBallPos2.X = _position.X + 7;
                    _energyBallPos2.Y = _position.Y;
                    swapImage(_ATTACK_RIGHT);
                    break;
            }
            Graphics.CEffects.createEffect(Graphics.CTextures.EFFECT_ENERGY_BALL_SMALL, _energyBallPos1, 9);
            Graphics.CEffects.createEffect(Graphics.CTextures.EFFECT_ENERGY_BALL_SMALL, _energyBallPos2, 9);
            startTimer3(_ATTACK_TIME);
            _state = ACTOR_STATES.ATTACK;
        }

        private void _attack()
        {
            _fireProjectile();
            _vanish();
        }

        protected abstract void _fireProjectile();

        public override void drawMe(bool useOverlay = false)
        {
            if (_state != ACTOR_STATES.INVISIBLE)
                base.drawMe(useOverlay);
        }

        private void _randomizePosition(Vector2 playerPosition)
        {
            Vector2 xRange = new Vector2(playerPosition.X - 60, playerPosition.X + 60);
            Vector2 yRange = new Vector2(playerPosition.Y - 60, playerPosition.Y + 60);

            _position.X = _randNum.Next((int)xRange.X, (int)xRange.Y);
            _position.Y = _randNum.Next((int)yRange.X, (int)yRange.Y);
        }

    }
}
