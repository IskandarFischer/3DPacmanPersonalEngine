using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.Components
{
    public class ComponentColliderSphere : ComponentCollider
    {
        ColliderType colliderType = ColliderType.Sphere;

        public float Size;

        public override ColliderType Collider { get { return colliderType; } }

        public ComponentColliderSphere()
        {
            Size = 1.0f;
        }

        public ComponentColliderSphere(float sphereSize, bool trigger)
        {
            Trigger = trigger;
            Size = sphereSize;
        }
    }
}
