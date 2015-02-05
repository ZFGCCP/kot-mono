using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Map;

namespace King_of_Thieves.Actors.NPC.Enemies.MoldormTail
{
    class CMoldormTailBody : CMoldormTailPiece
    {
        

        public CMoldormTailBody() :
            base(true)
        {
            _imageIndex.Add(_BODY, new Graphics.CSprite(_BODY));
            swapImage(_BODY);
            startTimer0(1);
        }


        public override void timer0(object sender)
        {
            base.timer0(sender);
            _follow = (Vector2)Map.CMapManager.propertyGetterFromComponent(this.componentAddress, _prev, EActorProperties.OLD_POSITION);
            startTimer0(15);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            moveToPoint(_follow.X, _follow.Y, 1);
            base.update(gameTime);
        }


    }
}
