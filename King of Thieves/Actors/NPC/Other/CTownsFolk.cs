using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using King_of_Thieves.Input;
using Gears.Cloud;

namespace King_of_Thieves.Actors.NPC.Other
{
    class CTownsFolk : CActor
    {
        private string _SPRITE_NAMESPACE = "";
        private const string _SHEET01 = "sprites/npc/friendly/townsfolk01";

        private string _WALK_UP = "walkUp";
        private string _WALK_DOWN = "walkDown";
        private string _WALK_LEFT = "walkLeft";
        private string _WALK_RIGHT = "walkRight";

        private string _IDLE_UP = "idleUp";
        private string _IDLE_DOWN = "idleDown";
        private string _IDLE_LEFT = "idleLeft";
        private string _IDLE_RIGHT = "idleRight";

        private bool _hasPettyItem = false;
        private bool _itemGiven = false;

        public CTownsFolk() :
            base()
        {
            _SPRITE_NAMESPACE = "npc:townsfolk01";

            _WALK_UP = _SPRITE_NAMESPACE + _WALK_UP;
            _WALK_DOWN = _SPRITE_NAMESPACE + _WALK_DOWN;
            _WALK_LEFT = _SPRITE_NAMESPACE + _WALK_LEFT;
            _WALK_RIGHT = _SPRITE_NAMESPACE + _WALK_RIGHT;

            _IDLE_UP = _SPRITE_NAMESPACE + _IDLE_UP;
            _IDLE_DOWN = _SPRITE_NAMESPACE + _IDLE_DOWN;
            _IDLE_LEFT = _SPRITE_NAMESPACE + _IDLE_LEFT;
            _IDLE_RIGHT = _SPRITE_NAMESPACE + _IDLE_RIGHT;

            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.rawTextures.Add(_SPRITE_NAMESPACE, CMasterControl.glblContent.Load<Texture2D>(_SHEET01));

                Graphics.CTextures.addTexture(_WALK_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:2", "2:2", 4));
                Graphics.CTextures.addTexture(_WALK_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:0", "0:3", 4));
                Graphics.CTextures.addTexture(_WALK_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:1", "2:1", 4));

                Graphics.CTextures.addTexture(_IDLE_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:2", "0:2", 0));
                Graphics.CTextures.addTexture(_IDLE_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "1:0", "1:0", 0));
                Graphics.CTextures.addTexture(_IDLE_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 30, 36, 1, "0:1", "0:1", 0));
            }

            _imageIndex.Add(_IDLE_DOWN, new Graphics.CSprite(_IDLE_DOWN));
            _imageIndex.Add(_IDLE_UP, new Graphics.CSprite(_IDLE_UP));
            _imageIndex.Add(_IDLE_LEFT, new Graphics.CSprite(_IDLE_RIGHT,true));
            _imageIndex.Add(_IDLE_RIGHT, new Graphics.CSprite(_IDLE_RIGHT));

            _imageIndex.Add(_WALK_DOWN, new Graphics.CSprite(_WALK_DOWN));
            _imageIndex.Add(_WALK_UP, new Graphics.CSprite(_WALK_UP));
            _imageIndex.Add(_WALK_LEFT, new Graphics.CSprite(_WALK_RIGHT, true));
            _imageIndex.Add(_WALK_RIGHT, new Graphics.CSprite(_WALK_RIGHT));

            _direction = DIRECTION.DOWN;
            _angle = 270;
            _state = ACTOR_STATES.IDLE;
            swapImage(_IDLE_DOWN);
            _lineOfSight = 20;
            _visionRange = 60;
            _hearingRadius = 30;

            _hitBox = new Collision.CHitBox(this, 10, 15, 16, 16);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if (!_itemGiven)
            {
                _itemGiven = true;
                if (_randNum.Next(100) >= 50)
                {
                    _hasPettyItem = true;
                    indicator.CIndicatorPickpocketPetty petty = new indicator.CIndicatorPickpocketPetty();
                    petty.init(_name + "pickpocketPettyIndicator", new Microsoft.Xna.Framework.Vector2(_position.X + 5, _position.Y - 20), "", this.componentAddress);
                    Map.CMapManager.addActorToComponent(petty, this.componentAddress);
                }
            }
            Vector2 playerPos = new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY);
            
            if (MathExt.MathExt.checkPointInCircle(playerPos, _position, _hearingRadius))
            {
                if (_checkIfPointInView(playerPos))
                    _state = ACTOR_STATES.TALK_READY;
                else
                    _state = ACTOR_STATES.IDLE;
            }
        }

        public override void keyRelease(object sender)
        {
            CInput input = Master.GetInputManager().GetCurrentInputHandler() as CInput;
            if (_state == ACTOR_STATES.TALK_READY && input.keysReleased.Contains(Microsoft.Xna.Framework.Input.Keys.C))
                startTimer0(2);
        }

        public override void timer0(object sender)
        {
            CMasterControl.buttonController.createTextBox("Bah! Out of my way, filth!!");
        }


    }
}
