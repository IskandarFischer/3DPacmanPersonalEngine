using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Scenes;
using PrototypeEngine.Utilities;
using OpenTK;

namespace PrototypeEngine.Systems
{
    public class SystemCollision : SystemBase
    {
        public List<Entity> CollisionEntites = new List<Entity>();

        public override void RemoveEntity(Entity e)
        {
            base.RemoveEntity(e);
            CollisionEntites.Remove(e);
        }

        public bool CheckCollisionBetweenTwoEntity(Entity firstEntity, ComponentCollider firstCollider, Entity secondEntity, ComponentCollider secondCollider)
        {
            if (firstCollider.Collider == ColliderType.Cube && secondCollider.Collider == ColliderType.Cube)
                return false;

            else if (firstCollider.Collider == ColliderType.Sphere && secondCollider.Collider == ColliderType.Cube)
                return SphereToCubeCollision(firstEntity, (ComponentColliderSphere)firstCollider, secondEntity, (ComponentColliderCube)secondCollider);

            else if (firstCollider.Collider == ColliderType.Cube && secondCollider.Collider == ColliderType.Sphere)
                return SphereToCubeCollision(secondEntity, (ComponentColliderSphere)secondCollider, firstEntity, (ComponentColliderCube)firstCollider);

            else if (firstCollider.Collider == ColliderType.Sphere && secondCollider.Collider == ColliderType.Sphere)
                return SphereToSphereCollision(firstEntity, (ComponentColliderSphere)firstCollider, secondEntity, (ComponentColliderSphere)secondCollider);

            return false;
        }

        bool SphereToSphereCollision(Entity firstEntity, ComponentColliderSphere firstCollider, Entity secondEntity, ComponentColliderSphere secondCollider)
        {
            if ((secondEntity.Transform.Position - firstEntity.Transform.Position).Length <= firstCollider.Size + secondCollider.Size)
                return true;

            return false;
        }

        bool SphereToCubeCollision(Entity sphereEntity, ComponentColliderSphere sphereCollider, Entity cubeEntity, ComponentColliderCube cubeCollider)
        {
            var closestPointToCube = sphereEntity.Transform.Position + (sphereEntity.Transform.Position - cubeEntity.Transform.Position).Normalized() * sphereCollider.Size;
            var OldClosestPointToCube = sphereEntity.Transform.OldPosition + (sphereEntity.Transform.OldPosition - cubeEntity.Transform.Position).Normalized() * sphereCollider.Size;

            var normalMov = closestPointToCube - OldClosestPointToCube;
            normalMov.Normalize();

            var movementNormal = Vector3.Cross(normalMov, new Vector3(0, 1, 0));

            var rightSide = cubeEntity.Transform.Position + cubeEntity.Transform.Right * (cubeCollider.Size.X);
            var leftSide = cubeEntity.Transform.Position + (-cubeEntity.Transform.Right) * (cubeCollider.Size.X);

            if (SegmentToSegmentIntersection(OldClosestPointToCube, closestPointToCube, movementNormal,
                rightSide + cubeEntity.Transform.Forward * (cubeCollider.Size.Z),
                leftSide + cubeEntity.Transform.Forward * (cubeCollider.Size.Z),
                cubeEntity.Transform.Forward))
                return true;

            if (SegmentToSegmentIntersection(OldClosestPointToCube, closestPointToCube, movementNormal,
                rightSide + -cubeEntity.Transform.Forward * (cubeCollider.Size.Z),
                 leftSide + -cubeEntity.Transform.Forward * (cubeCollider.Size.Z),
                 -cubeEntity.Transform.Forward))
                return true;

            var forwardSide = cubeEntity.Transform.Position + cubeEntity.Transform.Forward * (cubeCollider.Size.Z);
            var backwardSide = cubeEntity.Transform.Position + (-cubeEntity.Transform.Forward) * (cubeCollider.Size.Z);

            if (SegmentToSegmentIntersection(OldClosestPointToCube, closestPointToCube, movementNormal,
                forwardSide + cubeEntity.Transform.Right * (cubeCollider.Size.X),
                backwardSide + cubeEntity.Transform.Right * (cubeCollider.Size.X),
                cubeEntity.Transform.Right))
                return true;

            if (SegmentToSegmentIntersection(OldClosestPointToCube, closestPointToCube, movementNormal,
                forwardSide + -cubeEntity.Transform.Right * (cubeCollider.Size.X),
                backwardSide + -cubeEntity.Transform.Right * (cubeCollider.Size.X),
                -cubeEntity.Transform.Right))
                return true;

            return false;
        }

        bool SegmentToSegmentIntersection(Vector3 startPosition, Vector3 endPosition, Vector3 firstNormal, Vector3 secondSegmentStart, Vector3 secondSegmentEnd, Vector3 secondSegmentNormal)
        {
            var firstDir = (endPosition - secondSegmentStart).Normalized();
            var secondDir = (startPosition - secondSegmentStart).Normalized();

            var dot = Vector3.Dot(firstDir, secondSegmentNormal) * Vector3.Dot(secondDir, secondSegmentNormal);

            if (dot >= 0)
                return false;

            firstDir = (secondSegmentStart - startPosition).Normalized();
            secondDir = (secondSegmentEnd - startPosition).Normalized();

            dot = Vector3.Dot(firstDir, firstNormal) * Vector3.Dot(secondDir, firstNormal);

            if (dot >= 0)
                return false;

            return true;
        }

        public override void OnAction(Entity entity)
        {
            foreach (var e in CollisionEntites)
            {
                if (e.Enabled == false)
                    return;

                if (e == entity)
                    continue;


                var eCol = (ComponentCollider)e.GetDerivedTypes(typeof(ComponentCollider));
                var entityCol = (ComponentCollider)entity.GetDerivedTypes(typeof(ComponentCollider));

                if (entity.GetComponent<ComponentRigidbody>().Rigid && !eCol.Trigger)
                    continue;

                var col = CheckCollisionBetweenTwoEntity(entity, entityCol, e, eCol);

                if (!col)
                    continue;

                if (eCol.Trigger || entityCol.Trigger)
                {
                    entity.OnTrigger(e, eCol, entityCol);
                    e.OnTrigger(entity, entityCol, eCol);
                }
                else
                {
                    entity.OnCollision(e, eCol, entityCol);
                    e.OnCollision(entity, entityCol, eCol);
                }
            }
        }
    }
}
