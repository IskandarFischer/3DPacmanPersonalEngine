using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentVelocity : IComponent
    {
        Vector3 velocity;

        public ComponentVelocity(Vector3 initialVelocity)
        {
            velocity = initialVelocity;
        }

        public ComponentVelocity(float x, float y, float z)
        {
            velocity = new Vector3(x, y, z);
        }

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_VELOCITY; }
        }
    }
}
