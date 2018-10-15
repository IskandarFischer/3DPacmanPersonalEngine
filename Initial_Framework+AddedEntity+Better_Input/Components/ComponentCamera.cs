using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentCamera : IComponent
    {
        public ComponentTypes ComponentType { get { return ComponentTypes.COMPONENT_CAMERA; } }

        public static ComponentCamera MainCamera;

        public ComponentCamera()
        {
            if (MainCamera == null)
                MainCamera = this;
        }
    }
}
