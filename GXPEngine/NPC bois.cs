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

    //void RandomNPC() 
    //{
    //    for (int i = 0; i < NPCList.count; i++)
    //    {

    //    }
    //    NPCRandomizer = Utils.Random(0, 100);

    //    if 
    //}

}

