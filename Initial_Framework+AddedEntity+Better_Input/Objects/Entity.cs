using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OpenGL_Game.Components;
using OpenTK;

namespace OpenGL_Game.Objects
{
    class Entity
    {
        string name;
        List<IComponent> componentList = new List<IComponent>();
        ComponentTypes mask;
 
        public Entity(string name)
        {
            this.name = name;
            InstantiateEntity(Vector3.Zero, Quaternion.Identity, Vector3.One);
        }

        public Entity(string name, Vector3 position)
        {
            this.name = name;
            InstantiateEntity(position, Quaternion.Identity, Vector3.One);
        }

        public Entity(string name, Vector3 position, Quaternion rotation)
        {
            this.name = name;
            InstantiateEntity(position, rotation, Vector3.One);
        }

        public Entity(string name, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.name = name;
            InstantiateEntity(position, rotation, scale);
        }

        protected virtual void InstantiateEntity(Vector3 position, Quaternion rotation, Vector3 scale) { }

        /// <summary>Adds a single component</summary>
        public void AddComponent(IComponent component)
        {
            Debug.Assert(component != null, "Component cannot be null");

            componentList.Add(component);
            mask |= component.ComponentType;
        }

        public IComponent GetComponent(ComponentTypes typeToFind)
        {
            IComponent foundComponent = componentList.Find(delegate (IComponent component)
            {
                return component.ComponentType == typeToFind;
            });

            return foundComponent;
        }

        public String Name
        {
            get { return name; }
        }

        public ComponentTypes Mask
        {
            get { return mask; }
        }

        public List<IComponent> Components
        {
            get { return componentList; }
        }
    }
}
