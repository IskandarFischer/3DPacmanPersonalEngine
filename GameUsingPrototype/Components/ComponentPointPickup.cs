using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Managers;
using PrototypeEngine.Components;
using PrototypeEngine.Objects;

namespace OpenGL_Game.Components
{
    class ComponentPointPickup : ComponentPickup
    {
        protected ComponentPointPickup()
        {

        }

        public ComponentPointPickup(string soundPrefab)
        {
            SoundPrefab = soundPrefab;
        }

        public override void OnTrigger(Entity otherEntity, ComponentCollider otherCollider)
        {
            base.OnTrigger(otherEntity, otherCollider);

            GameManager.Instance.PickupItem();
        }
    }
}
