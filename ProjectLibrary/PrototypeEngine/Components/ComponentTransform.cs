using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using PrototypeEngine.Utilities;

namespace PrototypeEngine.Components
{
    public class ComponentTransform : ComponentBase
    {

        public ComponentTransform()
        {
            oldPos = Position;
        }

        [JsonConverter(typeof(QuaternionConverter))]
        public Quaternion Rotation { get; set; }
        [JsonConverter(typeof(VectorConverter))]
        public Vector3 Position
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                oldPos = value;
            }
        }
        [JsonConverter(typeof(VectorConverter))]
        public Vector3 Scale { get; set; }
        [JsonConverter(typeof(VectorConverter))]
        public Vector3 OldPosition { get { return oldPos; } }

        
        Vector3 currentPosition;
        Vector3 oldPos;

        public void PhysicsMove(Vector3 newPosition)
        {
            oldPos = currentPosition;
            currentPosition = newPosition;
        }

        public ComponentTransform(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
            this.Rotation = Quaternion.Identity;
            this.Scale = Vector3.One;
            

            oldPos = Position;
        }

        public ComponentTransform(Vector3 pos)
        {
            this.Position = pos;
            this.Rotation = Quaternion.Identity;
            this.Scale = Vector3.One;

            oldPos = Position;
        }

        public ComponentTransform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.Position = position;
            this.Rotation = Quaternion.FromEulerAngles(rotation);
            this.Scale = scale;

            oldPos = Position;
        }

        [JsonIgnore]
        public Matrix4 ObjectTransform
        {
            get
            {
                Matrix4 objectMatrix = new Matrix4();

                objectMatrix = Matrix4.CreateScale(Scale);
                objectMatrix *= Matrix4.CreateFromQuaternion(Rotation);
                objectMatrix *= Matrix4.CreateTranslation(Position);

                return objectMatrix;
            }
        }

        [JsonIgnore]
        public Vector3 Right
        {
            get
            {
                var vr = new Vector3(1, 0, 0);
                var rotatedVector = Rotation * vr;

                return rotatedVector;
            }
        }

        [JsonIgnore]
        public Vector3 Up
        {
            get
            {
                var vr = new Vector3(0, 1, 0);
                var rotatedVector = Rotation * vr;

                return rotatedVector;
            }
        }

        [JsonIgnore]
        public Vector3 Forward
        {
            get
            {
                var vr = new Vector3(0, 0, 1);
                var rotatedVector = Rotation * vr;

                return rotatedVector;
            }
        }

        public override void OnPrefabCreate()
        {
            base.OnPrefabCreate();
            oldPos = Transform.Position;
        }
    }
}
