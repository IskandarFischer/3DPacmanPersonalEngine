using Newtonsoft.Json;
using PrototypeEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.Components
{
    public abstract class ComponentBase
    {
        [JsonIgnore]
        private Entity e;
        [JsonIgnore]
        private ComponentTransform t;

        public virtual void DestroyComponent() { }
        public virtual void OnTrigger(Entity otherEntity, ComponentCollider otherCollider) { }
        public virtual void OnCollision(Entity otherEntity, ComponentCollider otherCollider) { }

        [JsonIgnore]
        public Entity Entity
        {
            get { return e; }
        }

        [JsonIgnore]
        public ComponentTransform Transform
        {
            get { return t; }
        }

        public void SetComponent(Entity e, ComponentTransform t)
        {
                this.e = e;
                this.t = t;
        }

        public ComponentBase()
        {
        }


        public virtual void OnClose() { }
        public virtual void AddToSystems() { }
        public virtual void OnPrefabCreate() { }
    }
}
