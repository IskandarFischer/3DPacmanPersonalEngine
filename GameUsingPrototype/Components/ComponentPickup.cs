using OpenGL_Game.Managers;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentPickup : ComponentBase
    {
        public string SoundPrefab;

        public ComponentPickup() { }

        public override void OnTrigger(Entity otherEntity, ComponentCollider otherCollider)
        {
            base.OnTrigger(otherEntity, otherCollider);

            var soundEntity = EntityManager.Instance.SpawnPrefab(SoundPrefab);
            soundEntity.Transform.Position = Transform.Position;
            soundEntity.Transform.Rotation = Transform.Rotation;

            EntityManager.Instance.DestroyEntity(Entity);
        }
    }
}
