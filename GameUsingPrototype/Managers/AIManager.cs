using OpenGL_Game.Components;
using OpenTK;
using PrototypeEngine.AI;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace OpenGL_Game.Managers
{
    class AIManager : Singleton<AIManager>
    {
        Node ghostSpawnDestination;
        Node[] ghostSpawnPositions;

        List<Entity> ghostEntities = new List<Entity>();
        int maxGhost = 4;

        public Random AIRandom = new Random();

        Timer powerupTimer = new Timer();

        public AIManager()
        {
            powerupTimer.Elapsed += Timer_Elapsed;
        }

        public void DisableGhosts()
        {
            foreach (var g in ghostEntities)
            {
                g.Enabled = false;
            }
        }

        public void DebugGhosts()
        {
            foreach (var g in ghostEntities)
            {
                g.Enabled = !g.Enabled;
            }
        }

        public void ResetGhosts()
        {
            foreach (var g in ghostEntities)
            {
                EntityManager.Instance.DestroyEntity(g);
            }

            ghostEntities.Clear();
            SpawnGhosts();
        }

        public void SpawnGhosts()
        {
            var amountOfGhostToSpawn = maxGhost - ghostEntities.Count;

            for (int i = 0; i < amountOfGhostToSpawn; i++)
            {
                CreateNewGhost();
            }
        }

        void MoveGhostToSpawn()
        {
            foreach (var e in ghostEntities)
            {
                if (e.Enabled)
                    continue;

                var nodeToSpawn = ghostSpawnPositions[AIRandom.Next(ghostSpawnPositions.Length)];
                e.GetComponent<ComponentAI>().SpawnAI(nodeToSpawn, ghostSpawnDestination);
                e.Enabled = true;
            }
        }

        public void EatGhost(Entity e)
        {
            e.Enabled = false;
        }

        void CreateNewGhost()
        {
            var nodeToSpawn = ghostSpawnPositions[AIRandom.Next(ghostSpawnPositions.Length)];
            var ghost = EntityManager.Instance.SpawnPrefab("Prefabs/ghost.txt");
            ghost.GetComponent<ComponentAI>().SpawnAI(nodeToSpawn, ghostSpawnDestination);

            ghostEntities.Add(ghost);
        }

        public void SetGhostSpawn(Node ghostSpawn, List<Node> spawns)
        {
            this.ghostSpawnDestination = ghostSpawn;
            ghostSpawnPositions = spawns.ToArray();
        }

        public void EatPowerUp()
        {
            powerupTimer.Stop();
            powerupTimer.Interval = 15 * 1000;
            powerupTimer.AutoReset = false;
            powerupTimer.Enabled = true;

            GhostEatable(true);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GhostEatable(false);
            MoveGhostToSpawn();
        }

        public void StopTimer()
        {
            powerupTimer.Stop();
        }

        void GhostEatable(bool state)
        {
            foreach (var g in ghostEntities)
            {
                g.GetComponent<ComponentAI>().ChangeEatState(state);
            }
        }
    }
}
