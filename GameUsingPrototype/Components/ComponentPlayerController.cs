using Newtonsoft.Json;
using OpenGL_Game.Systems;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentPlayerController : ComponentBase
    {
        ComponentRigidbody body;

        float movementSpeed = 120.0f;
        float rotationSpeed = 2.5f;

        public ComponentPlayerController()
        {
        }

        public ComponentPlayerController(float movSpeed, float rotSpeed)
        {
            movementSpeed = movSpeed;
        }

        [JsonIgnore]
        public float RotationSpeed { get { return rotationSpeed; } }

        [JsonIgnore]
        public float MovementSpeed { get { return movementSpeed; } }

        [JsonIgnore]
        public ComponentRigidbody RigidBody
        {
            get
            {
                if (body == null)
                    body = Entity.GetComponent<ComponentRigidbody>();

                return body;
            }
        }

        public override void AddToSystems()
        {
            base.AddToSystems();
            SystemManager.Instance.GetSystem<SystemPlayerController>().AddToList(Entity);
        }
    }
}
