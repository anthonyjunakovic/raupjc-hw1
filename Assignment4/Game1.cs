using Assignment3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Assignment4
{
    /// <summary>
    /// Contains all of the game constants used by the objects
    /// </summary>
    public class GameConstants
    {
        public const float PaddleDefaulSpeed = 0.9f;
        public const int PaddleDefaultWidth = 200;
        public const int PaddleDefaulHeight = 20;
        public const float DefaultInitialBallSpeed = 0.4f;
        public const float DefaultBallBumpSpeedIncreaseFactor = 1.05f;
        public const int DefaultBallSize = 40;
        public const int WallDefaultSize = 900;
        public const float MaxBallSpeed = 1.0f;
    }


    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// Scores of the players
        /// </summary>
        private int ScoreBottom, ScoreTop;
        /// <summary >
        /// Bottom paddle object
        /// </ summary >
        public Paddle PaddleBottom { get; private set; }
        /// <summary >
        /// Top paddle object
        /// </ summary >
        public Paddle PaddleTop { get; private set; }
        /// <summary >
        /// Ball object
        /// </ summary >
        public Ball Ball { get; private set; }
        /// <summary >
        /// Background image
        /// </ summary >
        public Background Background { get; private set; }
        /// <summary >
        /// Sound when ball hits an obstacle .
        /// SoundEffect is a type defined in Monogame framework
        /// </ summary >
        public SoundEffect HitSound { get; private set; }
        /// <summary >
        /// Background music . Song is a type defined in Monogame framework
        /// </ summary >
        public Song Music { get; private set; }
        /// <summary >
        /// Generic list that holds Sprites that should be drawn on screen
        /// </ summary >
        private IGenericList<Sprite> SpritesForDrawList = new GenericList<Sprite>();
        /// <summary>
        /// List of all walls
        /// </summary>
        public GenericList<Wall> Walls { get; set; }
        /// <summary>
        /// List of all goals
        /// </summary>
        public GenericList<Wall> Goals { get; set; }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferHeight = 900,
                PreferredBackBufferWidth = 500
            };
            Content.RootDirectory = "Content";
        }

        private void UpdateTitle()
        {
            Window.Title = $"Pong Game ({ScoreTop} - {ScoreBottom})";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            UpdateTitle();
            Window.Position = new Point(
                (GraphicsDevice.DisplayMode.Width - Window.ClientBounds.Width) / 2,
                (GraphicsDevice.DisplayMode.Height - Window.ClientBounds.Height) / 2
            );

            var screenBounds = GraphicsDevice.Viewport.Bounds;
            PaddleBottom = new Paddle(GameConstants.PaddleDefaultWidth, GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleBottom.X = screenBounds.Width / 2.0f - PaddleBottom.Width / 2.0f;
            PaddleBottom.Y = screenBounds.Bottom - PaddleBottom.Height;
            PaddleTop = new Paddle(GameConstants.PaddleDefaultWidth, GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleTop.X = screenBounds.Width / 2.0f - PaddleBottom.Width / 2.0f;
            PaddleTop.Y = screenBounds.Top;
            Ball = new Ball(40, GameConstants.DefaultInitialBallSpeed, GameConstants.DefaultBallBumpSpeedIncreaseFactor);
            Ball.X = screenBounds.Width / 2.0f - Ball.Width / 2.0F;
            Ball.Y = screenBounds.Height / 2.0f - Ball.Height / 2.0F;
            Background = new Background(screenBounds.Width, screenBounds.Height);

            // Add our game objects to the sprites that should be drawn collection .
            SpritesForDrawList.Add(Background);
            SpritesForDrawList.Add(PaddleBottom);
            SpritesForDrawList.Add(PaddleTop);
            SpritesForDrawList.Add(Ball);

            Walls = new GenericList<Wall>();
            Walls.Add(new Wall(null, -GameConstants.WallDefaultSize, 0, GameConstants.WallDefaultSize, screenBounds.Height));
            Walls.Add(new Wall(null, screenBounds.Right, 0, GameConstants.WallDefaultSize, screenBounds.Height));

            Goals = new GenericList<Wall>();
            Goals.Add(new Wall(PaddleBottom, 0, screenBounds.Height, screenBounds.Width, GameConstants.WallDefaultSize));
            Goals.Add(new Wall(PaddleTop, screenBounds.Top, -GameConstants.WallDefaultSize, screenBounds.Width, GameConstants.WallDefaultSize));
            

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Initialize new SpriteBatch object which will be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Set textures
            Texture2D paddleTexture = Content.Load<Texture2D>("paddle");
            PaddleBottom.Texture = paddleTexture;
            PaddleTop.Texture = paddleTexture;
            Ball.Texture = Content.Load<Texture2D>("ball");
            Background.Texture = Content.Load<Texture2D>("background");

            // Load sounds
            HitSound = Content.Load<SoundEffect>("hit");
            Music = Content.Load<Song>("music");

            // Start background music
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Music);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var bounds = GraphicsDevice.Viewport.Bounds;

            var touchState = Keyboard.GetState();

            if (touchState.IsKeyDown(Keys.Left)) {
                PaddleBottom.X -= (float)(PaddleBottom.Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            else if (touchState.IsKeyDown(Keys.Right))
            {
                PaddleBottom.X += (float)(PaddleBottom.Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            PaddleBottom.X = MathHelper.Clamp(PaddleBottom.X, bounds.Left, bounds.Right - PaddleBottom.Width);

            if (touchState.IsKeyDown(Keys.A))
            {
                PaddleTop.X -= (float)(PaddleTop.Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            else if (touchState.IsKeyDown(Keys.D))
            {
                PaddleTop.X += (float)(PaddleTop.Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            PaddleTop.X = MathHelper.Clamp(PaddleTop.X, bounds.Left, bounds.Right - PaddleTop.Width);

            var ballPositionChange = Ball.Direction * new Vector2((float)(gameTime.ElapsedGameTime.TotalMilliseconds * Ball.Speed));
            Ball.X += ballPositionChange.X;
            Ball.Y += ballPositionChange.Y;

            foreach (Wall wall in Walls)
            {
                if (CollisionDetector.Overlaps(Ball, wall))
                {
                    Ball.Direction.TurnHorizontal();
                    Ball.Speed *= Ball.BumpSpeedIncreaseFactor;
                }
            }

            foreach (Wall goal in Goals)
            {
                if (CollisionDetector.Overlaps(Ball, goal))
                {
                    if (goal.Player.Equals(PaddleBottom))
                    {
                        ScoreTop++;
                    }
                    else
                    {
                        ScoreBottom++;
                    }
                    UpdateTitle();
                    Ball.X = bounds.Width / 2 - Ball.Width / 2;
                    Ball.Y = bounds.Height / 2 - Ball.Height / 2;
                    Ball.Speed = GameConstants.DefaultInitialBallSpeed;
                    HitSound.Play();
                    break;
                }
            }

            if ((CollisionDetector.Overlaps(Ball, PaddleBottom)) || (CollisionDetector.Overlaps(Ball, PaddleTop)))
            {
                Ball.Direction.TurnVertical();
                Ball.Speed *= Ball.BumpSpeedIncreaseFactor;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Start drawing.
            spriteBatch.Begin();
            foreach (Sprite sprite in SpritesForDrawList)
            {
                sprite.DrawSpriteOnScreen(spriteBatch);
            }

            // End drawing.
            // Send all gathered details to the graphic card in one batch.
            spriteBatch.End();
            base.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
