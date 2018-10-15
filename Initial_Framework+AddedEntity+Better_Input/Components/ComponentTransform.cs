using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentTransform : IComponent
    {
        Vector3 position;
        Quaternion rotation;
        Vector3 scale;

        public ComponentTransform(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
            this.rotation = Quaternion.Identity;
            this.scale = Vector3.One;
        }

        public ComponentTransform(Vector3 pos)
        {
            this.position = pos;
            this.rotation = Quaternion.Identity;
            this.scale = Vector3.One;
        }

        public ComponentTransform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = Quaternion.FromEulerAngles(rotation);
            this.scale = scale;
        }

        public ComponentTransform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }


        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Quaternion Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_TRANSFORM; }
        }
    }
}
