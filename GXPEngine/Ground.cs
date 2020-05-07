using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class Ground : AnimationSprite
{
    public Ground(float givenX, float givenY) : base("bricks.png", 1, 1)
    {
        SetXY(givenX, givenY);
        SetOrigin(width / 2, height / 2);
    }

    void Update()
    {
    }
}

