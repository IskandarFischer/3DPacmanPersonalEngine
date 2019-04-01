using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using PrototypeEngine.Components;
using OpenTK;
using System.IO;
using Newtonsoft.Json;
using PrototypeEngine.Managers;

namespace PrototypeEngine.Objects
{
    public class Entity
    {
        public bool Enabled = true;

        string name;

        List<ComponentBase> componentList = new List<ComponentBase>();

        private ComponentTransform transform;

        public Entity() { }

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

        public void OnTrigger(Entity otherEntity, ComponentCollider otherCollider, ComponentCollider myCollider)
        {
            foreach (var c in componentList)
            {
                c.OnTrigger(otherEntity, otherCollider);
            }
        }

        public void OnCollision(Entity otherEntity, ComponentCollider otherCollider, ComponentCollider myCollider)
        {
            foreach (var c in componentList)
            {
                c.OnCollision(otherEntity, otherCollider);
            }
        }

        public void AddComponent(ComponentBase component)
        {
            componentList.Add(component);
            component.SetComponent(this, Transform);
        }

        public ComponentBase GetDerivedTypes(Type t)
        {
            foreach (var item in componentList)
            {
                if (item.GetType().IsSubclassOf(t.GetType()) || t.IsAssignableFrom(item.GetType()))
                    return item;
            }
            return null;
        }
        
        public void AddToSystem()
        {
            foreach (var item in componentList)
            {
                item.AddToSystems();
            }
        }

        public T GetComponent<T>()
        {
            Type typeparam = typeof(T);

            foreach (ComponentBase item in componentList)
            {
                if (item.GetType() == typeof(T))
                    return (T)Convert.ChangeType(item, typeof(T));
            }

            return default(T);
        }

        public void SetPrefab()
        {
            foreach (var item in componentList)
            {
                item.SetComponent(this, GetComponent<ComponentTransform>());
                item.OnPrefabCreate();
            }
        }

        public void CreatePrefab(string path)
        {
            JsonSerializerSettings set = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            string json = JsonConvert.SerializeObject(this, set);
            File.WriteAllText(path, json);
        }
        public void DestroyEntity()
        {
            foreach (var component in componentList)
            {
                component.DestroyComponent();
            }
        }

        public void OnPrefabCreate()
        {
            foreach (var item in Components)
            {
                item.SetComponent(this, transform);
                item.OnPrefabCreate();
            }
        }

        public String Name
        {
            get { return name; }
        }

        public List<ComponentBase> Components
        {
            get { return componentList; }
        }

        [JsonIgnore]
        public ComponentTransform Transform
        {
            get
            {
                if (transform == null)
                    transform = GetComponent<ComponentTransform>();

                return transform;
            }
        }
    }
}
