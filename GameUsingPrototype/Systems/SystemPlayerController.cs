using OpenGL_Game.Components;
using OpenTK;
using OpenTK.Input;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Systems
{
    class SystemPlayerController : SystemBase
    {
        public Vector2 PreviousMousePosition;

        public SystemPlayerController()
        {
            PreviousMousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        void CheckInputsVelocity(ComponentPlayerController cpc)
        {
            float speed = 0.0f;
            Vector3 forward = Vector3.Zero;
            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                speed += cpc.MovementSpeed;
                forward = cpc.Transform.Forward;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                speed = cpc.MovementSpeed;
                if (forward != Vector3.Zero)
                    forward = Vector3.Zero;
                else
                    forward = -cpc.Transform.Forward;
            }

            cpc.RigidBody.Velocity = forward * speed * TimeManager.dt;
        }

        void CheckInputsRotation(ComponentPlayerController cpc, Entity e)
        {
            float newRightRotation = 0.0f;

            if (Keyboard.GetState().IsKeyDown(Key.Left))
                newRightRotation += cpc.RotationSpeed;

            if (Keyboard.GetState().IsKeyDown(Key.Right))
                newRightRotation -= cpc.RotationSpeed;

            float changeInX = Mouse.GetState().X - PreviousMousePosition.X;

            newRightRotation -= changeInX * cpc.RotationSpeed;

            if (newRightRotation > cpc.RotationSpeed)
                newRightRotation = cpc.RotationSpeed;
            else if (newRightRotation < -cpc.RotationSpeed)
                newRightRotation = -cpc.RotationSpeed;

            e.Transform.Rotation *= Quaternion.FromEulerAngles(new Vector3(0, newRightRotation * TimeManager.dt, 0));
        }

        public override void OnAction(Entity entity)
        {
            var cpc = entity.GetComponent<ComponentPlayerController>();

            CheckInputsVelocity(cpc);
            CheckInputsRotation(cpc, entity);
        }
    }
}
