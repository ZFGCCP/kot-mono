using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace King_of_Thieves.Sound
{
    class CAudioPlayer
    {
        private Thread _audioThread;
        private CSound _song;
        private BlockingCollection<CSound> _effects;
        public Dictionary<string, CSound> soundBank = new Dictionary<string, CSound>();

        public CAudioPlayer()
        {

            _init();
            _effects = new BlockingCollection<CSound>();
            System.Threading.ThreadStart threadStarter = _checkForThingsToPlay;
            _audioThread = new Thread(threadStarter);
            _audioThread.Start();
        }

        ~CAudioPlayer()
        {
            _audioThread.Abort();
            _effects.Dispose();
            _effects = null;
            _song = null;
        }

        private void _init()
        {
            //load sound files here
            //USE NAMESPACE FORMAT
            soundBank.Add("Player:Attack1", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/linkAttack1")));
            soundBank.Add("Player:Attack2", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/linkAttack2")));
            soundBank.Add("Player:Attack3", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/linkAttack3")));
            soundBank.Add("Player:Attack4", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/linkAttack4")));
            soundBank.Add("Player:SwordSlash", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/linkSwordSlash")));
            soundBank.Add("Player:Electrocute", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/link/shocked")));
            soundBank.Add("Player:Hurt1", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/link/hurt1")));
            soundBank.Add("Items:Decor:ItemSmash", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/MC_Shatter")));

            soundBank.Add("Items:explosionSmall", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/items/bomb_explode")));
            soundBank.Add("Items:boomerang", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/items/boomerang"),true));
            
            //text
            soundBank.Add("Text:textBoxContinue", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/text/TextBoxContinue")));
            soundBank.Add("Text:textBoxClose", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/text/TextBoxDone")));

            //hud
            soundBank.Add("HUD:health:heartGet", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/health/lttp_heart")));
            soundBank.Add("HUD:health:healthBeep", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/health/Low_Health_Beep")));

            //npcs
            soundBank.Add("Npc:wizzrobe:vanish", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/npc/wizzrobevanish")));

            //background sfx
            soundBank.Add("Background:Nature:Rooster", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/rooster")));
            soundBank.Add("Background:Nature:Wolf", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/wolf")));
            soundBank.Add("Background:Shock", new CSound(CMasterControl.glblContent.Load<SoundEffect>("sounds/environment/shock")));
        }

        public void stop()
        {
            _audioThread.Abort();
        }

        public void pause()
        {
            MediaPlayer.Pause();
        }

        public void resume()
        {
            MediaPlayer.Resume();
        }

        public CSound song
        {
            get
            {
                return _song;
            }
            set
            {
                _song = value;
            }
        }

        public void addSfx(CSound sfx)
        {
            _effects.Add(sfx);
        }

        public void stopSfx(CSound sfx)
        {
            if (sfx.sfxInstance != null)
                sfx.sfxInstance.Stop(true);
        }

        public void addSfx(CSound sfx, int playCount)
        {
            sfx.repeat = playCount;
            _effects.Add(sfx);
        }

        //this function name is an abomination to my programming abilities. Luckily only the thread is going to use this.
        private void _checkForThingsToPlay()
        {
            while (true)
            {
                _play(_effects.Take());
            }
        }

        private void _play(CSound file)
        {
            if (file != null)
            {
                if (file.sfx != null)
                {
                    file.sfx.Play();
                    file.sfx.Dispose();
                }
                else if (file.sfxInstance != null)
                {
                    file.sfxInstance.Play();
                }
                else if (file.song != null)
                {
                    _song = file;
                    MediaPlayer.IsRepeating = file.loop;
                    MediaPlayer.Play(_song.song);
                }
                
                file = null;
            }
            else
            {
                file.sfx.Dispose();
                file = null;
                throw new FormatException("The CSound passed did not contain any valid audio information.");
            }
        }
    }
}