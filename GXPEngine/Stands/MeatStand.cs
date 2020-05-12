using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MeatStand : MarketStand
    {
        public MeatStand(float givenX, float givenY, int givenRotation = 0) : base(givenX, givenY, "MeatStand.png", givenRotation)
        { 
        }
    }
}
