using OpenGL_Game.Components;
using PrototypeEngine.Objects;
using PrototypeEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Systems
{
    class SystemFollow : SystemBase
    {
        public override void OnAction(Entity entity)
        {
            entity.Transform.Position = entity.GetComponent<ComponentFollow>().EntityToFollow.Transform.Position;
        }
    }
}
