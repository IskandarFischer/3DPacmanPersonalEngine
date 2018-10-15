using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using OpenGL_Game.Managers;

namespace OpenGL_Game.Scenes
{
    class MainMenuScene : Scene
    {
        public MainMenuScene(SceneManager sceneManager) : base(sceneManager)
        {
            // Set the title of the window
            sceneManager.Title = "Main Menu";
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;

            sceneManager.Mouse.ButtonDown += Mouse_ButtonDown;
        }

        private void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButton.Left:
                    sceneManager.Mouse.ButtonDown -= Mouse_ButtonDown;
                    sceneManager.ChangeScene(SceneTypes.SCENE_GAME);
                    break;
            }
        }

        public override void Update(FrameEventArgs e)
        {
        }

        public override void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, sceneManager.Width, 0, sceneManager.Height, -1, 1);

            GUI.clearColour = Color.CornflowerBlue;

            //Display the Title
            float width = sceneManager.Width, height = sceneManager.Height, fontSize = Math.Min(width, height) / 10f;
            GUI.Label(new Rectangle(0, (int)(fontSize / 2f), (int)width, (int)(fontSize * 2f)), "Main Menu", (int)fontSize, StringAlignment.Center);

            GUI.Render();
        }

        public override void Close()
        {
        }
    }
}