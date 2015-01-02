using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace King_of_Thieves.Sound
{
    public class CSound
    {
        //:D
        private SoundEffect _sfx;
        private SoundEffectInstance _sfxInstance;
        private Song _song;
        private bool _loop;
        private bool _track;
        private int _repeat;

        public CSound(SoundEffect fx)
        {
            _sfx = fx;
            _song = null;
        }

        public CSound(SoundEffect fx, bool Track)
        {
            _sfxInstance = fx.CreateInstance();
            _sfx = null;
            _song = null;
            _track = Track;
        }

        public CSound(Song song, bool loop, int repeat)
        {
            _song = song;
            _sfx = null;
            _loop = loop;
            _repeat = repeat;
        }

        public SoundEffect sfx
        {
            get
            {
                return _sfx;
            }
        }

        public SoundEffectInstance sfxInstance
        {
            get
            {
                return _sfxInstance;
            }
        }

        public Song song
        {
            get
            {
                return _song;
            }
        }

        public bool loop
        {
            get
            {
                return _loop;
            }
        }

        public int repeat
        {
            get
            {
                return _repeat;
            }
            set
            {
                _repeat = value;
            }
        }

        public bool track
        {
            get
            {
                return _track;
            }
        }
    }
}
