using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.shields
{
    class CWoodShield : CBaseShield
    {
        public CWoodShield() :
            base()
        {
            _imageIndex.Add(Graphics.CTextures.WOOD_SHIELD_DOWN, new Graphics.CSprite(Graphics.CTextures.WOOD_SHIELD_DOWN));
            _imageIndex.Add(Graphics.CTextures.WOOD_SHIELD_ENGAGE_DOWN, new Graphics.CSprite(Graphics.CTextures.WOOD_SHIELD_ENGAGE_DOWN));
        }

        public override void collide(object sender, CActor collider)
        {
            if (collider is Projectiles.CFireBall)
            {
                _killMe = true;
                Graphics.CEffects.createEffect(Graphics.CTextures.EFFECT_FIRE_BALL_SMALL, _position, 9);
                CMasterControl.buttonController.createTextBox("Your shield was burned!");
            }
        }
    }
}
