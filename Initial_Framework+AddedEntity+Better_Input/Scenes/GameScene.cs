using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenGL_Game.Components;
using OpenGL_Game.Systems;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Scenes
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class GameScene : Scene
    {
        public Matrix4 view, projection;
        public static float dt = 0;
        EntityManager entityManager;
        SystemManager systemManager;
        AudioManager audioManager;

        public static GameScene gameInstance;

        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {
            gameInstance = this;
            entityManager = new EntityManager();
            systemManager = new SystemManager();
            audioManager = new AudioManager();

            // Set the title of the window
            sceneManager.Title = "Game";
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;
            // Set Keyboard events to go to a method in this class
            sceneManager.Keyboard.KeyDown += Keyboard_KeyDown;

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            view = Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 800f / 480f, 0.01f, 100f);

            CreateSystems();
            CreateEntities();
            // TODO: Add your initialization logic here
        }

        private void CreateEntities()
        {
            Entity newEntity;

            newEntity = new Entity("Triangle1");
            newEntity.Transform.Position = new Vector3(-1.0f, 0.0f, -3.0f);
            newEntity.AddComponent(new ComponentGeometry("Geometry/TriangleGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/spaceship.png"));
            newEntity.AddComponent(new ComponentVelocity(Vector3.Zero));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav", true, new Vector3(-1.0f, 0.0f, -3.0f)));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Square1");
            newEntity.Transform.Position = new Vector3(1.0f, 0.0f, -3.0f);
            newEntity.AddComponent(new ComponentGeometry("Geometry/SquareGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/spaceship.png"));
            newEntity.AddComponent(new ComponentVelocity(Vector3.Zero));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Triangle2");

            newEntity.Transform.Position = new Vector3(3.0f, 0.0f, -3.0f);
            //newEntity.Transform.Rotation = new Vector3(0, 45, 0);

            newEntity.AddComponent(new ComponentGeometry("Geometry/TriangleGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/spaceship.png"));
            newEntity.AddComponent(new ComponentVelocity(Vector3.Zero));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Square2");
            newEntity.Transform.Position = new Vector3(-3.0f, 0.0f, -3.0f);
            newEntity.AddComponent(new ComponentGeometry("Geometry/SquareGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/spaceship.png"));
            newEntity.AddComponent(new ComponentVelocity(new Vector3(0,1,1)));
            entityManager.AddEntity(newEntity);
        }

        private void CreateSystems()
        {
            ISystem newSystem;

            newSystem = new SystemRender();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemPhysics();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemAudio();
            systemManager.AddSystem(newSystem);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Update(FrameEventArgs e)
        {
            //if (GamePad.GetState(1).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Key.Escape))
            //    sceneManager.Exit();

            dt = (float)e.Time;

            // TODO: Add your update logic here

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            systemManager.ActionSystems(entityManager);
        }

        /// <summary>
        /// This is called when the game exits.
        /// </summary>
        public override void Close()
        {
            audioManager.StopAudioManager();
        }

        public void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    break;
                case Key.Down:
                    break;
                case Key.Escape:
                    // Set Keyboard events to go to a method in this class
                    sceneManager.Keyboard.KeyDown -= Keyboard_KeyDown;
                    sceneManager.ChangeScene(SceneTypes.SCENE_MAIN_MENU);
                    break;
            }
        }
    }
}
