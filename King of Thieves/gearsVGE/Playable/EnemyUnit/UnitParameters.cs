using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Microsoft.Xna.Framework.Storage;

namespace Gears.Playable
{
    internal struct UnitParameters
    {
        internal UnitParameters(Vector2 originIN, Color colorIN, float rotationIN, string textureFileNameIN)
        {
            //note not parsed
            origin = originIN;
            color = colorIN;
            rotation = rotationIN;
            textureFileName = textureFileNameIN;
        }

        internal void Pack(Vector2 originIN, Color colorIN, float rotationIN, string textureFileNameIN)
        {
            //note not parsed
            origin = originIN;
            color = colorIN;
            rotation = rotationIN;
            textureFileName = textureFileNameIN;
        }

        private Vector2 origin;
        internal Vector2 Origin
        {
            get { return origin; }
            //set { origin = value; } //note not parsed
        }
        private Color color;
        internal Color Color
        {
            get { return color; }
            //set { color = value; } //note not parsed
        }
        private float rotation;
        internal float Rotation
        {
            get { return rotation; }
            //set { rotation = value; } //note not parsed
        }
        private string textureFileName;
        internal string TextureFileName
        {
            get { return textureFileName; }
            //set { textureFileName = value; } //note not parsed
        }
    }
}
