using OpenGL_Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    abstract class ComponentBase
    {
        public abstract ComponentTypes ComponentType
        {
            get;
        }

        private ComponentTransform transform;

        public ComponentTransform Transform
        {
            get
            {
                if (transform == null)
                    transform = (ComponentTransform)Entity.GetComponent(ComponentTypes.COMPONENT_TRANSFORM);

                return transform;
            }
        }

        public Entity Entity;
    }
}
