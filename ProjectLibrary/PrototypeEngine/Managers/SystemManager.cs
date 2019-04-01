using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using PrototypeEngine.Systems;
using PrototypeEngine.Objects;
using PrototypeEngine.Utilities;

namespace PrototypeEngine.Managers
{
    public class SystemManager : Singleton<SystemManager>
    {
        List<SystemBase> systemList = new List<SystemBase>();
        List<SystemBase> renderSystems = new List<SystemBase>();
        List<SystemBase> physicsSystem = new List<SystemBase>();

        public SystemManager()
        {
        }

        public void InitializeSystem()
        {
            SystemBase newSystem;

            newSystem = new SystemSkybox();
            renderSystems.Add(newSystem);

            newSystem = new SystemRender();
            renderSystems.Add(newSystem);

            newSystem = new SystemPhysics();
            physicsSystem.Add(newSystem);

            newSystem = new SystemCollision();
            physicsSystem.Add(newSystem);

            newSystem = new SystemAudio();
            systemList.Add(newSystem);
        }

        public void ActionSystems(EntityManager entityManager)
        {
            entityManager.InstantiateAllItem();

            entityManager.inCycle = true;

            foreach (var system in physicsSystem)
            {
                foreach (Entity e in system.EntitiesInSystem)
                {
                    if (!e.Enabled)
                        continue;

                    system.OnAction(e);
                }
            }

            foreach (var system in systemList)
            {
                foreach (Entity entity in system.EntitiesInSystem)
                {
                    if (!entity.Enabled)
                        continue;

                    system.OnAction(entity);
                }
            }

            foreach (var item in entityManager.DestroyedEntites())
            {
                foreach (var system in systemList)
                {
                    system.RemoveEntity(item);
                }
                foreach (var system in renderSystems)
                {
                    system.RemoveEntity(item);
                }
                foreach (var system in physicsSystem)
                {
                    system.RemoveEntity(item);
                }

                entityManager.DestroyEntityInCycle(item);
            }

            entityManager.inCycle = false;

            entityManager.ClearDestroyedEntities();
        }

        public void RenderSystems(EntityManager entityManager)
        {
            foreach (var system in renderSystems)
            {
                foreach (var e in system.EntitiesInSystem)
                {
                    if (!e.Enabled)
                        continue;

                    system.OnAction(e);
                }
            }
        }

        public T GetSystem<T>()
        {
            foreach (SystemBase item in renderSystems)
            {
                if (item.GetType() == typeof(T))
                    return (T)Convert.ChangeType(item, typeof(T));
            }

            foreach (SystemBase item in systemList)
            {
                if (item.GetType() == typeof(T))
                    return (T)Convert.ChangeType(item, typeof(T));
            }

            foreach (SystemBase item in physicsSystem)
            {
                if (item.GetType() == typeof(T))
                    return (T)Convert.ChangeType(item, typeof(T));
            }


            return default(T);
        }

        public void AddSystem(SystemBase system)
        {
            systemList.Add(system);
        }
    }
}
