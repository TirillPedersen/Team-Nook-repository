using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private float _animationSpeed, _timePassed;

        public Character() : base("PlayerSpritesheet.png", 4, 3)
        {
            SetOrigin(width / 2, height / 2);
            _position = new Vec2(150, 150);
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
            _velocity *= 0.9f;
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
            _timePassed += Time.deltaTime;
            if (_velocity.Length() > 1) _animationSpeed = 1 + (Time.time / 70 % 10);
            currentFrame = (int)_animationSpeed;
            rotation = _velocity.GetAngleDegrees();
        }

        private void BoundaryCollision()
        {
            if (_position.x < width / 2)
            {
                float a = _oldPosition.x - width/2;
                float b = _oldPosition.x - _position.x;
                float POI = a / b;
                _position = _oldPosition + POI * _velocity;
            }
            else if (_position.x > game.width - width / 2)
            {
                float a = _oldPosition.x - (game.width - width / 2);
                float b = _oldPosition.x - _position.x;
                float POI = a / b;
                _position = _oldPosition + POI * _velocity;
            }
            else if (_position.y < height / 2)
            {
                float a = _oldPosition.y - height / 2;
                float b = _oldPosition.y - _position.y;
                float POI = a / b;
                _position = _oldPosition + POI * _velocity;
            }
            else if (_position.y > game.height - height / 2)
            {
                float a = _oldPosition.y - (game.height - height / 2);
                float b = _oldPosition.y - _position.y;
                float POI = a / b;
                _position = _oldPosition + POI * _velocity;
            }
        }

        private void BoothCollision()
        {
            foreach (MarketStands currentStand in LevelLoader.MarketStandList)
            {
                List<Vec2> vectorsToCorners = new List<Vec2>();
                vectorsToCorners.Add(new Vec2(currentStand.x - currentStand.width / 2, currentStand.y - currentStand.height / 2));
                vectorsToCorners.Add(new Vec2(currentStand.x + currentStand.width / 2, currentStand.y - currentStand.height / 2));
                vectorsToCorners.Add(new Vec2(currentStand.x + currentStand.width / 2, currentStand.y + currentStand.height / 2));
                vectorsToCorners.Add(new Vec2(currentStand.x - currentStand.width / 2, currentStand.y + currentStand.height / 2));

                List<Vec2> sideVectors = new List<Vec2>();
                sideVectors.Add(vectorsToCorners.ElementAt(1) - vectorsToCorners.ElementAt(0));
                sideVectors.Add(vectorsToCorners.ElementAt(2) - vectorsToCorners.ElementAt(1));
                sideVectors.Add(vectorsToCorners.ElementAt(3) - vectorsToCorners.ElementAt(2));
                sideVectors.Add(vectorsToCorners.ElementAt(0) - vectorsToCorners.ElementAt(3));

                byte currentReferenceCornerVector = 0;

                foreach (Vec2 currentSideVector in sideVectors)
                {
                    Vec2 differenceVector = _position - vectorsToCorners.ElementAt(currentReferenceCornerVector);
                    //float distance = differenceVector.Dot(currentSideVector.Normal());

                    float a = -differenceVector.Dot(currentSideVector.Normal()) - width / 2;
                    float b = _velocity.Dot(currentSideVector.Normal());

                    float currentVectorLength = currentSideVector.Length();

                    float t = a / b;

                    if (a > -width / 2 && a < 0) t = 0;

                    if (b >= 0 && t >= 0 && t <= 1)
                    {
                        Vec2 POI = _oldPosition + t * _velocity;
                        differenceVector = POI - vectorsToCorners.ElementAt(currentReferenceCornerVector);
                        float distanceAlongLine = differenceVector.Dot(currentSideVector.Normalized());

                        if (distanceAlongLine >= 0 && distanceAlongLine <= currentVectorLength)
                        {
                            _velocity = new Vec2(0, 0);
                            _position = POI;
                        }
                    }
                    currentReferenceCornerVector++;
                }
            }
        }

        protected void Update()
        {
            AddViscosity();
            PlayerInput();
            BoundaryCollision();
            BoothCollision();
            EulerIntegration();
            if (_velocity.Length() > 0.4f) PlayerAnimation(_currentAnimation);
            else currentFrame = 0;
            Console.WriteLine(_position);
        }
    }
}