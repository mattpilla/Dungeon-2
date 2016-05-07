using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_2
{
    public class D2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _spriteSheet; // Girl's sprites
        private SpriteFont _text; // Text
        private Color _backColor = new Color(10, 10, 120);
        private Texture2D _title; // Title screen image
        private Girl _girl; // Girl!
        private bool[] _dir = new bool[4]; // 0: left, 1: right, 2: up, 3: down
        private Vector2 _priority; // Directional priority for Girl's movement
        public static long FrameCount { get; set; } // Total number of frames since game has been turned on
        public GameState _gameState; // Game State

        /**
        * Possible states the game can be in
        */
        public enum GameState
        {
            TitleScreen,
            GameStarted
        }

        /**
        * Constructor: called at runtime
        */
        public D2() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 768;
            _graphics.PreferredBackBufferHeight = 432;
            Content.RootDirectory = "Content";
        }

        /**
        * Setup
        */
        protected override void Initialize() {
            base.Initialize();
            IsMouseVisible = true;
        }

        /**
        * Load
        */
        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _title = Content.Load<Texture2D>("title");
            _text = Content.Load<SpriteFont>("Text");
            _spriteSheet = Content.Load<Texture2D>("girl");
            Reset();
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /**
        * Reset: called every time you reset the game
        */
        private void Reset() {
            _gameState = GameState.TitleScreen;
            _girl = new Girl(new AnimatedSprite(_spriteSheet, 1, 29), new Vector2(300, 200));
        }

        /**
        * Draw function: runs once at the start of every frame
        */
        protected override void Update(GameTime gameTime) {
            FrameCount++;
            UpdateInput();

            base.Update(gameTime);
        }

        /**
        * Check for input
        */
        private void UpdateInput() {
            KeyboardState state = Keyboard.GetState();

            if (_gameState == GameState.TitleScreen) {
                if (state.IsKeyDown(Keys.Z)) {
                    _gameState = GameState.GameStarted;
                }
            } else if (_gameState == GameState.GameStarted) {
                /**
                * Movement
                */
                if (state.IsKeyDown(Keys.Left)) {
                    _dir[0] = true;
                    _priority.X = -1;
                }
                if (state.IsKeyDown(Keys.Right)) {
                    _dir[1] = true;
                    _priority.X = 1;
                }
                if (state.IsKeyDown(Keys.Up)) {
                    _dir[2] = true;
                    _priority.Y = -1;
                }
                if (state.IsKeyDown(Keys.Down)) {
                    _dir[3] = true;
                    _priority.Y = 1;
                }

                if (state.IsKeyUp(Keys.Left)) {
                    _dir[0] = false;
                }
                if (state.IsKeyUp(Keys.Right)) {
                    _dir[1] = false;
                }
                if (state.IsKeyUp(Keys.Up)) {
                    _dir[2] = false;
                }
                if (state.IsKeyUp(Keys.Down)) {
                    _dir[3] = false;
                }

                /**
                * Check for reset
                */
                if (state.IsKeyDown(Keys.R)) {
                    Reset();
                    return;
                }

                UpdatePos(); // Update position
            }
        }

        /**
        * Update Girl's position
        */
        private void UpdatePos() {
            if (!_dir[0] && !_dir[1]) {
                _priority.X = 0;
            }
            if (!_dir[2] && !_dir[3]) {
                _priority.Y = 0;
            }
            _girl.move(_priority);
        }

        /**
        * Draw function for actually drawing to the screen
        */
        protected override void Draw(GameTime gameTime) {
            if (_gameState == GameState.TitleScreen) {
                _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                _spriteBatch.Draw(_title, Vector2.Zero, Color.White);
                _spriteBatch.End();
            } else if (_gameState == GameState.GameStarted) {
                GraphicsDevice.Clear(_backColor);

                _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                _girl.draw(_spriteBatch, _text);
                _spriteBatch.End();

                base.Draw(gameTime);
            }
        }
    }
}
