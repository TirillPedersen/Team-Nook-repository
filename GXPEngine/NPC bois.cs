using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


public class NPC : AnimationSprite
{
    public NPC(float givenX, float givenY) : base("NPC.png", 2, 2)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(givenX, givenY);
    }
}

