using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using OpenTK;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentAudio : IComponent
    {
        int mySource;

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AUDIO; }
        }

        int sound;

        public ComponentAudio(string audioName, bool looping, Vector3 emitterPosition)
        {
            sound = ResourceManager.LoadSound(audioName);

            mySource = AL.GenSource(); // gen a Source Handle
            AL.Source(mySource, ALSourcei.Buffer, sound); // attach the buffer to a source
            AL.Source(mySource, ALSourceb.Looping, looping);
            AL.Source(mySource, ALSource3f.Position, ref emitterPosition);
            AL.SourcePlay(mySource);
        }

        public void UpdatePosition(Vector3 newPosition, Vector3 listenerPosition, Vector3 listenerDirection, Vector3 listenerUp)
        {
            AL.Source(mySource, ALSource3f.Position, ref newPosition);

            AL.Listener(ALListener3f.Position, ref listenerPosition);
            AL.Listener(ALListenerfv.Orientation, ref listenerDirection, ref listenerUp);
        }
    }
}
