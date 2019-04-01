using OpenGL_Game.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Systems
{
    class SystemTimedDestroy : SystemBase
    {
        public override void OnAction(Entity entity)
        {
            var e = entity.GetComponent<ComponentTimedDestroy>();

            var time = e.CheckTimer(TimeManager.dt);
            if (time)
            {
                EntityManager.Instance.DestroyEntity(entity);
            }
        }
    }
}
