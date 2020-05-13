using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GXPEngine
{
    class Character : AnimationSprite
    {
        public Vec2 Position;
        private Vec2 _oldPosition;
        private Vec2 _velocity;
        private Vec2 _acceleration;
        private static float _maxSpeed = 5.0f;
        private byte _currentAnimation;
        private float _animationSpeed, _timePassed, _mapWidth, _mapHeight;
        public Camera CharacterCamera;
        public Vec2 CharacterOffset;

        public Character(float givenX, float givenY, float mapWidth, float mapHeight) : base("PlayerSpritesheet.png", 4, 3)
        {
            SetOrigin(width / 2, height / 2);
            Position.SetXY(givenX, givenY);
            SetXY(givenX, givenY);
            CharacterCamera = new Camera(0, 0, game.width, game.height);
            game.AddChild(CharacterCamera);
            _velocity = new Vec2(0, 0);
            _acceleration = new Vec2(0, 0);
            _animationSpeed = 0;
            _currentAnimation = 0;
            _timePassed = 0;
            CharacterOffset = Position;
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
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
            _oldPosition = Position;
            _velocity += _acceleration;
            if (_velocity.Length() > _maxSpeed)
            {
                _velocity.Normalize();
                _velocity *= _maxSpeed;
            }
            Position += _velocity;
            SetXY(Position.x, Position.y);
        }

        private void PlayerAnimation(byte currentAnimation)
        {
            //Animation
            _timePassed += Time.deltaTime;
            if (_velocity.Length() > 1) _animationSpeed = 1 + (Time.time / 70 % 10);
            currentFrame = (int)_animationSpeed;

            //Rotation
            Vec2 rotationVector = Vec2.GetUnitVectorDeg(rotation);
            if ((_velocity.GetAngleDegrees() - rotationVector.GetAngleDegrees()) < -5) rotationVector.RotateDegrees(-10);
            else if ((_velocity.GetAngleDegrees() - rotationVector.GetAngleDegrees()) > 5) rotationVector.RotateDegrees(10);

            if (Math.Abs((_velocity.GetAngleDegrees() - rotationVector.GetAngleDegrees()) % 360) > 180) rotation = 0 - rotationVector.GetAngleDegrees();
            else rotation = rotationVector.GetAngleDegrees();
            Console.WriteLine(rotation);
        }

        private void BoundaryCollision()
        {
            if (Position.x < width / 2)
            {
                float a = _oldPosition.x - width / 2;
                float b = _oldPosition.x - Position.x;
                float POI = a / b;
                Position = _oldPosition + POI * _velocity;
                _velocity = new Vec2(0, 0);
            }
            else if (Position.x > _mapWidth - width / 2)
            {
                float a = _oldPosition.x - (_mapWidth - width / 2);
                float b = _oldPosition.x - Position.x;
                float POI = a / b;
                Position = _oldPosition + POI * _velocity;
                _velocity = new Vec2(0, 0);
            }
            else if (Position.y < height / 2)
            {
                float a = _oldPosition.y - height / 2;
                float b = _oldPosition.y - Position.y;
                float POI = a / b;
                Position = _oldPosition + POI * _velocity;
                _velocity = new Vec2(0, 0);
            }
            else if (Position.y > _mapHeight - height / 2)
            {
                float a = _oldPosition.y - (_mapHeight - height / 2);
                float b = _oldPosition.y - Position.y;
                float POI = a / b;
                Position = _oldPosition + POI * _velocity;
                _velocity = new Vec2(0, 0);
            }
        }

        public Vec2 CalculateCharacterOffset()
        {
            return new Vec2(Position.x - CharacterOffset.x, Position.y - CharacterOffset.y);
        }

        private void BoothCollision()
        {
            foreach (AnimationSprite currentStand in LevelLoader.CollisionObjectList)
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
                    Vec2 differenceVector = Position - vectorsToCorners.ElementAt(currentReferenceCornerVector);

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
                            Position = POI;
                        }
                    }
                    currentReferenceCornerVector++;
                }
            }
        }

        private void CameraMovement()
        {
            CharacterCamera.SetXY(x, y);
            LevelLoader.hud.SetXY(x - game.width / 2, y - game.height / 2);
        }

        protected void Update()
        {
            AddViscosity();
            PlayerInput();
            if (_velocity.Length() > 1.5f) PlayerAnimation(_currentAnimation);
            else currentFrame = 0;
            BoothCollision();
            BoundaryCollision();
            EulerIntegration();
            CameraMovement();
        }
    }
}