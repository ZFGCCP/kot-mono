using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Other.DemoGuys
{
    class CKeyGuyHead : CActor
    {
        private const string _SPRITE_NAMESPACE = "demoFolk:";
        private const string _FACE_DOWN = _SPRITE_NAMESPACE + "faceDown";
        private const string _FACE_LEFT = _SPRITE_NAMESPACE + "faceLeft";
        private const string _FACE_RIGHT = _SPRITE_NAMESPACE + "faceRight";
        private const string _FACE_UP = _SPRITE_NAMESPACE + "faceUp";

        public CKeyGuyHead() :
            base()
        {
            _imageIndex.Add(_FACE_DOWN, new Graphics.CSprite(Graphics.CTextures.DEMO_FOLK_KEY_GUY_HEAD_DOWN));
            _imageIndex.Add(_FACE_LEFT, new Graphics.CSprite(Graphics.CTextures.DEMO_FOLK_KEY_GUY_HEAD_LEFT));
            _imageIndex.Add(_FACE_RIGHT, new Graphics.CSprite(Graphics.CTextures.DEMO_FOLK_KEY_GUY_HEAD_LEFT, true));
            _imageIndex.Add(_FACE_UP, new Graphics.CSprite(Graphics.CTextures.DEMO_FOLK_KEY_GUY_HEAD_UP));

            _direction = DIRECTION.DOWN;
            _angle = 270;
            _drawDepth = 10;
            swapImage(_FACE_LEFT);
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            swapImage(this.component.root.currentImageIndex);
        }
    }
}
