using PrototypeEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.Systems
{
    public abstract class SystemBase
    {
        public List<Entity> EntitiesInSystem = new List<Entity>();
        public abstract void OnAction(Entity entity);

        public void AddToList(Entity e)
        {
            EntitiesInSystem.Add(e);
        }

        public virtual void RemoveEntity(Entity e)
        {
            EntitiesInSystem.Remove(e);
        }

        // Property signatures: 
        string Name
        {
            get;
        }
    }
}
