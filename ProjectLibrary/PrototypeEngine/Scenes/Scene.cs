using OpenTK;
using PrototypeEngine.Managers;

namespace PrototypeEngine.Scenes
{
    public abstract class Scene : IScene
    {
        protected SceneManager sceneManager;

        public Scene(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public abstract void Render(FrameEventArgs e);

        public abstract void Update(FrameEventArgs e);

        public abstract void Close();
    }
}
