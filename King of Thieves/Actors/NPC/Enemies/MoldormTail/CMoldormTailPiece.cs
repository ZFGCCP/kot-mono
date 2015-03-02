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
        protected readonly static string _HEAD_UP = "headUp";
        protected readonly static string _HEAD_DOWN = "headDown";
        protected readonly static string _HEAD_LEFT = "headLeft";
        protected readonly static string _HEAD_RIGHT = "headRight";

        protected readonly static string _HEAD_DLEFT = "headDLeft";
        protected readonly static string _HEAD_DRIGHT = "headDRight";
        protected readonly static string _HEAD_ULEFT = "headULeft";
        protected readonly static string _HEAD_URIGHT = "headURight";

        protected readonly static string _TAIL_UP = "tailUp";
        protected readonly static string _TAIL_DOWN = "tailDown";
        protected readonly static string _TAIL_LEFT = "tailLeft";
        protected readonly static string _TAIL_RIGHT = "tailRight";

        protected readonly static string _TAIL_DLEFT = "tailDLeft";
        protected readonly static string _TAIL_DRIGHT = "tailDRight";
        protected readonly static string _TAIL_ULEFT = "tailULeft";
        protected readonly static string _TAIL_URIGHT = "tailURight";

        protected readonly static string _BODY = "body";

        protected readonly static string _NPC_MOLDORM = "npc:MoldormTail";
        protected static int _moldormAndTailCount = 0;
        protected readonly bool _isMoldorm;
        protected string _prev = "";
        protected string _next = "";
        protected Vector2 _follow = Vector2.Zero;

        public CMoldormTailPiece(bool isMoldorm, params dropRate[] drops)
            : base(drops)
        {
            //cache the textures needed
            if (!Graphics.CTextures.rawTextures.ContainsKey(_NPC_MOLDORM))
                Graphics.CTextures.rawTextures.Add(_NPC_MOLDORM, CMasterControl.glblContent.Load<Texture2D>(@"sprites/npc/moldormandtail"));

            _direction = DIRECTION.DOWN;
            _angle = 270;
            _visionRange = 90;
            _lineOfSight = 300;
            _isMoldorm = isMoldorm;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);
            _prev = additional[0];
            _next = additional[1];
        }

        protected override void cleanUp()
        {
            Graphics.CTextures.rawTextures.Remove(_NPC_MOLDORM);
        }
    }
}
