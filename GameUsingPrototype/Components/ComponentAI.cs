using Newtonsoft.Json;
using OpenGL_Game.Managers;
using OpenGL_Game.Systems;
using PrototypeEngine.AI;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentAI : ComponentBase
    {
        [JsonIgnore]
        Node currentNode;
        [JsonIgnore]
        Node targetNode;

        public float MovementSpeed = 0.25f;

        [JsonIgnore]
        public Node CurrentNode { get { return currentNode; } }
        [JsonIgnore]
        public Node TargetNode { get { return targetNode; } }

        [JsonIgnore]
        bool IsEatable = false;

        public string OnGhostEat;

        public ComponentAI() { }

        public ComponentAI(float movSpeed, string ghostEaten)
        {
            OnGhostEat = ghostEaten;
            MovementSpeed = movSpeed;
        }

        public void SpawnAI(Node startNode, Node exitNode)
        {
            currentNode = startNode;
            targetNode = exitNode;

            Transform.Position = startNode.Position;
        }

        public void ToNextNode(Node newNode)
        {
            currentNode = targetNode;
            targetNode = newNode;
        }

        public override void OnTrigger(Entity otherEntity, ComponentCollider otherCollider)
        {
            base.OnTrigger(otherEntity, otherCollider);

            if (IsEatable)
                KillGhost();
            else
            {
                if (otherEntity.GetComponent<ComponentLives>() != null)
                    otherEntity.GetComponent<ComponentLives>().TakeDamage();
            }
        }

        void KillGhost()
        {
            //EntityManager.Instance.DestroyEntity(Entity);
            var a = EntityManager.Instance.SpawnPrefab("Prefabs/eatghost.txt");
            a.Transform.Position = Transform.Position;
            a.Transform.Rotation = Transform.Rotation;
            AIManager.Instance.EatGhost(Entity);
        }

        public override void AddToSystems()
        {
            base.AddToSystems();
            SystemManager.Instance.GetSystem<SystemAI>().AddToList(Entity);
        }

        public void ChangeEatState(bool eat)
        {
            IsEatable = eat;
        }
    }
}
