using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Components
{
    class ComponentGeometry : IComponent
    {
        Geometry geometry;

        public ComponentGeometry(string geometryName)
        {
            this.geometry = ResourceManager.LoadGeometry(geometryName);
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_GEOMETRY; }
        }

        public Geometry Geometry()
        {
            return geometry;
        }
    }
}
