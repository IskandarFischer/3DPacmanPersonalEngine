#region Using Statements
using PrototypeEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using PrototypeEngine;
using OpenGL_Game.Scenes;
#endregion

namespace OpenGL_Game
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class MainEntry
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var game = new SceneManager();
            game.ChangeScene(new MainMenuScene(game));
            using (game)
                game.Run();
        }
    }
#endif
}
