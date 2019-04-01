using Newtonsoft.Json;
using OpenGL_Game.Components;
using OpenGL_Game.Managers;
using OpenGL_Game.Systems;
using OpenGL_Game.Utilities;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PrototypeEngine.Components;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Scenes;
using PrototypeEngine.Systems;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace OpenGL_Game.Scenes
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class GameScene : Scene
    {
        EntityManager entityManager;
        SystemManager systemManager;
        AudioManager audioManager;
        TimeManager timeManager;
        GameManager gameManager;
        AIManager aiManager;

        public static GameScene gameInstance;

        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {
            gameInstance = this;
            entityManager = new EntityManager();
            systemManager = new SystemManager();
            audioManager = new AudioManager();
            timeManager = new TimeManager();
            gameManager = new GameManager();
            aiManager = new AIManager();

            // Set the title of the window
            sceneManager.Title = "Game";
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;
            // Set Keyboard events to go to a method in this class

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            // TODO: Add your initialization logic here
            CreateSystems();

        }

        private void CreateEntities()
        {
            entityManager.SpawnPrefab("Prefabs/skybox.txt");
            var floor = entityManager.SpawnPrefab("Prefabs/floor.txt");
            floor.Transform.Position = new Vector3(25, -1, 25);
            BoardReader.SetBoard("Scenes/pac.txt");
            AIManager.Instance.SpawnGhosts();
            GameManager.Instance.SpawnPlayer();

            ////Heart Prefab
            //var heart = new Entity("Heart");
            //heart.AddComponent(new ComponentRenderer("Geometry/SquareGeometry.txt", "Textures/heart.png"));
            //heart.GetComponent<ComponentRenderer>().ComponentUI = true;
            //heart.Transform.Position = new Vector3(0.5f, 0.5f, 0);
            //heart.Transform.Scale = new Vector3(0.036f, 0.05f, 0.1f);
            //heart.CreatePrefab("Prefabs/heart.txt");

            ////Skybox Prefab
            //var skybox = new Entity("Skybox");
            //skybox.AddComponent(new ComponentRenderer("Geometry/CubeGeometry.txt", "Textures/starsky.png"));
            //skybox.AddComponent(new ComponentSkybox());
            //skybox.CreatePrefab("Prefabs/skybox.txt");

            ////floor Prefab
            //var floor = new Entity("Floor");
            //floor.Transform.Scale = new Vector3(25, 0.5f, 25);
            //floor.AddComponent(new ComponentRenderer("Geometry/CubeGeometry.txt", "Textures/floor.jpg"));
            //floor.GetComponent<ComponentRenderer>().RepeatTexture = true;
            //floor.CreatePrefab("Prefabs/floor.txt");

            ////Wall Prefab
            //var e = new Entity("Wall");
            //e.Transform.Position = new Vector3(0.0f, 0.0f, 0.0f);
            //e.Transform.Scale = new Vector3(0.5f, 0.5f, 0.5f);
            //e.AddComponent(new ComponentRenderer("Geometry/CubeGeometry.txt", "Textures/wall.png"));
            //e.AddComponent(new ComponentColliderCube());
            //e.CreatePrefab("Prefabs/wall.txt");

            ////Player Prefab 
            //Entity player = new Entity("Player");
            //player.AddComponent(new ComponentColliderSphere(0.25f, false));
            //player.AddComponent(new ComponentPlayerController(120.0f, 2.5f));
            //player.AddComponent(new ComponentRigidbody(Vector3.Zero));
            //player.AddComponent(new ComponentCamera(new Vector3(0, 1f, 0)));
            //player.AddComponent(new ComponentAudioListener());
            //player.AddComponent(new ComponentLives("Prefabs/onlifelost.txt"));
            //player.CreatePrefab("Prefabs/player.txt");

            ////PickupItem Prefab
            //var pickup = new Entity("pickup");
            //pickup.AddComponent(new ComponentRenderer("Geometry/sphere.bin", "Textures/yellowtext.jpg"));
            //pickup.AddComponent(new ComponentColliderSphere(0.5f, true));
            //pickup.AddComponent(new ComponentPointPickup("Prefabs/pointpicksound.txt"));
            //pickup.Transform.Scale = Vector3.One * 0.15f;
            //pickup.CreatePrefab("Prefabs/pickup.txt");

            ////Powerup Prefab
            //var powerup = new Entity("PowerUp");
            //powerup.Transform.Scale = Vector3.One * 0.15f;
            //powerup.AddComponent(new ComponentRenderer("Geometry/sphere.bin", "Textures/purpletext.jpg"));
            //powerup.AddComponent(new ComponentColliderSphere(0.5f, true));
            //powerup.AddComponent(new ComponentPowerUp("Prefabs/pointpicksound.txt"));
            //powerup.AddComponent(new ComponentAudio("Audio/beep.wav", true, powerup.Transform.Position));
            //powerup.CreatePrefab("Prefabs/powerup.txt");

            ////AI Prefab
            //var aI = new Entity("Ghost");
            //aI.Transform.Scale = new Vector3(0.25f, 0.75f, 0.25f);
            //aI.AddComponent(new ComponentRenderer("Geometry/model.bin", "Textures/modeltext.jpg"));
            //aI.AddComponent(new ComponentAI(4f, "Prefabs/eatghost.txt"));
            //aI.AddComponent(new ComponentColliderSphere(0.25f, true));
            //aI.CreatePrefab("Prefabs/ghost.txt");

            ////#region soundprefab
            //var pickupSound = new Entity("pickupsound");
            //pickupSound.AddComponent(new ComponentAudio("Audio/pickup.wav", false, Vector3.Zero));
            //pickupSound.AddComponent(new ComponentTimedDestroy(1f));
            //pickupSound.CreatePrefab("Prefabs/pointpicksound.txt");

            //var powerupSound = new Entity("powerupsound");
            //powerupSound.AddComponent(new ComponentAudio("Audio/alienspaceship.wav", false, Vector3.Zero));
            //powerupSound.AddComponent(new ComponentTimedDestroy(1f));
            //powerupSound.CreatePrefab("Prefabs/powerupsound.txt");

            //var onLifeLost = new Entity("lifelost");
            //onLifeLost.AddComponent(new ComponentAudio("Audio/lifelost.wav", false, Vector3.Zero));
            //onLifeLost.AddComponent(new ComponentTimedDestroy(1f));
            //onLifeLost.CreatePrefab("Prefabs/onlifelost.txt"); 

            //var powerUpActive = new Entity("powerupactive");
            //powerUpActive.AddComponent(new ComponentAudio("Audio/buzz.wav", true, Vector3.Zero));
            //powerUpActive.AddComponent(new ComponentTimedDestroy(15.0f));
            //powerUpActive.AddComponent(new ComponentFollow());
            //powerUpActive.CreatePrefab("Prefabs/powerupactive.txt");

            //var eatGhost = new Entity("eatGhost");
            //eatGhost.AddComponent(new ComponentAudio("Audio/eatghost.wav", false, Vector3.Zero));
            //eatGhost.AddComponent(new ComponentTimedDestroy(1f));
            //eatGhost.CreatePrefab("Prefabs/eatghost.txt");
            //#endregion

            //BoardReader.ReadBoard(@"\Scenes\PacmanMap.xlsx", "Scenes/pac.txt");
        }

        private void CreateSystems()
        {
            SystemBase newSystem;

            systemManager.InitializeSystem();

            newSystem = new SystemPlayerController();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemTimedDestroy();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemAI();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemFollow();
            systemManager.AddSystem(newSystem);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Update(FrameEventArgs e)
        {
            AIManager.Instance.AIRandom = new Random();

            //if (GamePad.GetState(1).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Key.Escape))
            //    sceneManager.Exit();

            TimeManager.dt = (float)e.Time;

            if (!started && startTime > currentStart)
                currentStart += (float)e.Time;

            if (!started && startTime <= currentStart)
                StartGame();

            systemManager.ActionSystems(entityManager);

            var state = Mouse.GetState();

            SystemManager.Instance.GetSystem<SystemPlayerController>().PreviousMousePosition = new Vector2(state.X, state.Y);
        }

        void StartGame()
        {
            started = true;

            sceneManager.Keyboard.KeyDown += Keyboard_KeyDown;
            sceneManager.Keyboard.KeyUp += Keyboard_KeyUp;

            CreateEntities();
        }

        bool started = false;
        bool endGame = false;
        int startTime = 4;
        float currentStart = 0;

        bool victory = false;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            float width = sceneManager.Width, height = sceneManager.Height, fontSize = Math.Min(width, height) / 10f;

            systemManager.RenderSystems(entityManager);

            if (!started)
            {
                GUI.Label(new Rectangle(0, (int)(height / 3.5f), (int)width, (int)(fontSize * 2f)), "Game Starting In", (int)fontSize, StringAlignment.Center);
                GUI.Label(new Rectangle(0, (int)(height / 2.5f), (int)width, (int)(fontSize * 2f)), (startTime - Math.Floor(currentStart)).ToString(), (int)fontSize, StringAlignment.Center);
                GUI.Render();
            }

            if (endGame)
            {
                string endText = victory ? "You Win with " + GameManager.Instance.Lives.ToString() + " Lives" : "You Lose";
                GUI.Label(new Rectangle(0, (int)(height / 3.5f), (int)width, (int)(fontSize * 2f)), endText, (int)fontSize, StringAlignment.Center);
                GUI.Label(new Rectangle(0, (int)(height / 2.5f), (int)width, (int)(fontSize * 2f)), "Press Enter To Return", (int)(fontSize / 1.5f), StringAlignment.Center);

                GUI.Render();
            }
        }

        /// <summary>
        /// This is called when the game exits.
        /// </summary>
        public override void Close()
        {
            sceneManager.Keyboard.KeyDown -= Keyboard_KeyDown;
            sceneManager.Keyboard.KeyUp -= Keyboard_KeyUp;

            ResourceManager.ClearResourceManager();
            entityManager.OnSceneClose();
        }

        public void EndGame(bool win)
        {
            AIManager.Instance.StopTimer();
            AIManager.Instance.DisableGhosts();
            audioManager.StopAudioManager();

            victory = win;
            endGame = true;
            //sceneManager.ChangeScene(new MainMenuScene(sceneManager));
        }

        public void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    break;
                case Key.Enter:
                    if (endGame)
                        sceneManager.ChangeScene(new MainMenuScene(sceneManager));
                    break;
                case Key.Escape:
                    // Set Keyboard events to go to a method in this class
                    sceneManager.ChangeScene(new MainMenuScene(sceneManager));
                    break;
                case Key.G:
                    if (endGame)
                        break;
                    AIManager.Instance.DebugGhosts();
                    break;
                case Key.C:
                    GameManager.Instance.DebugPlayer();
                    break;
                case Key.P:
                    if (!endGame)
                        GameManager.Instance.PickUpAllItems();
                    break;
            }
        }

        private void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e) { }
    }
}
