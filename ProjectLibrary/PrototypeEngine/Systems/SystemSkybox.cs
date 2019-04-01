using OpenTK;
using OpenTK.Graphics.OpenGL;
using PrototypeEngine.Components;
using PrototypeEngine.Objects;

namespace PrototypeEngine.Systems
{
    class SystemSkybox : SystemBase
    {
        public override void OnAction(Entity entity)
        {
            if (ComponentCamera.MainCamera == null)
                return;

            entity.Transform.Position = ComponentCamera.MainCamera.Transform.Position + ComponentCamera.MainCamera.PositionFix;
        }
    }
}
