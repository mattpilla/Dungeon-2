using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_2
{
    public class AnimatedSprite
    {
        public Texture2D _sheet;
        public int _rows;
        public int _columns;

        public AnimatedSprite(Texture2D sheet, int rows, int columns) {
            _sheet = sheet;
            _rows = rows;
            _columns = columns;
        }

        public int next(AnimationDefinition def, bool moving) {
            int[] order = def.order();
            if (moving) {
                if (D2.FrameCount % def.rate() == 0) {
                    def.Index++;
                    if (def.Index >= order.Length) {
                        def.Index = 0;
                    }
                }
            } else {
                def.Index = 0;
            }
            return order[def.Index];
        }

        public void draw(SpriteBatch spriteBatch, int currentFrame, Vector2 location) {
            int width = _sheet.Width / _columns;
            int height = _sheet.Height / _rows;
            int row = (int)((float)currentFrame / (float)_columns);
            int column = currentFrame % _columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(_sheet, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
