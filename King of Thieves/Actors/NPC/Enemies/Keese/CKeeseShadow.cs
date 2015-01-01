using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeeseShadow : CBaseKeese
    {
        public CKeeseShadow()
            : base(60)
        {
            _type = KEESETYPE.SHADOW;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keeseShadow:Idle"));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keeseShadow:Fly"));
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);

            if (collider is Player.CPlayer)
            {
                if (!INVINCIBLE_STATES.Contains(collider.state))
                    collider.dealDamange(4, collider);
            }
        }
    }
}
