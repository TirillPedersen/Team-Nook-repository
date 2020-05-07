using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Character : AnimationSprite
    {
        private Vec2 _position;
        private Vec2 _oldPosition;
        private Vec2 _velocity;
        private Vec2 _acceleration;
        private static float _maxSpeed = 5.0f;
        private byte _currentAnimation;
        private float _animationSpeed;

        public Character() : base("Barry.png", 7, 1)
        {
            SetOrigin(width / 2, height / 2);
            _position = new Vec2(50, 50);
            _velocity = new Vec2(0, 0);
            _acceleration = new Vec2(0, 0);
            _animationSpeed = 0;
            _currentAnimation = 0;
        }

        private void PlayerMovement(byte currentInput)
        {
            switch (currentInput)
            {
                case 0:
                    _acceleration += new Vec2(0, -1.0f);
                    break;
                case 1:
                    _acceleration += new Vec2(-1.0f, 0);
                    break;
                case 2:
                    _acceleration += new Vec2(0, 1.0f);
                    break;
                case 3:
                    _acceleration += new Vec2(1.0f, 0);
                    break;
            }
        }

        private void PlayerInput()
        {
            if (Input.GetKey(Key.W)) PlayerMovement(0);
            if (Input.GetKey(Key.S)) PlayerMovement(2);
            if (Input.GetKey(Key.A)) PlayerMovement(1);
            if (Input.GetKey(Key.D)) PlayerMovement(3);
        }

        private void AddViscosity()
        {
            _acceleration = new Vec2(0, 0);
            _velocity *= 0.95f;
        }

        private void EulerIntegration()
        {
            _oldPosition = _position;
            _velocity += _acceleration;
            if (_velocity.Length() > _maxSpeed)
            {
                _velocity.Normalize();
                _velocity *= _maxSpeed;
            }
            _position += _velocity;
            SetXY(_position.x, _position.y);
        }

        private void PlayerAnimation(byte currentAnimation)
        {
            if (_velocity.Length() > 1) _animationSpeed = (Time.time / (150 - (int)_velocity.Length() * 15) % 3);
            currentFrame = (int)_animationSpeed;
            rotation = _velocity.GetAngleDegrees();
        }

        private void BoundaryCollision()
        {
            if (_position.x < width / 2)
            {
                float a = width / 2 - _oldPosition.x;
                float b = _oldPosition.x - _position.x;
                float POI = a / b;
                _position = _oldPosition + POI * _velocity;
            }
            else if (_position.x > game.width - width / 2)
            {
                float a = game.width - width / 2 - _oldPosition.x;
                float b = _oldPosition.x - _position.x;
                float POI = a / b;
                _position = _oldPosition + POI * _velocity;
            }
            else if (_position.y < height / 2)
            {
                float a = height / 2 - _oldPosition.y;
                float b = _oldPosition.y - _position.y;
                float POI = a / b;
                _position = _oldPosition + POI * _velocity;
            }
            else if (_position.y > game.height - height / 2)
            {
                float a = game.height - height / 2 - _oldPosition.y;
                float b = _oldPosition.y - _position.y;
                float POI = a / b;
                _position = _oldPosition + POI * _velocity;
            }
        }

        private void BoothCollision()
        {
            foreach (MarketStands currentStand in LevelLoader.MarketStandList)
            {
                //Implement actual collision here
            }
        }

        protected void Update()
        {
            AddViscosity();
            PlayerInput();
            BoundaryCollision();
            EulerIntegration();
            if (_velocity.Length() > 0.1) PlayerAnimation(_currentAnimation);
        }
    }
}