using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dungeon_2
{
    public class Object
    {
        Texture2D _sprite;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Rectangle BoundingBox {
            get {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    _sprite.Width,
                    _sprite.Height
                );
            }
        }

        public Object(Texture2D texture, Vector2 position) {
            _sprite = texture;
            Position = position;
        }

        public Object(Texture2D sprite, Vector2 position, Vector2 velocity) {
            _sprite = sprite;
            Position = position;
            Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_sprite, Position, Color.White);
        }
    }
}