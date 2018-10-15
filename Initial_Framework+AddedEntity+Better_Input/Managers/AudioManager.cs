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

        public static Entity AudioListener;

        public AudioManager()
        {
            audioContext = new AudioContext();
        }

        public void StopAudioManager()
        {
            audioContext.Dispose();
        }
    }
}
