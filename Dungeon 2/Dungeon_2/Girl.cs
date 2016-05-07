using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dungeon_2
{
    public class Girl
    {
        private Vector2 _pos;
        private float _maxSpeed = 4.0f;
        private const float _accel = 0.2f;
        private const float _decel = 0.5f;
        private Vector2 _speed = Vector2.Zero;
        private string _animKey = "up";
        private int _animIndex = 4;
        private AnimatedSprite _spriteSheet;
        private Dictionary<string, AnimationDefinition> _animations;

        public Girl(AnimatedSprite spriteSheet, Vector2 pos) {
            _spriteSheet = spriteSheet;
            _pos = pos;
            _animations = new Dictionary<string, AnimationDefinition>() {
                { "left", new AnimationDefinition(new int[2] { 0, 1 }, 10) },
                { "right", new AnimationDefinition(new int[2] { 2, 3 }, 10) },
                { "up", new AnimationDefinition(new int[4] { 4, 5, 4, 7 }, 10) },
                { "down", new AnimationDefinition(new int[4] { 8, 9, 8, 11 }, 10) }
            };
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont text) {
            _spriteSheet.draw(spriteBatch, _animIndex, _pos);
            spriteBatch.DrawString(text, _speed.X + ", " + _speed.Y + ", " + _speed.Length(), Vector2.Zero, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
        }

        /**
        * Position Girl
        */
        public void move(Vector2 priority) {
            var newPos = _pos;
            if (priority.X != 0) {
                _animKey = (priority.X == -1 ? "left" : "right");
                _speed.X += _accel;
                if (_speed.X > _maxSpeed) {
                    _speed.X = _maxSpeed;
                }
                newPos.X += priority.X * _speed.X;
            } else {
                _speed.X -= _decel;
                if (_speed.X < 0) {
                    _speed.X = 0;
                }
            }
            if (priority.Y != 0) {
                _animKey = (priority.Y == -1 ? "up" : "down");
                _speed.Y += _accel;
                if (_speed.Y > _maxSpeed) {
                    _speed.Y = _maxSpeed;
                }
                newPos.Y += priority.Y * _speed.Y;
            } else {
                _speed.Y -= _decel;
                if (_speed.Y < 0) {
                    _speed.Y = 0;
                }
            }
            Vector2 diff = Vector2.Subtract(newPos, _pos);
            if (diff.Length() > _maxSpeed) {
                diff = Vector2.Normalize(diff);
                diff = Vector2.Multiply(diff, _maxSpeed);
                _speed.X = Math.Abs(diff.X);
                _speed.Y = Math.Abs(diff.Y);
            }
            _pos = Vector2.Add(_pos, diff);
            _animIndex = _spriteSheet.next(_animations[_animKey], priority.X != 0 || priority.Y != 0);
        }
    }
}
