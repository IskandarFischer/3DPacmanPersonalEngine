using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    [FlagsAttribute]
    enum ComponentTypes
    {
        COMPONENT_NONE = 0,
        COMPONENT_POSITION = 1 << 0,
        COMPONENT_GEOMETRY = 1 << 1,
        COMPONENT_TEXTURE = 1 << 2,
        COMPONENT_VELOCITY = 1 << 4,
        COMPONENT_AUDIO = 1 << 8,
        COMPONENT_TRANSFORM = 1 << 16
    }

    interface IComponent
    {
        ComponentTypes ComponentType
        {
            get;
        }
    }
}
