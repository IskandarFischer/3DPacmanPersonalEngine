using PrototypeEngine.Objects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using PrototypeEngine.Managers;
using PrototypeEngine.Systems;

namespace PrototypeEngine.Components
{
    public class ComponentRigidbody : ComponentBase
    {
        [JsonIgnore]
        public bool Rigid = false;

        public ComponentRigidbody()
        {
            velocity = Vector3.Zero;
        }

        [JsonIgnore]
        Vector3 velocity;

        public ComponentRigidbody(Vector3 initialVelocity)
        {
            velocity = initialVelocity;
        }

        public ComponentRigidbody(float x, float y, float z)
        {
            velocity = new Vector3(x, y, z);
        }

        [JsonIgnore]
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public override void OnCollision(Entity otherEntity, ComponentCollider otherCollider)
        {
            base.OnCollision(otherEntity, otherCollider);

            Transform.Position = Transform.OldPosition;
        }

        public override void AddToSystems()
        {
            base.AddToSystems();

            SystemManager.Instance.GetSystem<SystemPhysics>().AddToList(Entity);
            SystemManager.Instance.GetSystem<SystemCollision>().AddToList(Entity);
        }
    }
}
