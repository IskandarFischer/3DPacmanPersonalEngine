using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;
using OpenTK;

namespace OpenGL_Game.Systems
{
    class SystemPhysics : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_TRANSFORM | ComponentTypes.COMPONENT_VELOCITY);

        public string Name
        {
            get { return "SystemPhysics"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) != MASK)
                return;

            var allComponents = entity.Components;

            ComponentTransform pos = (ComponentTransform)entity.GetComponent(ComponentTypes.COMPONENT_TRANSFORM);
            ComponentVelocity vel = (ComponentVelocity)entity.GetComponent(ComponentTypes.COMPONENT_VELOCITY);

            Motion(pos, vel);
        }

        public void Motion(ComponentTransform pos, ComponentVelocity vel)
        {
            Vector3 currentPos = pos.Position;
            Vector3 velocity = vel.Velocity;
            
            pos.Position = currentPos + velocity * GameScene.dt;
        }
    }
}
