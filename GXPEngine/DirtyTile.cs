using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class DirtyTile : AnimationSprite
{
    public DirtyTile(float givenX, float givenY) : base("DirtyTile.png", 1, 1)
    {
        SetXY(givenX, givenY);
    }
}
