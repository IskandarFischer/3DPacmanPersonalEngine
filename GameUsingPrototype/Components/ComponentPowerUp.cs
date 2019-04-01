using OpenGL_Game.Managers;
using PrototypeEngine.Components;
using PrototypeEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentPowerUp : ComponentPickup
    {
        public ComponentPowerUp(string soundPrefab)
        {
            SoundPrefab = soundPrefab;
        }

        protected ComponentPowerUp()
        {

        }

        public override void OnTrigger(Entity otherEntity, ComponentCollider otherCollider)
        {
            base.OnTrigger(otherEntity, otherCollider);

            GameManager.Instance.PickupPowerUp();
        }
    }
}
