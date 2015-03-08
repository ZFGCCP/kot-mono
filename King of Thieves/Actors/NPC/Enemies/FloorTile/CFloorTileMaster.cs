using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.FloorTile
{
    class CFloorTileMaster : CBaseEnemy
    {
        private int _numberOfChildren = 0;
        public const string CHILD_PREFIX = "CHILD";

        public CFloorTileMaster()
            : base()
        {
            _state = ACTOR_STATES.IDLE;
            _hearingRadius = 100;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _numberOfChildren = Convert.ToInt32(additional[0]);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if (_state == ACTOR_STATES.IDLE)
            {
                Vector2 playerPos = Vector2.Zero;
                
                playerPos.X = Player.CPlayer.glblX;
                playerPos.Y = Player.CPlayer.glblY;
                if (isPointInHearingRange(playerPos))
                {
                    _state = ACTOR_STATES.ATTACK;
                    for (int i = 0; i < _numberOfChildren; i++)
                        _triggerUserEvent(0, _name + CHILD_PREFIX + i);
                }
            }
        }
    }
}
