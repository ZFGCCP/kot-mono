using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeeseFire : CBaseKeese
    {
        public CKeeseFire()
            : base(60)
        {
            _type = KEESETYPE.FIRE;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keeseFire:Idle"));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keeseFire:Fly"));
        }

        public override void drawMe(bool useOverlay = false)
        {
            base.drawMe(false);
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
        }
    }
}
