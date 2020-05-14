using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class GarbageCan : AnimationSprite
{
    public GarbageCan(float givenX, float givenY) : base("GarbageCan.png", 1, 1)
    {
        SetXY(givenX, givenY);
    }

    void Update()
    {
    }
}

