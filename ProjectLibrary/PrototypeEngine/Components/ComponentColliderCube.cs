using Newtonsoft.Json;
using OpenTK;
using PrototypeEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.Components
{
    public class ComponentColliderCube : ComponentCollider
    {
        ColliderType colliderType = ColliderType.Cube;

        [JsonConverter(typeof(VectorConverter))]
        public Vector3 Size;

        public override ColliderType Collider { get { return colliderType; } }

        public ComponentColliderCube()
        {
            Size = Vector3.One;
        }

        public ComponentColliderCube(Vector3 size)
        {
            Size = size;
        }
    }
}
