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

        List<ComponentBase> componentLL = new List<ComponentBase>();

        private ComponentTransform transform;

        public Entity(string name)
        {
            this.name = name;
            InstantiateEntity(Vector3.Zero, Vector3.Zero, Vector3.One);
        }

        public Entity(string name, Vector3 position)
        {
            this.name = name;
            InstantiateEntity(position, Vector3.Zero, Vector3.One);
        }

        public Entity(string name, Vector3 position, Vector3 rotation)
        {
            this.name = name;
            InstantiateEntity(position, rotation, Vector3.One);
        }

        public Entity(string name, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.name = name;
            InstantiateEntity(position, rotation, scale);
        }

        private void InstantiateEntity(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            AddComponent(new ComponentTransform(position, rotation, scale));
        }

        public void AddComponent(ComponentBase component)
        {
            Debug.Assert(component != null, "Component cannot be null");

            componentLL.Add(component);
            mask |= component.ComponentType;
        }

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

        public T GetComponent<T>()
        {
            foreach (ComponentBase item in componentLL)
            {
                if (item.GetType() == typeof(T))
                    return (T)Convert.ChangeType(item, typeof(T));
            }

            return default(T);
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

        public ComponentTransform Transform
        {
            get
            {
                if (transform == null)
                    transform = (ComponentTransform)GetComponent(ComponentTypes.COMPONENT_TRANSFORM);

                return transform;
            }
        }
    }
}
