using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using OpenTK;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Text;
using PrototypeEngine.Systems;

namespace PrototypeEngine.Components
{
    public class ComponentAudio : ComponentBase
    {
        int mySource;
        int sound;

        public string AudioPath;
        public bool looping;

        bool playing;

        public ComponentAudio(string audioName, bool looping, Vector3 emitterPosition)
        {
            AudioPath = audioName;
            this.looping = looping;

            sound = ResourceManager.LoadSound(audioName);

            mySource = AL.GenSource(); // gen a Source Handle
            AL.Source(mySource, ALSourcei.Buffer, sound); // attach the buffer to a source
            AL.Source(mySource, ALSourceb.Looping, looping);
            AL.Source(mySource, ALSource3f.Position, ref emitterPosition);
        }

        public void UpdatePosition(Vector3 newPosition, Vector3 listenerPosition, Vector3 listenerDirection, Vector3 listenerUp)
        {
            if (!playing)
            {
                AL.SourcePlay(mySource);
                playing = true;
            }

            AL.Source(mySource, ALSource3f.Position, ref newPosition);

            AL.Listener(ALListener3f.Position, ref listenerPosition);
            AL.Listener(ALListenerfv.Orientation, ref listenerDirection, ref listenerUp);
        }

        public override void OnPrefabCreate()
        {
            base.OnPrefabCreate();

            if (AudioPath != null)
            {
                sound = ResourceManager.LoadSound(AudioPath);

                var currentPos = Transform.Position;

                mySource = AL.GenSource(); // gen a Source Handle
                AL.Source(mySource, ALSourcei.Buffer, sound); // attach the buffer to a source
                AL.Source(mySource, ALSourceb.Looping, looping);
                AL.Source(mySource, ALSource3f.Position, ref currentPos);

                AL.SourcePlay(mySource);
            }
        }

        public override void DestroyComponent()
        {
            base.DestroyComponent();

            AL.SourceStop(mySource);
            AL.DeleteSource(mySource);
        }

        public override void AddToSystems()
        {
            base.AddToSystems();
            SystemManager.Instance.GetSystem<SystemAudio>().AddToList(Entity);
        }

        public ComponentAudio()
        {

        }
    }
}
