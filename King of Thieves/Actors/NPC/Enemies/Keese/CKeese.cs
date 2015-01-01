using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeese : CBaseKeese
    {
        public CKeese()
            : base(60, new dropRate(new Items.Drops.CHeartDrop(), 100))
        {
            _type = KEESETYPE.NORMAL;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keese:Idle"));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keese:Fly"));
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);

            if (collider is Player.CPlayer)
                if (!INVINCIBLE_STATES.Contains(collider.state))
                    collider.dealDamange(1, collider);
        }
    }
}
