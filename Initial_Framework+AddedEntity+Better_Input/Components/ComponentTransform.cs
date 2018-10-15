using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentTransform : IComponent
    {
        public Vector3 Rotation { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }

        public ComponentTransform(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
            this.Rotation = Vector3.Zero;
            this.Scale = Vector3.One;
        }

        public ComponentTransform(Vector3 pos)
        {
            this.Position = pos;
            this.Rotation = Vector3.Zero;
            this.Scale = Vector3.One;
        }

        public ComponentTransform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }

        public Matrix4 ObjectTransform
        {
            get
            {
                Matrix4 objectMatrix = new Matrix4();

                objectMatrix = Matrix4.CreateScale(Scale);
                objectMatrix *= Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(Rotation));
                objectMatrix *= Matrix4.CreateTranslation(Position);

                return objectMatrix;
            }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_TRANSFORM; }
        }
    }
}
