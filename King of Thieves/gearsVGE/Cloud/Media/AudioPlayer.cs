using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Gears.Cloud.Media
{
    public static class AudioPlayer
    {
        
        private static ThreadStart _threadStarter = new ThreadStart(_playAudio);
        private static BlockingCollection<Sound> _audioData = new BlockingCollection<Sound>();
        private static Thread _audioThread = new Thread(_threadStarter);

        public static void queueAudio(Sound data)
        {
            _audioData.Add(data);
        }

        public static void start()
        {
            _audioThread.Start();
        }

        public static void stop()
        {
            _audioThread.Abort();
        }

        private static void _playAudio()
        {
            while (true)
            {
                Sound temp = _audioData.Take();
                Type theType = temp.DataType;

                if (theType == typeof(SoundEffect))
                {
                    ((SoundEffect)temp.soundData).Play();
                }
                else if (theType == typeof(Song))
                {
                    MediaPlayer.Play((Song)temp.soundData);
                }
                else
                {
                    throw new FormatException("The audio data passed was not recognized as a Song or SoundEffect.");
                }
            }
        }
    }
}
