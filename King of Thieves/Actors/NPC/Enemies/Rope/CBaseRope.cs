using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Rope
{
    enum ROPEETYPE
    {
        NORMAL = 0
    
    }

    class CBaseRope : CBaseEnemy
    {
        public CBaseRope(int foh, params dropRate[] drops)
            : base(drops)
        {
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
        }

        public override void create(object sender)
        {
            base.create(sender);

        }

        public override void destroy(object sender)
        {
         
            base.destroy(sender);
        }

        protected override void cleanUp()
        { }

        public override void timer0(object sender)
        {
            base.timer0(sender);
        }

        public override void timer1(object sender)
        {
            base.timer1(sender);
            
        }

        public override void timer2(object sender)
        {
            base.timer2(sender);
        }

        public override void timer3(object sender)
        {
            base.timer3(sender);
        }

        public override void drawMe(bool useOverlay = false)
        {
            
                base.drawMe(useOverlay);
        }

      











    }

     
}
