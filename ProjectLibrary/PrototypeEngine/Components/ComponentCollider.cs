using PrototypeEngine.Managers;
using PrototypeEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.Components
{
    public enum ColliderType
    {
        Sphere,
        Cube
    }

    public abstract class ComponentCollider : ComponentBase
    {
        public abstract ColliderType Collider { get; }
        public bool Trigger;

        public override void AddToSystems()
        {
            base.AddToSystems();

            SystemManager.Instance.GetSystem<SystemCollision>().CollisionEntites.Add(Entity);

            if (Entity.GetComponent<ComponentRigidbody>() != null)
                SystemManager.Instance.GetSystem<SystemCollision>().AddToList(Entity);
        }
    }
}
