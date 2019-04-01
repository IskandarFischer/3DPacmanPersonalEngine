using Newtonsoft.Json;
using OpenGL_Game.Systems;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentTimedDestroy : ComponentBase
    {
        public float TimeToDestroy;

        [JsonIgnore]
        float currentTimer = 0.0f;

        public ComponentTimedDestroy()
        {

        }

        public ComponentTimedDestroy(float timer)
        {
            TimeToDestroy = timer;
        }

        public bool CheckTimer(float time)
        {
            currentTimer += time;
            return currentTimer >= TimeToDestroy;
        }

        public override void AddToSystems()
        {
            base.AddToSystems();

            SystemManager.Instance.GetSystem<SystemTimedDestroy>().AddToList(Entity);
        }
    }
}
