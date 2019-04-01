using Newtonsoft.Json;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.Components
{
    public class ComponentRenderer : ComponentBase
    {
        public string GeometryPath;
        public string TexturePath;

        [JsonIgnore]
        Geometry geometry;
        [JsonIgnore]
        int texture;

        public bool RepeatTexture = false;
        public bool ComponentUI = false;

        public Geometry Geometry { get { return geometry; } }
        public int Texture { get { return texture; } }

        public ComponentRenderer()
        {

        }

        public ComponentRenderer(string geometryPath, string texturePath)
        {
            GeometryPath = geometryPath;
            this.geometry = ResourceManager.LoadGeometry(geometryPath);

            TexturePath = texturePath;
            texture = ResourceManager.LoadTexture(texturePath);
        }

        public override void OnPrefabCreate()
        {
            base.OnPrefabCreate();

            if (GeometryPath != null)
                geometry = ResourceManager.LoadGeometry(GeometryPath);

            if (TexturePath != null)
                texture = ResourceManager.LoadTexture(TexturePath);
        }

        public override void AddToSystems()
        {
            base.AddToSystems();

            SystemManager.Instance.GetSystem<SystemRender>().AddToList(Entity);
        }
    }
}
