using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class GarbageCan : Sprite
{
    public GarbageCan(float givenX, float givenY) : base("GarbageCan.png")
    {
        SetXY(givenX, givenY);
    }

    void Update()
    {
    }
}

