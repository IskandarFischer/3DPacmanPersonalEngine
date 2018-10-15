using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace OpenGL_Game.Systems
{
    class SystemAudio : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_TRANSFORM | ComponentTypes.COMPONENT_AUDIO);

        public string Name
        {
            get { return "SystemAudio"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) != MASK)
                return;

            var allComponents = entity.Components;

            ComponentAudio audio = (ComponentAudio)entity.GetComponent(ComponentTypes.COMPONENT_AUDIO);
            ComponentTransform position = (ComponentTransform)entity.GetComponent(ComponentTypes.COMPONENT_TRANSFORM);

            audio.UpdatePosition(position.Position, new Vector3(0, 0, 33), new Vector3(0,0,-1), Vector3.UnitY);
        }

        public SystemAudio()
        {

        }

        IComponent GetComponent(List<IComponent> components, ComponentTypes typeToFind)
        {
            IComponent foundComponent = components.Find(delegate (IComponent component)
            {
                return component.ComponentType == typeToFind;
            });

            return foundComponent;
        }
    }
}
