using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    public class CAnimation
    {
        private float _speed = 0;
        private float _speedHold = 0;

 

        public void animate(bool loop = false)
        {
        //    Rectangle source = _sheet.getSprite((int)_counter);
        //    _counter += _speed;

        //    if ((int)_counter == _sheet.count)
        //    {
        //        if (!loop)
        //            _speed = 0;
        //        else
        //            _counter = 0;
        //    }

            //CGraphics.spriteBatch.Draw(_sheet.sheet, 
        }

        public void reset()
        {
            _speed = _speedHold;
        }
    }
}
