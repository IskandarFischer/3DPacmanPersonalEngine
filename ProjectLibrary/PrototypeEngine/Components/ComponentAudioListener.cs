using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.Components
{
    public class ComponentAudioListener : ComponentBase
    {
        public void ChangeAudioListener()
        {
            AudioManager.SetAudioListener(this);
        }

        public override void DestroyComponent()
        {
            base.DestroyComponent();

            AudioManager.StopAudioListener();
        }

        public ComponentAudioListener()
        {
            ChangeAudioListener();
        }

        public override void OnPrefabCreate()
        {
            base.OnPrefabCreate();
            ChangeAudioListener();
        }
    }
}
