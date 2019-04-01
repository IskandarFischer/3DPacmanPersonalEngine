using OpenGL_Game.Components;
using OpenGL_Game.Scenes;
using OpenTK;
using PrototypeEngine.AI;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Managers
{
    class GameManager : Singleton<GameManager>
    {
        Node[,] BoardNodes;
        public Vector3 spawnPoint;
        Entity player;

        public int Lives { get { return player.GetComponent<ComponentLives>().CurrentLives; } }

        List<Entity> Hearts = new List<Entity>();

        public void EndGame(bool win)
        {
            GameScene.gameInstance.EndGame(win);
        }

        public void SetBoard(Node[,] board)
        {
            BoardNodes = board;
        }

        public void SetSpawnPoint(Vector3 spawn)
        {
            spawnPoint = spawn;
        }

        public void SpawnPlayer()
        {
            player = EntityManager.Instance.SpawnPrefab("Prefabs/player.txt");
            player.GetComponent<ComponentCamera>().SetMainCamera();
            player.GetComponent<ComponentAudioListener>().ChangeAudioListener();
            MovePlayerToSpawn();

            var length = Lives;
            for (int i = 0; i < length; i++)
            {
                CreateHearts();
            }
        }

        void CreateHearts()
        {
            var h = EntityManager.Instance.SpawnPrefab("Prefabs/heart.txt");
            h.Transform.Position = new Vector3(0.9f, 0.8f, 0) - new Vector3(0.15f,0,0) * Hearts.Count;
            Hearts.Add(h);
        }

        public void LifeLost()
        {
            AIManager.Instance.ResetGhosts();
            MovePlayerToSpawn();

            EntityManager.Instance.DestroyEntity(Hearts.Last());
            Hearts.RemoveAt(Hearts.Count - 1);
        }

        public void MovePlayerToSpawn()
        {
            if (player == null || spawnPoint == null)
                return;

            player.Transform.Position = spawnPoint;
            player.Transform.Rotation = Quaternion.Identity;
        }

        int currentAmountPickUp;

        public void SetupPickupBoard(int totalItems)
        {
            currentAmountPickUp = totalItems;
        }

        public void PickupPowerUp()
        {
            var end = CheckEndGame();

            if (!end)
            {
                EntityManager.Instance.SpawnPrefab("Prefabs/powerupactive.txt");
                AIManager.Instance.EatPowerUp();
            }
        }

        public void PickUpAllItems()
        {
            currentAmountPickUp = 0;
            CheckEndGame();
        }

        public void PickupItem()
        {
            CheckEndGame();
        }

        bool CheckEndGame()
        {
            currentAmountPickUp--;

            if (currentAmountPickUp <= 0)
            {
                EndGame(true);

                foreach (var hearts in Hearts)
                {
                    EntityManager.Instance.DestroyEntity(hearts);
                }

                return true;
            }

            return false;
        }

        public void DebugPlayer()
        {
            var rb = player.GetComponent<ComponentRigidbody>();

            rb.Rigid = !rb.Rigid;
        }
    }
}
