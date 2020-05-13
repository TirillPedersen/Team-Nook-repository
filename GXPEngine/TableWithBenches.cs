using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class TableWithBenches : MarketStand
    {
        public TableWithBenches(float givenX, float givenY, float givenRotation = 0) : base(givenX, givenY, "tableWithBenches.png", givenRotation, cols: 1)
        {
            scale = 0.8f;
        }
    }
}
