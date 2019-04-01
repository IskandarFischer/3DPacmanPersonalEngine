using Newtonsoft.Json;
using OpenGL_Game.Systems;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentFollow : ComponentBase
    {
        [JsonIgnore]
        public Entity EntityToFollow;

        public ComponentFollow()
        {
            if (ComponentCamera.MainCamera.Entity == null)
                return;

            EntityToFollow = ComponentCamera.MainCamera.Entity;
        }

        public override void AddToSystems()
        {
            base.AddToSystems();
            SystemManager.Instance.GetSystem<SystemFollow>().AddToList(Entity);
        }
    }
}
