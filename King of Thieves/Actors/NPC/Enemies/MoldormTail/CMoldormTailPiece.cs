using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.NPC.Enemies.MoldormTail
{
    class CMoldormTailPiece : CBaseEnemy
    {
        protected readonly static string _NPC_MOLDORM = "npc:MoldormTail";
        private static int _moldormAndTailCount = 0;
        private static int[] _directionChangeTimes = new int[] { 20, 30, 60 };
        

        public CMoldormTailPiece(params dropRate[] drops)
            : base(drops)
        {
            //cache the textures needed
            if (!Graphics.CTextures.rawTextures.ContainsKey(_NPC_MOLDORM))
                Graphics.CTextures.rawTextures.Add(_NPC_MOLDORM, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/moldormandtail"));

            _moldormAndTailCount++;
            _direction = DIRECTION.DOWN;
            _changeDirection();
        }

        protected override void cleanUp()
        {
            Graphics.CTextures.rawTextures.Remove(_NPC_MOLDORM);
        }

        private void _changeDirection()
        {
            int nextChangeTime = _randNum.Next(0, 2);
            int direction = _randNum.Next(0, 7);
            _direction = (DIRECTION)direction;
            startTimer0(_directionChangeTimes[nextChangeTime]);

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    break;

                case DIRECTION.UP:
                    break;

                case DIRECTION.LEFT:
                    break;

                case DIRECTION.RIGHT:
                    break;

                case DIRECTION.DLEFT:
                    break;

                case DIRECTION.DRIGHT:
                    break;

                case DIRECTION.ULEFT:
                    break;

                case DIRECTION.URIGHT:
                    break;
            }
        }

        public override void timer0(object sender)
        {
            base.timer0(sender);
            _changeDirection();
        }
    }
}
