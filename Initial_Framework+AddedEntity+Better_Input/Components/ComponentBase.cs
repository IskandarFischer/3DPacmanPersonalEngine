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
    }
}
