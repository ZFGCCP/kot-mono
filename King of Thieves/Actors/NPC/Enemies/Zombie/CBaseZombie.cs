using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.NPC.Enemies.Zombie
{
    class CBaseZombie : CBaseEnemy
    {
        private const int _SCREECH_RADIUS = 120;
        private static int _zombieCount = 0;
        protected const string _SCREECHER = "schreecher";
        protected bool _screecherExists = false;

        protected const string _SPRITE_NAMESPACE = "npc:zombie";
        private int _shakeOffMeter = 0;
        protected int _shakeOffThreshold = 0;
        private Vector2 _shakeOffVelocity = Vector2.Zero;
        protected int _damagePerSec = 0;
        protected Actors.CActor _actorToHug = null;

        public CBaseZombie()
            : base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/zombie");

            _zombieCount += 1;
        }

        public void killScreecher()
        {
            _screecherExists = false;
        }

        public override void timer2(object sender)
        {
            if (_actorToHug == null)
                return;

            _actorToHug.dealDamange(_damagePerSec, _actorToHug);

            if (_state == ACTOR_STATES.HOLD)
                startTimer2(60);
        }

        public override void keyRelease(object sender)
        {
            if (_state == ACTOR_STATES.HOLD)
            {
                CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;

                if (input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.C))
                    _shakeOffMeter++;
            }
        }

        protected void _shootScreech()
        {
            _screecherExists = true;
            Vector2 screechVelocity = _prepareScreechVelocity();
            Vector2 screechPosition = _position + new Vector2(20, 20);
            CZombieScreecher screecher = new CZombieScreecher(_direction, screechVelocity, screechPosition);
            screecher.init(_name + _SCREECHER, screechPosition, "", this.componentAddress);
            Map.CMapManager.addActorToComponent(screecher, this.componentAddress);
        }

        public override void destroy(object sender)
        {
            _zombieCount -= 1;
            _doNpcCountCheck(ref _zombieCount);
            base.destroy(sender);
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Actors.Collision.CSolidTile));
            _collidables.Add(typeof(Actors.Player.CPlayer));
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);

            switch (_state)
            {
                case ACTOR_STATES.MOVING:
                    if (_checkIfPointInView(new Vector2(playerPos.X, playerPos.Y)))
                    {
                        if (!_screecherExists)
                            _shootScreech();
                    }
                    else if (isPointInHearingRange(playerPos))
                        moveToPoint2(playerPos.X, playerPos.Y, .25f);

                break;

                case ACTOR_STATES.SHOOK_OFF:
                    _shakeOff();
                break;

                default:
                    break;
            }
        }

        private void _shakeOff()
        {
            moveInDirection(_shakeOffVelocity);
        }

        private Vector2 _prepareScreechVelocity()
        {
            Vector2 output = Vector2.Zero;
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    output.Y = 4;
                    break;

                case DIRECTION.UP:
                    output.Y = -4;
                    break;

                case DIRECTION.LEFT:
                    output.X = -4;
                    break;

                case DIRECTION.RIGHT:
                    output.X = 4;
                    break;

                default:    
                    break;
            }
            return output;
        }

        protected int shakeOffMeter
        {
            get
            {
                return _shakeOffMeter;
            }
        }

        protected void resetShakeOffMeter()
        {
            _shakeOffMeter = 0;
        }

        protected void _setShakeOffVelo()
        {
            _shakeOffVelocity = Vector2.Zero;
            switch (_direction)
            {
                case DIRECTION.DOWN:
                    _shakeOffVelocity.Y = -4;
                    break;

                case DIRECTION.UP:
                    _shakeOffVelocity.Y = 4;
                    break;

                case DIRECTION.LEFT:
                    _shakeOffVelocity.X = 4;
                    break;

                case DIRECTION.RIGHT:
                    _shakeOffVelocity.X = -4;
                    break;

                default:
                    break;
            }
        }
    }
}
