using OpenTK;

namespace PrototypeEngine.Scenes
{
    public interface IScene
    {
        void Render(FrameEventArgs e);
        void Update(FrameEventArgs e);
        void Close();
    }
}
