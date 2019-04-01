using PrototypeEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PrototypeEngine.Utilities;
using Newtonsoft.Json;

namespace PrototypeEngine.Components
{
    public class ComponentCamera : ComponentBase
    {
        [JsonConverter(typeof(VectorConverter))] public Vector3 PositionFix;

        public static ComponentCamera MainCamera
        {
            get { return camera; }
        }

        static ComponentCamera camera;

        public ComponentCamera()
        {
            camera = this;
        }

        public ComponentCamera(Vector3 posFix)
        {
            camera = this;
            PositionFix = posFix;
        }

        public void SetMainCamera()
        {
            camera = this;
        }

        public override void DestroyComponent()
        {
            base.DestroyComponent();

            camera = null;
        }
    }
}
