using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Scenes;
using OpenTK;

namespace PrototypeEngine.Systems
{
   public class SystemPhysics : SystemBase
    {
        public void Motion(ComponentTransform pos, ComponentRigidbody vel)
        {
            Vector3 currentPos = pos.Position;
            Vector3 velocity = vel.Velocity;
            
            pos.PhysicsMove(currentPos + velocity * TimeManager.dt);
        }

        public override void OnAction(Entity entity)
        {
            var allComponents = entity.Components;

            ComponentTransform pos = entity.Transform;
            ComponentRigidbody vel = entity.GetComponent<ComponentRigidbody>();

            Motion(pos, vel);
        }
    }
}
