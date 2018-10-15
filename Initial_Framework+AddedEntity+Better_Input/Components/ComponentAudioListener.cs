using OpenGL_Game.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentAudioListener : IComponent
    {
        public ComponentTypes ComponentType { get { return ComponentTypes.COMPONENT_AUDIOLISTENER; } }
    }
}
