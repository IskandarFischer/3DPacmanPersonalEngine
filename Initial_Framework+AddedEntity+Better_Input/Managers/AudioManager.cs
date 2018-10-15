using OpenGL_Game.Objects;
using OpenTK.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Managers
{
    class AudioManager
    {
        AudioContext audioContext;

        Entity audioListener;

        public static Entity GetAudioListener
        {
            get { return GetAudioListener; }
        }

        public AudioManager()
        {
            audioContext = new AudioContext();
        }

        public void ChangeAudioListener(Entity newListener)
        {
            audioListener = newListener;
        }

        public void StopAudioManager()
        {
            audioContext.Dispose();
        }
    }
}
