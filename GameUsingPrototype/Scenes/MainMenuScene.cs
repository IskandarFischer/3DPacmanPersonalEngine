using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using PrototypeEngine.Scenes;
using PrototypeEngine.Managers;

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

            sceneManager.Keyboard.KeyDown += Keyboard_KeyDown;
        }

        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    sceneManager.Keyboard.KeyDown -= Keyboard_KeyDown;
                    sceneManager.ChangeScene(new GameScene(sceneManager));
                    break;
                case Key.Escape:
                    Environment.Exit(1);
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
            GUI.Label(new Rectangle(0, (int)(fontSize / 0.35f), (int)width, (int)(fontSize * 2f)), "Press Enter To Start", (int)fontSize / 2, StringAlignment.Center);
            GUI.Label(new Rectangle(0, (int)(fontSize / 0.25f), (int)width, (int)(fontSize * 2f)), "Press Escape To Close Game", (int)fontSize / 2, StringAlignment.Center);


            GUI.Render();
        }

        public override void Close()
        {
        }
    }
}