using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Carousel : AnimationSprite
    {
        private byte _currentlyUsedFrame, _frameCounter;

        public Carousel(float givenX, float givenY, float givenRotation) : base("Carousel.png", 6, 3)
        {
            SetXY(givenX, givenY);
            SetOrigin(width / 2, height / 2);
            rotation = givenRotation;
            _currentlyUsedFrame = 0;
            _frameCounter = 0;
        }

        protected void Update()
        {
            if (_currentlyUsedFrame >= 18) _currentlyUsedFrame = 0;
            currentFrame = _currentlyUsedFrame;

            if (_frameCounter == 1)
            {
                _currentlyUsedFrame++;
                _frameCounter = 0;
            }
            else _frameCounter++;
        }
    }
}
