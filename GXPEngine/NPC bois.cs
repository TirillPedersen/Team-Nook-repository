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
        RandomNPC();

    }

    void RandomNPC()
    {
        int NPCAmount = 10;

        for (int i = 0; i < NPCAmount; i++)
        {
            int NPCRandomizer = Utils.Random(1, 4);

            if (NPCRandomizer == 1)
            {
                SetFrame(0);
            }

            if (NPCRandomizer == 2)
            {
                SetFrame(1);
            }

            if (NPCRandomizer == 3)
            {
                SetFrame(2);
            }

            if (NPCRandomizer == 4)
            {
                SetFrame(3);
            }
        }
    }
}

