using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class DirtyTile : Sprite
{
    public DirtyTile(float givenX, float givenY) : base("DirtyTile.png")
    {
        SetXY(givenX, givenY);
    }
}
