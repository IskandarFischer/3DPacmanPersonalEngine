using PrototypeEngine.Components;
using PrototypeEngine.Objects;
using OpenTK.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrototypeEngine.Utilities;

namespace PrototypeEngine.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        AudioContext audioContext;

        public static Entity AudioListener
        {
            get { return audio; }
        }

        private static Entity audio;

        public AudioManager()
        {
            audioContext = new AudioContext();
        }

        public void StopAudioManager()
        {
            audioContext.Dispose();
        }

        public static void SetAudioListener(ComponentAudioListener listener)
        {
            audio = listener.Entity;
        }

        public static void StopAudioListener()
        {
            audio = null;
        }
    }
}
