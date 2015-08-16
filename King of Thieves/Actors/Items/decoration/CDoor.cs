using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Actors.Collision;

namespace King_of_Thieves.Actors.Items.decoration
{
    class CDoor : CActor
    {
        private bool _locked = false;
        private const string _SPRITE_NAMESPACE = "door";

        private const string _DOOR_DOWN = _SPRITE_NAMESPACE + ":down";
        private const string _DOOR_LEFT = _SPRITE_NAMESPACE + ":left";
        private const string _DOOR_RIGHT = _SPRITE_NAMESPACE + ":right";
        private const string _DOOR_UP = _SPRITE_NAMESPACE + ":up";

        private static int _doorCount = 0;

        public CDoor() :
            base()
        {
            if (!Graphics.CTextures.rawTextures.ContainsKey(_SPRITE_NAMESPACE))
            {
                Graphics.CTextures.addRawTexture(_SPRITE_NAMESPACE, "tilesets/indoors/jim_morrison");

                Graphics.CTextures.addTexture(_DOOR_DOWN, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 15, 0, "0:0", "0:0", 0));
                Graphics.CTextures.addTexture(_DOOR_UP, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 24, 15, 0, "1:0", "1:0", 0));
                Graphics.CTextures.addTexture(_DOOR_LEFT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 15, 24, 0, "0:1", "0:1", 0));
                Graphics.CTextures.addTexture(_DOOR_RIGHT, new Graphics.CTextureAtlas(_SPRITE_NAMESPACE, 15, 24, 0, "1:1", "1:1", 0));
            }

            _imageIndex.Add(_DOOR_DOWN, new Graphics.CSprite(_DOOR_DOWN));
            _imageIndex.Add(_DOOR_UP, new Graphics.CSprite(_DOOR_UP));
            _imageIndex.Add(_DOOR_LEFT, new Graphics.CSprite(_DOOR_LEFT));
            _imageIndex.Add(_DOOR_RIGHT, new Graphics.CSprite(_DOOR_RIGHT));

            _hitBox = new CHitBox(this, 5, 0, 16, 16);
            _drawDepth = 12;
        }

        public override void init(string name, Microsoft.Xna.Framework.Vector2 position, string dataType, int compAddress, params string[] additional)
        {
            base.init(name, position, dataType, compAddress, additional);

            _direction = (DIRECTION)Convert.ToInt32(additional[1]);

            switch (_direction)
            {
                case DIRECTION.DOWN:
                    swapImage(_DOOR_DOWN);
                    break;

                case DIRECTION.UP:
                    swapImage(_DOOR_UP);
                    break;

                case DIRECTION.LEFT:
                    swapImage(_DOOR_LEFT);
                    break;

                case DIRECTION.RIGHT:
                    swapImage(_DOOR_RIGHT);
                    break;
            }

            int lockedParam = Convert.ToInt32(additional[0]);
            _locked = lockedParam != 0;
        }

        public bool isLocked
        {
            get
            {
                return _locked;
            }
        }
    }
}
