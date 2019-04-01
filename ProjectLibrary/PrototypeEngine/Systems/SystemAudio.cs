using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace PrototypeEngine.Systems
{
    public class SystemAudio : SystemBase
    {
        public override void OnAction(Entity entity)
        {
            ComponentAudio audio = entity.GetComponent<ComponentAudio>();
            ComponentTransform position = entity.Transform;

            if (AudioManager.AudioListener != null)
                audio.UpdatePosition(position.Position, AudioManager.AudioListener.Transform.Position, AudioManager.AudioListener.Transform.Forward, AudioManager.AudioListener.Transform.Up);
            else
                audio.UpdatePosition(position.Position, new Vector3(0, 0, 3), new Vector3(0, 0, -1), Vector3.UnitY);
        }
    }
}
