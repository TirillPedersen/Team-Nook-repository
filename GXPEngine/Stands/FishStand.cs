using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class FishStand : MarketStand
{
    public FishStand(float givenX, float givenY, float givenRotation = 0) : base(givenX, givenY, "fishShop.png", givenRotation)
    { 
    }
}
