﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Graphics;
using Microsoft.Xna.Framework;
using King_of_Thieves.Input;
using Gears.Cloud;

namespace King_of_Thieves.Actors.HUD.magic
{
    class CMagicMeter : CHUDElement
    {
        private int _capacity;
        private int _amount;
        private CRect _meter;

        public CMagicMeter() :
            base()
        {
            _imageIndex.Add(Graphics.CTextures.HUD_MAGIC_METER, new CSprite(Graphics.CTextures.HUD_MAGIC_METER));
            swapImage(Graphics.CTextures.HUD_MAGIC_METER);
            _capacity = 100;
            _amount = 100;
            _meter = new CRect(100, 2, 0, 0, new Microsoft.Xna.Framework.Color(72, 248, 24));
            _fixedPosition.X = 14;
            _fixedPosition.Y = 30;
        }

        public override void drawMe(bool useOverlay = false)
        {
            base.drawMe(useOverlay);
            _meter.draw((int)_position.X + 2, (int)_position.Y + 2);
        }

        public override void keyDown(object sender)
        {
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;

            if (input.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.PageDown))
            {
                if (_amount > 0)
                {
                    _amount--;
                    _meter.width = _amount;
                }
            }
            else if (input.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.PageUp))
            {
                if (_amount < _capacity)
                {
                    _amount++;
                    _meter.width = _amount;
                }
            }
        }

        public int amount
        {
            get
            {
                return _amount;
            }
        }

        public void subtractMagic(int MP)
        {
            if (_amount > 0)
            {
                _amount -= MP;
                if (_amount < 0)
                    _amount = 0;

                _meter.width = _amount;
            }
        }

        public void addMagic(int MP)
        {
            if (_amount < _capacity)
            {
                _amount += MP;
                if (_amount > _capacity)
                    _amount = _capacity;

                _meter.width = _amount;
            }
        }

        public bool isMagicLeft
        {
            get
            {
                return _amount > 0;
            }
        }

        public bool checkMagicAmount(int MP)
        {
            return _amount >= MP;
        }
    }
}
