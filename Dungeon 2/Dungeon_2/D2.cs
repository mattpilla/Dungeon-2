using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_2
{
    public class D2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _text;
        private Color _backColor = Color.BlueViolet;
        private Girl _girl;
        private bool[] _dir = new bool[4];
        private Vector2 _priority;
        public static long FrameCount { get; set; }

        public D2() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _text = Content.Load<SpriteFont>("Text");
            _girl = new Girl(new AnimatedSprite(Content.Load<Texture2D>("girl"), 1, 29), new Vector2(300, 200));
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime) {
            FrameCount++;
            UpdateInput();
            UpdatePos();

            base.Update(gameTime);
        }

        private void UpdateInput() {
            KeyboardState state = Keyboard.GetState();

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
        }

        private void UpdatePos() {
            if (!_dir[0] && !_dir[1]) {
                _priority.X = 0;
            }
            if (!_dir[2] && !_dir[3]) {
                _priority.Y = 0;
            }
            _girl.move(_priority);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(_backColor);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _girl.draw(_spriteBatch, _text);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
