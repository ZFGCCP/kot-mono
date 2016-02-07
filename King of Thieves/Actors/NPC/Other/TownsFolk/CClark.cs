using System.Linq;
using Microsoft.Xna.Framework;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.NPC.Other.TownsFolk
{
    class CClark : CBaseNpc
    {
        private const string _SPRITE_NAMESPACE = "npc:townsFolk:";
        private const string _CLARK_IDLE = _SPRITE_NAMESPACE + "idle";
        private const string _CLARK_TALK = _SPRITE_NAMESPACE + "talk";

        private string[] _dialog1 = { "Snakes?! In MY slum?! It's more likely than you think!", "It's because of the Capitalistic treatment of the circus, you know.", "If our Bourgeois society would treat them right, they would be more careful with their snakes!" };

        public CClark() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "sprites/npc/friendly/friendlyNPCs");  

            Graphics.CTextures.addTexture(_CLARK_IDLE, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "0:1", "0:1"));
            Graphics.CTextures.addTexture(_CLARK_TALK, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 32, 32, 1, "1:1", "1:1"));

            _imageIndex.Add(_CLARK_IDLE, new Graphics.CSprite(_CLARK_IDLE));
            _imageIndex.Add(_CLARK_TALK, new Graphics.CSprite(_CLARK_TALK));
            _state = ACTOR_STATES.IDLE;
            swapImage(_CLARK_IDLE);
            _hearingRadius = 30;
            _MAP_ICON = _CLARK_IDLE;
        }

        public override void keyDown(object sender)
        {
            CInput input = (CInput)sender;
            if (input.keysPressed.Contains(input.getKey(CInput.KEY_ACTION)))
            {
                if (MathExt.MathExt.checkPointInCircle(new Vector2(Player.CPlayer.glblX, Player.CPlayer.glblY), _position, _hearingRadius))
                {
                    _currentDialog = _dialog1;
                    _triggerTextEvent();
                    swapImage(_CLARK_TALK);
                }
            }
        }

        protected override void dialogEnd(object sender)
        {
            base.dialogEnd(sender);
            swapImage(_CLARK_IDLE);
        }
    }
}
