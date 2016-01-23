using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.NPC.Other.TownsFolk
{
    class CBernice : CBaseNpc
    {
        private const string _SPRITE_NAMESPACE = "npc:townsFolk:";
        private const string _BERNICE_IDLE = _SPRITE_NAMESPACE + "berniceIdle";

        private string[] _DIALOG1 = {"Oh, thank the gods the circus trainer is here!! You really need to get these snakes round up!","They're going to hurt my cats!!","MY CATS!! DO YOU HEAR ME?! MY CATS WILL GET HURT."}; 

        public CBernice() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/friendly/friendlyNPCs");

            Graphics.CTextures.addTexture(_BERNICE_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:2", "8:2", 15));

            _imageIndex.Add(_BERNICE_IDLE, new Graphics.CSprite(_BERNICE_IDLE));
            _state = ACTOR_STATES.IDLE;
            swapImage(_BERNICE_IDLE);
            _hearingRadius = 30;
            _MAP_ICON = _BERNICE_IDLE;
            _hitBox = new Collision.CHitBox(this, 10, 10, 16, 16);
        }

        public override void keyDown(object sender)
        {
            CInput input = (CInput)sender;
            if (input.keysPressed.Contains(input.getKey(CInput.KEY_ACTION)))
            {
                if (MathExt.MathExt.checkPointInCircle(new Vector2(Player.CPlayer.glblX,Player.CPlayer.glblY),_position,_hearingRadius))
                {
                    _currentDialog = _DIALOG1;
                    _triggerTextEvent();
                }
            }
        }
    }
}
