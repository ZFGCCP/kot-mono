using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Gears.Cloud.Media
{
    public class Sound
    {
        private object _data; //i'd rather this be generic, but it was too complicated
        private Type _type;
        public bool loop = false;

        public Sound(SoundEffect data)
        {
            _data = data;
            _type = typeof(SoundEffect);
        }

        public Sound(Song data, bool loop = false)
        {
            _data = data;
            _type = typeof(Song);
            this.loop = loop;
        }
        public Type DataType
        {
            get
            {
                return _type;
            }
        }
        public object soundData
        {
            get
            {
                return _data;
            }
        }
    }
}
