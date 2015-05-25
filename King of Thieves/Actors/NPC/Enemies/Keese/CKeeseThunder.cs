using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeeseThunder : CBaseKeese
    {
        public CKeeseThunder()
            : base(60)
        {
            _type = KEESETYPE.THUNDER;
            _properties.Add(ENEMY_PROPERTIES.ELECTRIC);
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keeseThunder:Idle"));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keeseThunder:Fly"));
        }

        public override void drawMe(bool useOverlay = false, SpriteBatch spriteBatch = null)
        {
            base.drawMe(false);
        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);

            if (collider is Player.CPlayer)
            {
                if (!INVINCIBLE_STATES.Contains(collider.state))
                {
                    collider.shock();
                    collider.dealDamange(2, collider);
                }
            }
        }
    }
}
