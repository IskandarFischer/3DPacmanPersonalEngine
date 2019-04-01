using OpenGL_Game.Components;
using OpenGL_Game.Managers;
using OpenTK;
using PrototypeEngine.AI;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Systems
{
    class SystemAI : SystemBase
    {

        public void MoveToNextNode(ComponentAI ai)
        {
            List<Node> PossiblePaths = new List<Node>();

            foreach (var node in ai.TargetNode.Neighbours)
            {
                if (node == null)
                    continue;

                if (node.Walkable && node != ai.CurrentNode)
                    PossiblePaths.Add(node);
            }

            var newNode = PossiblePaths[AIManager.Instance.AIRandom.Next(0, PossiblePaths.Count)];
            ai.ToNextNode(newNode);
        }

        public Vector3 MoveTowards(Vector3 current, Vector3 target, float dt, float speed)
        {
            float distanceToTarget = (target - current).Length;
            Vector3 dirToTarget = (target - current).Normalized();

            var newPos = current + dirToTarget * dt * speed;

            if ((newPos - current).Length > distanceToTarget)
                newPos = target;

            return newPos;
        }

        public override void OnAction(Entity entity)
        {
            var ai = entity.GetComponent<ComponentAI>();

            var newPosition = MoveTowards(entity.Transform.Position, ai.TargetNode.Position, TimeManager.dt, ai.MovementSpeed);
            entity.Transform.Position = newPosition;

            if (newPosition == ai.TargetNode.Position)
            {
                MoveToNextNode(ai);
            }
        }
    }
}
