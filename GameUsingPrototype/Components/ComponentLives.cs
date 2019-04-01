using Newtonsoft.Json;
using OpenGL_Game.Managers;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentLives : ComponentBase
    {
        int currentLives = 3;

        public int CurrentLives { get { return currentLives; } }

        public string OnLifeLost;

        public ComponentLives() { }

        public ComponentLives(string lifeLostSound)
        {
            OnLifeLost = lifeLostSound;
        }

        public void TakeDamage()
        {
            currentLives--;

            if (currentLives <= 0)
            {
                GameManager.Instance.LifeLost();

                GameManager.Instance.EndGame(false);
                EntityManager.Instance.DestroyEntity(Entity);
            }
            else
            {
                GameManager.Instance.LifeLost();

                var soundEntity = EntityManager.Instance.SpawnPrefab(OnLifeLost);
                soundEntity.Transform.Position = GameManager.Instance.spawnPoint;
                soundEntity.Transform.Rotation = Transform.Rotation;
            }
        }
    }
}
