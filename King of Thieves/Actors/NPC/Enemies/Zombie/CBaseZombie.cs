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


        public CBaseZombie()
            : base()
        {
            if (_zombieCount <= 0)
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/zombie");

            _zombieCount += 1;
        }

        protected void _killScreecher()
        {
            _screecherExists = false;
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

        protected override void cleanUp()
        {
            if (_zombieCount <= 0)
                Graphics.CTextures.cleanUp(_SPRITE_NAMESPACE);
        }

        public override void destroy(object sender)
        {
            base.destroy(sender);
            _zombieCount -= 1;

            cleanUp();
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

                default:
                    break;
            }
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
    }
}
