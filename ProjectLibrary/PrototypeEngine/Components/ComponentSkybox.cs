using PrototypeEngine.Managers;
using PrototypeEngine.Systems;

namespace PrototypeEngine.Components
{
    public class ComponentSkybox : ComponentBase
    {
        public override void AddToSystems()
        {
            base.AddToSystems();
            SystemManager.Instance.GetSystem<SystemSkybox>().AddToList(Entity);
        }
    }
}
