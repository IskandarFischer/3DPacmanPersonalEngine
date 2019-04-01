using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using PrototypeEngine.Objects;
using System.IO;
using PrototypeEngine.Utilities;
using Newtonsoft.Json;
using PrototypeEngine.Components;

namespace PrototypeEngine.Managers
{
    public class EntityManager : Singleton<EntityManager>
    {
        List<Entity> entityList;

        List<Entity> nextFrameList;

        List<Entity> entitytoDestroy;

        public bool inCycle = false;

        public EntityManager()
        {
            entityList = new List<Entity>();
            entitytoDestroy = new List<Entity>();
            nextFrameList = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            if (inCycle)
                nextFrameList.Add(entity);
            else
            {
                entityList.Add(entity);
                entity.AddToSystem();
            }
        }

        public Entity SpawnPrefab(string path)
        {
            if (!File.Exists(path))
                return null;

            FileStream fin = File.OpenRead(path);

            Entity prefab = null;

            using (StreamReader r = new StreamReader(fin))
            {
                JsonSerializerSettings set = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };

                string json = r.ReadToEnd();
                prefab = JsonConvert.DeserializeObject<Entity>(json, set);

                if (prefab != null)
                {
                    foreach (var item in prefab.Components)
                    {
                        item.SetComponent(prefab, prefab.GetComponent<ComponentTransform>());
                        item.OnPrefabCreate();
                    }

                    AddEntity(prefab);
                }
            }

            return prefab;
        }

        private Entity FindEntity(string name)
        {
            return entityList.Find(delegate (Entity e)
            {
                return e.Name == name;
            }
            );
        }

        public List<Entity> Entities()
        {
            return entityList;
        }

        public List<Entity> DestroyedEntites()
        {
            return entitytoDestroy;
        }

        public void DestroyEntity(Entity entity)
        {
            var myEntity = entityList.Find(e => e == entity);

            if (myEntity == null)
                return;

            entity.Enabled = false;

            entitytoDestroy.Add(entity);
        }

        public void InstantiateAllItem()
        {
            foreach (var entity in nextFrameList)
            {
                entityList.Add(entity);
                entity.AddToSystem();
            }

            nextFrameList.Clear() ;
        }

        public void DestroyEntityInCycle(Entity e)
        {
            e.DestroyEntity();
            entityList.Remove(e);
        }

        public void ClearDestroyedEntities()
        {
            entitytoDestroy.Clear();
        }

        public void OnSceneClose()
        {
            foreach (var e in entityList)
            {
                e.DestroyEntity();
            }
        }
    }
}
