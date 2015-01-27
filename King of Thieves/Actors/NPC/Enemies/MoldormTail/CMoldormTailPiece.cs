using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.NPC.Enemies.MoldormTail
{
    class CMoldormTailPiece : CBaseEnemy
    {
        protected readonly static string _NPC_MOLDORM = "npc:MoldormTail";
        protected static int _moldormAndTailCount = 0;
        private static int[] _directionChangeTimes = new int[] { 10, 10, 10, 20, 20, 30, 60 };
        protected readonly bool _isMoldorm;
        protected string _prev = "";
        protected string _next = "";
        protected Vector2 _moveTowardsPoint = Vector2.Zero;

        public CMoldormTailPiece(bool isMoldorm, params dropRate[] drops)
            : base(drops)
        {
            //cache the textures needed
            if (!Graphics.CTextures.rawTextures.ContainsKey(_NPC_MOLDORM))
                Graphics.CTextures.rawTextures.Add(_NPC_MOLDORM, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/moldormandtail"));

            _direction = DIRECTION.DOWN;
            _angle = 270;
            _visionRange = 90;
            _lineOfSight = 120;
            _isMoldorm = isMoldorm;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            //_prev = additional[0];
            //_next = additional[1];
        }

        protected override void cleanUp()
        {
            Graphics.CTextures.rawTextures.Remove(_NPC_MOLDORM);
        }

        protected int _getChangeTime(int index)
        {
            return _directionChangeTimes[index];
        }
    }
}
