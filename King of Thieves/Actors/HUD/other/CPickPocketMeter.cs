using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Graphics;

namespace King_of_Thieves.Actors.HUD.other
{
    class CPickPocketMeter : magic.CMagicMeter
    {
        public CPickPocketMeter() :
            base()
        {
            _imageIndex.Add(Graphics.CTextures.HUD_MAGIC_METER, new CSprite(Graphics.CTextures.HUD_MAGIC_METER));
            swapImage(Graphics.CTextures.HUD_MAGIC_METER);
            _capacity = 100;
            _amount = 100;
            _meter = new CRect(100, 2, 0, 0, new Microsoft.Xna.Framework.Color(222, 40, 41));
            _fixedPosition.X = 160;
            _fixedPosition.Y = 240;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if (_amount == 0)
                _state = ACTOR_STATES.DECREMENT;
            else if (_amount == _capacity)
                _state = ACTOR_STATES.INCREMENT;

            if (_state == ACTOR_STATES.DECREMENT)
                subtractMagic(2);
            else if (_state == ACTOR_STATES.INCREMENT)
                addMagic(2);
        }
    }
}
