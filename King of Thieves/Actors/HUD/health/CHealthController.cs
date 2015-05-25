using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.HUD.health
{
    class CHealthController
    {
        private int _totalNumberOfHearts = 0;
        private int _hp = 0;
        private int _totalHp = 0;
        private CHealth[] _hearts = null;
        private const int _HP_PER_HEART = 4;

        public CHealthController(int totalNumberOfHearts, int hp)
        {
            _totalNumberOfHearts = totalNumberOfHearts;
            _totalHp = _totalNumberOfHearts * _HP_PER_HEART;

            _hp = _isHpToHigh(hp) ? _totalHp : hp;

            _hearts = new CHealth[_totalNumberOfHearts];

            for (int i = _totalNumberOfHearts - 1; i >= 0; i--)
                _prepareHeart(i, true);
        }

        private int _getPortionFilled(bool isHeartDead, int portionFilledBuffer)
        {
            int portionFilled = 0;
            if (isHeartDead)
                portionFilled = 0;
            else if (portionFilledBuffer >= 3)
                portionFilled = 4;
            else
            {
                if (portionFilledBuffer > 0)
                    portionFilled = 4;
                else
                    portionFilled = 4 + portionFilledBuffer;
            }
            return portionFilled;
        }

        public void update(GameTime gametime)
        {
            if (CMasterControl.glblInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.Up))
                modifyHp(1);

            if (CMasterControl.glblInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.Down))
                modifyHp(-1);

            foreach (CHealth health in _hearts)
                health.update(gametime);
        }

        public void drawMe(SpriteBatch spriteBatch)
        {
            foreach (CHealth health in _hearts)
                health.drawMe(false);
        }

        public void modifyHp(int hp)
        {
            int previousHp = _hp;
            int previousHpBuffer = previousHp / 4;

            _hp += hp;
            _hp = _isHpToHigh(_hp) ? _totalHp : _hp;
            _hp = _isHpTooLow(_hp) ? 0 : _hp;

            int hpBuffer = _hp / 4;

            hpBuffer = hpBuffer >= 20 ? 19 : hpBuffer;

            if (hpBuffer > previousHpBuffer)
                hpBuffer--;

            _prepareHeart(hpBuffer, false);

        }

        private bool _isHpToHigh(int hp)
        {
            return hp > _totalHp;
        }

        private bool _isHpTooLow(int hp)
        {
            return hp < 0;
        }

        private void _prepareHeart(int currentHeartIndex, bool createNewHeart)
        {
            int currentHeartNoMaxHP = (currentHeartIndex + 1) * _HP_PER_HEART;
            bool isHeartDead = (_hp < currentHeartNoMaxHP && _hp >= currentHeartNoMaxHP - _HP_PER_HEART) || _hp >= currentHeartNoMaxHP ? false : true;
            int portionFilledBuffer = _hp - currentHeartNoMaxHP;
            int portionFilled = 0;

            portionFilled = _getPortionFilled(isHeartDead, portionFilledBuffer);

            bool isActive = isHeartDead ? false : (portionFilledBuffer == 3 && currentHeartIndex * _HP_PER_HEART == _hp - 1 ? true : false);

            if (createNewHeart)
                _hearts[currentHeartIndex] = new CHealth(currentHeartIndex + 1, portionFilled, isActive);
            else
            {
                _hearts[currentHeartIndex].portionFilled = portionFilled;

                if (currentHeartIndex + 1 < _hearts.Length)
                    _hearts[currentHeartIndex + 1].portionFilled = 0;

                for (int i = 0; i < currentHeartIndex; i++)
                    _hearts[i].portionFilled = 4;
            }
        }
    }
}
