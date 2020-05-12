using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Carousel : AnimationSprite
    {
        private byte _currentFrame;

        public Carousel(float givenX, float givenY, float givenRotation) : base("Carousel.png", 6, 3)
        {
            SetXY(givenX, givenY);
            SetOrigin(width / 2, height / 2);
            rotation = givenRotation;
            _currentFrame = 0;
        }

        protected void Update()
        {
            if (_currentFrame >= 18) _currentFrame = 0;
            currentFrame = _currentFrame;
            _currentFrame++;
        }
    }
}
